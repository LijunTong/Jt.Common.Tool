using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Common.Tool
{
    public class ResSystemHelper
    {
        /// <summary>
        /// 检测目录是否存在，若不存在则创建
        /// </summary>
        public static void Mkdirs(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 获取去除拓展名的文件路径
        /// </summary>
        public static string GetPathNoExt(string path)
        {
            if (File.Exists(path))
                return Path.Combine(Directory.GetParent(path).FullName, Path.GetFileNameWithoutExtension(path));
            else
                return Path.Combine(Directory.GetParent(path).FullName, Path.GetFileNameWithoutExtension(path));
        }

        /// <summary>
        /// 获取父目录的路径信息
        /// </summary>
        public static string GetParent(string path)
        {
            return Directory.GetParent(path).FullName;
        }


        /// <summary>
        /// 获取父目录的路径信息
        /// </summary>
        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        /// <summary>
        /// 获取filePath的相对于BaseDir的路径
        /// </summary>
        public static string RelativePath(string BaseDir, string filePath)
        {
            string relativePath = "";
            if (filePath.StartsWith(BaseDir))
                relativePath = filePath.Substring(BaseDir.Length);
            return relativePath;
        }


        //-----------------------------------------------------------------------------------------

        /// <summary>
        /// 获取paths路径下所有文件信息
        /// </summary>
        public static string[] GetSubFiles(string[] Paths)
        {
            List<string> list = new List<string>();         // paths路径下所有文件信息

            foreach (string path in Paths)
            {
                List<string> subFiles = GetSubFiles(path);	// 获取路径path下所有文件列表信息
                list = ListAdd(list, subFiles);
            }

            string[] A = list.ToArray();                  // 转化为数组形式

            return A;
        }

        /// <summary>
        /// 合并list1和list2到新的list
        /// </summary>
        public static List<string> ListAdd(List<string> list1, List<string> list2)
        {
            List<string> list = new List<string>();

            foreach (string path in list1)
                if (!list.Contains(path))
                    list.Add(path);
            foreach (string path in list2)
                if (!list.Contains(path))
                    list.Add(path);

            return list;
        }

        /// <summary>
        /// 获取file目录下所有文件列表
        /// </summary>
        public static List<string> GetSubFiles(string file)
        {
            List<string> list = new List<string>();

            if (File.Exists(file))
            {
                if (!list.Contains(file))
                    list.Add(file);
            }

            if (Directory.Exists(file))
            {
                // 获取目录下的文件信息
                foreach (string iteam in Directory.GetFiles(file))
                {
                    if (!list.Contains(iteam))
                        list.Add(iteam);
                }

                // 获取目录下的子目录信息
                foreach (string iteam in Directory.GetDirectories(file))
                {
                    List<string> L = GetSubFiles(iteam);	// 获取子目录下所有文件列表
                    foreach (string path in L)
                    {
                        if (!list.Contains(path))
                            list.Add(path);
                    }
                }

                // 记录当前目录
                if (Directory.GetFiles(file).Length == 0 && Directory.GetDirectories(file).Length == 0)
                {
                    if (!list.Contains(file))
                        list.Add(file);
                }
            }

            return list;
        }
    }
}
