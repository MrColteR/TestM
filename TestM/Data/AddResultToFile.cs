using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestM.Data
{
    public static class AddResultToFile
    {
        private static string path = Directory.GetCurrentDirectory();
        private static string fileResult = path.Substring(0, path.IndexOf("bin")) + "Result.txt";
        public static void Write(string name, string subdivision, string date)
        {
            using (StreamWriter sw = new StreamWriter(fileResult, true))
            {
                sw.Write(" ");
                sw.Write(name);
                sw.Write(" ");
                sw.Write(subdivision);
                sw.Write(" ");
                sw.Write(date);
            }
        }
    }
}
