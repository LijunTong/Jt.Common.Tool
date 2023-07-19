using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Common.Tool
{
    public class FileInfoHelper
    {
        public static string SaveToFile(string fileDir, string fileName, string content)
        {
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            string filePath = Path.Combine(fileDir, fileName);
            FileInfo fileInfo = new FileInfo(filePath);
            Directory.CreateDirectory(fileInfo.DirectoryName);
            return SaveToFile(filePath, content);
        }

        public static string SaveToFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
            return filePath;
        }
    }
}
