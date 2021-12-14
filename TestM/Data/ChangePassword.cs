using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestM.Data
{
    public class ChangePassword
    {
        private static string path = Directory.GetCurrentDirectory();
        private readonly string filePassword = path.Substring(0, path.IndexOf("bin")) + "Password.txt";
        public string Read()
        {
            using (StreamReader reader = new StreamReader(filePassword))
            {
                return reader.ReadLine();
            }
        }
        public void Change(string password)
        {
            using (StreamWriter sw = new StreamWriter(File.Create(filePassword)))
            {
                sw.Write(password);
            }
        }
    }
}
