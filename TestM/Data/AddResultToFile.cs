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
        private static string fileResult = path.Substring(0, path.IndexOf("bin")) + "Result.csv";
        public static void WriteName(string name, string subdivision, string date)
        {
            if (!File.Exists(fileResult))
            {
                using (FileStream file = File.Create(fileResult))
                {
                    ;
                }
            }

            bool IsEmpty = false;
            using (StreamReader sr = new StreamReader(fileResult))
            {
                string temp = sr.ReadToEnd();
                if (temp == "")
                {
                    IsEmpty = true;
                }
            }
            using (StreamWriter sw = new StreamWriter(fileResult, true, Encoding.GetEncoding(1251)))
            {
                if (IsEmpty)
                {
                    sw.Write(name + ";" + subdivision + ";" + date.ToString());
                }
                else
                {
                    sw.WriteLine();
                    sw.Write(name + ";" + subdivision + ";" + date.ToString());
                }
            }
        }
        public static void WritePoints(string points)
        {
            using (StreamWriter sw = new StreamWriter(fileResult, true, Encoding.GetEncoding(1251)))
            {
                if (Convert.ToInt32(points) >= 17)
                {
                    sw.Write(";" + points + " удовлетворительно");
                }
                else
                {
                    sw.Write(";" + points + " не удовлетворительно");
                }
            }
        }
    }
}
