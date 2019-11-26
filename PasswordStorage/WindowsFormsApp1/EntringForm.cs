using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class EntringForm : Form
    {
        public List<string> GetList(int ID, Account account)
        {
            List<string> outputList = new List<string>();
            DirectoryInfo di = new DirectoryInfo(@"Accounts\");
            FileInfo[] fileInfos = di.GetFiles();
            foreach (var items in fileInfos)
            {
                if(items.Name == ID.ToString()+".txt")
                {
                    using (StreamReader sr = new StreamReader(items.FullName))
                    {
                        Shifrator sh = new Shifrator();
                        string checkString = sr.ReadLine();
                        while(checkString != null && checkString != "")
                        {
                            outputList.Add(sh.Decrypt(checkString, account.accountPassword));
                            checkString = sr.ReadLine();                           
                        }
                    }
                }
            }
            return outputList;
        }
        public EntringForm()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Account incomingAccount = new Account(AutLTBox.Text, AutPTBox.Text, 0);
            List<string> list1 = new List<string>();
            incomingAccount.resoursesList = list1;
            AccountRegistrator ar = new AccountRegistrator();
            try
            {
                using (StreamReader sr = new StreamReader(@"Accounts\list.txt", Encoding.Default))
                {
                    sr.ReadLine();
                    string inputInformation = sr.ReadToEnd();
                    try
                    {
                        if (ar.IsSimilarAccount(inputInformation, incomingAccount) == false)
                        {
                            incomingAccount.ID = ar.GetID(inputInformation, incomingAccount);
                            incomingAccount.resoursesList = GetList(incomingAccount.ID, incomingAccount);
                            MainForm mainForm = new MainForm(incomingAccount);
                            mainForm.Show();
                            Hide();
                            sr.Close();
                        }
                        else MessageBox.Show("Неверный логин/пароль!");
                        
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("Неверный логин/пароль!");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл с пользователями не найден! Создан новый файл");
                using (StreamWriter sr = new StreamWriter(@"Accounts\list.txt"))
                {
                    sr.WriteLine("0");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный логин/пароль!");
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            RegistrationForm rf = new RegistrationForm();
            rf.ShowDialog();
        
        }
    }
}
