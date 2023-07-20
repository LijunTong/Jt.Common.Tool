using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Common.Tool.Helper
{
    public class FileInfoHelper
    {
        /// <summary>
        /// 将文本写入文件
        /// </summary>
        /// <param name="fileDir">文件目录</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="content">文本内容</param>
        /// <returns></returns>
        public static string WriteToFile(string fileDir, string fileName, string content)
        {
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            string filePath = Path.Combine(fileDir, fileName);
            FileInfo fileInfo = new FileInfo(filePath);
            Directory.CreateDirectory(fileInfo.DirectoryName);
            return WriteToFile(filePath, content);
        }

        public static string WriteToFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
            return filePath;
        }
    }
}
