using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private Account saveAccount { get; set; }
        private void ReWrite()
        {
            using (StreamWriter sw = new StreamWriter(@"Accounts\" + saveAccount.ID + ".txt", false, Encoding.Default))
            {
                Shifrator encrypter = new Shifrator();
                for (int j = 0; j < saveAccount.resoursesList.Count(); j++)
                {
                    sw.WriteLine(encrypter.Encrypt(saveAccount.resoursesList[j], saveAccount.accountPassword));
                }
            }
        }
        public MainForm()
        {          
            InitializeComponent();
        }
        public MainForm(Account account)
        {
            InitializeComponent();
            saveAccount = account;
            for (int i = 0; i< account.resoursesList.Count; i++)
            {
                dataGridView1.Rows.Add();                
                string[] data = account.resoursesList[i].Split(' ');
                dataGridView1.Rows[i].Cells[0].Value = data[0];
                dataGridView1.Rows[i].Cells[1].Value = data[1];
                dataGridView1.Rows[i].Cells[2].Value = data[2];
                dataGridView1.Rows[i].ReadOnly = true;
            }
            
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }   

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddingForm addingForm = new AddingForm();
            DialogResult dr = new DialogResult();
            dr = addingForm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (addingForm.resorseTBox.Text == "" || addingForm.loginTBox.Text == "" || addingForm.passTBox.Text == "")
                {
                    MessageBox.Show("Одно или несколько полей пусты!");
                }
                else if (addingForm.resorseTBox.Text.Contains(" ") || addingForm.loginTBox.Text.Contains(" ") || addingForm.passTBox.Text.Contains(" "))
                {
                    MessageBox.Show("Поля не должны содержать пробелы!");
                }
                else
                {
                    int i = dataGridView1.Rows.Count;
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = addingForm.resorseTBox.Text;
                    dataGridView1.Rows[i].Cells[1].Value = addingForm.loginTBox.Text;
                    dataGridView1.Rows[i].Cells[2].Value = addingForm.passTBox.Text;
                    dataGridView1.Rows[i].ReadOnly = true;
                    saveAccount.resoursesList.Add(addingForm.resorseTBox.Text + " " + addingForm.loginTBox.Text + " " + addingForm.passTBox.Text); 
                    ReWrite();
                }
                
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            ChanginForm changinForm = new ChanginForm();
            DialogResult dr1 = new DialogResult();
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Выбирите элемент!");
                return;
            }
            int i = dataGridView1.CurrentRow.Index;         
            changinForm.resourseTBox.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            changinForm.loginTBox.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            changinForm.passTBox.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            dr1 = changinForm.ShowDialog();
            
            
            if (dr1 == DialogResult.OK)
            {
                if (changinForm.resourseTBox.Text == "" || changinForm.loginTBox.Text == "" || changinForm.passTBox.Text == "")
                {
                    MessageBox.Show("Одно или несколько полей пусты!");
                }
                else if (changinForm.resourseTBox.Text.Contains(" ") || changinForm.loginTBox.Text.Contains(" ") || changinForm.passTBox.Text.Contains(" "))
                {
                    MessageBox.Show("Поля не должны содержать пробелы!");
                }
                else
                {
                    saveAccount.resoursesList[i] = changinForm.resourseTBox.Text + " " + changinForm.loginTBox.Text + " " + changinForm.passTBox.Text;
                    dataGridView1.Rows[i].Cells[0].Value = changinForm.resourseTBox.Text;
                    dataGridView1.Rows[i].Cells[1].Value = changinForm.loginTBox.Text;
                    dataGridView1.Rows[i].Cells[2].Value = changinForm.passTBox.Text;
                    dataGridView1.Rows[i].ReadOnly = true;                    
                    ReWrite();
                    MessageBox.Show("Изменения сохранены!");                                       
                }
            }                      
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Выберите элемет!");
            }
            else
            {
                saveAccount.resoursesList.RemoveAt(dataGridView1.CurrentRow.Index);
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                ReWrite();
            }
        }     
    }
}
