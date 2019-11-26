using System;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Account
    {
        public string accountLogin;
        public string accountPassword;
        public int ID;
        public List<string> resoursesList;
        public Account(string inputLogin, string inputPassword, int id)
        {
            accountLogin = inputLogin;
            accountPassword = inputPassword;
            resoursesList = new List<string>();
            ID = id;
        }
    }
    class AccountRegistrator : IRegistrator
    {
        public void Registrate(Account newAccount)
        {
            string allInformationFromFile;
            int count = 0;
            using (StreamReader sr = new StreamReader(@"Accounts\list.txt", Encoding.Default))
            {               
                count = Convert.ToInt32(sr.ReadLine());
                allInformationFromFile = sr.ReadToEnd();
            }
            if (IsSimilarAccount(allInformationFromFile, newAccount) == false)
            {
                MessageBox.Show("Такой логин уже существует!");
                return;
            }
            using (StreamWriter sw = new StreamWriter(@"Accounts\list.txt", false, Encoding.Default))
            {
                count++;
                sw.WriteLine(count.ToString());
                sw.Write(allInformationFromFile);
                Shifrator encrypter = new Shifrator();
                sw.WriteLine(encrypter.Encrypt(newAccount.accountLogin + " " + count,"100501"));

                CreateNewFile(count.ToString());
                MessageBox.Show("Вы успешно зарегестрированы!");
            }
        }
        public bool IsSimilarAccount(string inputInformation, Account checkedAccount)
        {
            char[] delimiterChars = { '\n' };
            string[] loginsAndPasswords = inputInformation.Split(delimiterChars);
            Shifrator decrypter = new Shifrator();
            for (int i = 0; i<loginsAndPasswords.Length; i++)
            {
                string str = decrypter.Decrypt(loginsAndPasswords[i], "100501");
                loginsAndPasswords[i] = decrypter.Decrypt(loginsAndPasswords[i], "100501");
            }

            for(int i = 0; i<loginsAndPasswords.Length; i++)
            {
                if (loginsAndPasswords[i].Contains(checkedAccount.accountLogin))
                {
                    return false;
                }
            }
            return true;
        }
        public int GetID(string inputInformation, Account checkedAccount)
        {
            char[] delimiterChars = { '\n' };
            string[] loginsAndPasswords = inputInformation.Split(delimiterChars);
            Shifrator decrypter = new Shifrator();
            for (int i = 0; i < loginsAndPasswords.Length; i++)
            {
                string str = decrypter.Decrypt(loginsAndPasswords[i], "100501");
                loginsAndPasswords[i] = str;
            }

            for (int i = 0; i < loginsAndPasswords.Length; i++)
            {
                if (loginsAndPasswords[i].Contains(checkedAccount.accountLogin))
                {
                    string[] cur = loginsAndPasswords[i].Split(' ');
                    return Convert.ToInt32(cur[1]);
                }
            }
            return -1;
        }
        private void CreateNewFile(string accountID)
        {
            string path = @"Accounts\" + accountID + ".txt";
            Stream str;
            using (str = File.Create(path)) { }            
        }
    }

}
