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
        private static string newFileResult = path.Substring(0, path.IndexOf("bin"));
        private static string fileResult = path.Substring(0, path.IndexOf("bin")) + "Result.txt";
        public static void WriteName(string name, string subdivision, string date)
        {
            if (!File.Exists(fileResult))
            {
                File.Create(newFileResult);
            }
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
                    sw.Write($"ФИО: {name}, подразделение: {subdivision}, дата: {date}, ");
                }
                else
                {
                    sw.WriteLine();
                    sw.Write($"ФИО: {name}, подразделение: {subdivision}, дата: {date}, ");
                }
            }
        }
        public static void WritePoints(string points)
        {
            using (StreamWriter sw = new StreamWriter(fileResult, true))
            {
                sw.Write($"результат: {points}");
            }
        }
    }
}
