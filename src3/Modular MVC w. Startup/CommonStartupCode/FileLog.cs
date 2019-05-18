using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonStartupCode
{
    public class FileLog
    {
        private const string FileName = "WriteLog.txt";
        public static void Write(string folderPath, string lineOfText)
        {
            File.AppendAllText(Path.Combine(folderPath, FileName), $"{DateTime.Now}:=>{lineOfText}{Environment.NewLine}");
        }
        public static string ReadLog(string folderPath)
        {
            return File.ReadAllText(Path.Combine(folderPath,FileName));
        }
        public static void ClearLog(string folderPath)
        {
            File.WriteAllText(Path.Combine(folderPath, FileName), string.Empty);
        }
    }
}
