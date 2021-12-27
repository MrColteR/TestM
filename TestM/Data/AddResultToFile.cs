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
        public static void WriteName(string name, string subdivision, string date)
        {
            bool IsEmpty = false;
            using (StreamReader sr = new StreamReader(fileResult))
            {
                var temp = sr.ReadToEnd();
                if (temp == "")
                {
                    IsEmpty = true;
                }
            }
            using (StreamWriter sw = new StreamWriter(fileResult, true))
            {
                if (IsEmpty)
                {
                    sw.Write("ФИО: ");
                    sw.Write(name);
                    sw.Write(" ");
                    sw.Write("Подразделение: ");
                    sw.Write(subdivision);
                    sw.Write(" ");
                    sw.Write("Дата: ");
                    sw.Write(date);
                    sw.Write(" ");
                }
                else
                {
                    sw.WriteLine();
                    sw.Write("ФИО: ");
                    sw.Write(name);
                    sw.Write(" ");
                    sw.Write("Подразделение: ");
                    sw.Write(subdivision);
                    sw.Write(" ");
                    sw.Write("Дата: ");
                    sw.Write(date);
                    sw.Write(" ");
                }
            }
        }
        public static void WritePoints(string points)
        {
            using (StreamWriter sw = new StreamWriter(fileResult, true))
            {
                sw.Write("Резальтат:");
                sw.Write(points);
                sw.Write(" ");
            }
        }
    }
}
