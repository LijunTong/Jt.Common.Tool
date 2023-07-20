using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Common.Tool.Helper
{
    public class AppDomainHelper
    {
        /// <summary>
        /// 获取程序根目标
        /// </summary>
        /// <returns></returns>
        public static string GetBaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 在根目录创建文件夹
        /// </summary>
        /// <param name="paths">多个目录组合，例如：/a/v/b</param>
        /// <returns></returns>
        public static string CreateDirectoryInBaseDirectory(params string[] paths)
        {
            string path = GetBaseDirectory();
            paths = paths.Prepend(path).ToArray();
            path = Path.Combine(paths);
            FileInfo fileInfo = new FileInfo(path);
            fileInfo.Directory.Create();
            return path;
        }
    }
}
