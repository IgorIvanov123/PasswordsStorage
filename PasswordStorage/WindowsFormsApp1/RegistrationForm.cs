using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (passTBox.Text == trypassTBox.Text)
            {
                char[] checkLogin = loginTBox.Text.ToCharArray();
                char[] checkPass = passTBox.Text.ToCharArray();
                if (checkLogin.Contains(' ') || checkPass.Contains(' '))
                {
                    MessageBox.Show("Логин или пароль содержит пробел!");
                    loginTBox.Text = "";
                    passTBox.Text = "";
                    trypassTBox.Text = "";
                }
                else if(checkLogin.Count() == 0 || checkPass.Count() == 0)
                {
                    MessageBox.Show("Поля не могут быть пустыми!");
                    loginTBox.Text = "";
                    passTBox.Text = "";
                    trypassTBox.Text = "";
                }
                else
                {
                    Account newAccount = new Account(loginTBox.Text, passTBox.Text, 0);
                    AccountRegistrator ag = new AccountRegistrator();
                    ag.Registrate(newAccount);
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
                passTBox.Text = "";
                trypassTBox.Text = "";
            }
        }
    }
}
