using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    interface IShifrator
    {
        string Encrypt(string encryptedString, string password);
        string Decrypt(string encryptedString, string password);
    }

    interface IRegistrator
    {
        void Registrate(Account newAccount);
    }

    interface ISerializator
    {
        void Serialize(object serializedInformation);
    }
}
