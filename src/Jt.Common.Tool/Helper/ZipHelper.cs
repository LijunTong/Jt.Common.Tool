using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Common.Tool.Helper
{
    public class ZipHelper
    {
        /// <summary>
        /// 根据给的文件参数，自动进行压缩或解压缩操作
        /// </summary>
        public static void Process(string[] files, string Password = null)
        {
            if (files.Length > 0)
            {
                if (files.Length == 1 && (files[0].ToLower().EndsWith(".zip") || files[0].ToLower().EndsWith(".rar")))
                {
                    Unzip(files[0], null, Password, null);                  // 解压缩
                }
                else
                {
                    string zipPath = ResSystemHelper.GetPathNoExt(files[0]) + ".zip";	// 以待压缩的第一个文件命名生成的压缩文件
                    string BaseDir = ResSystemHelper.GetParent(files[0]);				// 获取第一个文件的父路径信息
                    if (files.Length == 1)									// 若载入的为单个目录，则已当前目录作为基础路径
                    {
                        string file = files[0];
                        if (Directory.Exists(file))
                            BaseDir = file;
                    }

                    string[] subFiles = ResSystemHelper.GetSubFiles(files);			// 获取args对应的所有目录下的文件列表
                    Zip(zipPath, BaseDir, subFiles, Password, null);		// 对载入的文件进行压缩操作
                }
            }
        }

        /// <summary>
        /// 压缩所有文件为zip
        /// </summary>
        public static bool ZipFiles(string[] files, string Password = null, string[] ignoreNames = null)
        {
            return Zip(null, null, files, Password, ignoreNames);
        }

        /// <summary>
        /// 压缩指定的文件或文件夹为zip
        /// </summary>
        public static bool Zip(string file, string Password = null, string[] ignoreNames = null)
        {
            return Zip(null, null, new string[] { file }, Password, ignoreNames);
        }

        /// <summary>
        /// 判断fileName中是否含有ignoreNames中的某一项
        /// </summary>
        private static bool ContainsIgnoreName(string fileName, string[] ignoreNames)
        {
            if (ignoreNames != null && ignoreNames.Length > 0)
            {
                foreach (string name in ignoreNames)
                {
                    if (fileName.Contains(name))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 压缩所有文件files，为压缩文件zipFile, 以相对于BaseDir的路径构建压缩文件子目录，ignoreNames指定要忽略的文件或目录
        /// </summary>
        public static bool Zip(string zipPath, string BaseDir, string[] files, string Password = null, string[] ignoreNames = null)
        {
            if (files == null || files.Length == 0)
                return false;
            if (zipPath == null || zipPath.Equals(""))
                zipPath = ResSystemHelper.GetPathNoExt(files[0]) + ".zip";	// 默认以第一个文件命名压缩文件
            if (BaseDir == null || BaseDir.Equals(""))
                BaseDir = ResSystemHelper.GetParent(files[0]);             // 默认以第一个文件的父目录作为基础路径
                                                                           //Console.WriteLine("所有待压缩文件根目录：" + BaseDir);


            ResSystemHelper.Mkdirs(ResSystemHelper.GetParent(zipPath));         // 创建目标路径
                                                                                //Console.WriteLine("创建压缩文件：" + zipPath);

            FileStream input = null;
            ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipPath));
            if (Password != null && !Password.Equals(""))
                zipStream.Password = Password;

            files = ResSystemHelper.GetSubFiles(files);               // 获取子目录下所有文件信息
            for (int i = 0; i < files.Length; i++)
            {
                if (ContainsIgnoreName(files[i], ignoreNames))
                    continue;    // 跳过忽略的文件或目录

                string entryName = ResSystemHelper.RelativePath(BaseDir, files[i]);
                zipStream.PutNextEntry(new ZipEntry(entryName));
                //Console.WriteLine("添加压缩文件：" + entryName);

                if (File.Exists(files[i]))                  // 读取文件内容
                {
                    input = File.OpenRead(files[i]);
                    Random rand = new Random();
                    byte[] buffer = new byte[10240];
                    int read = 0;
                    while ((read = input.Read(buffer, 0, 10240)) > 0)
                    {
                        zipStream.Write(buffer, 0, read);
                    }
                    input.Close();
                }
            }
            zipStream.Close();
            //Console.WriteLine("文件压缩完成！");
            return true;
        }

        /// <summary>
        /// 解压文件 到指定的路径，可通过targeFileNames指定解压特定的文件
        /// </summary>
        public static bool Unzip(string zipPath, string targetPath = null, string Password = null, string[] targeFileNames = null)
        {
            if (File.Exists(zipPath))
            {
                if (targetPath == null || targetPath.Equals(""))
                    targetPath = ResSystemHelper.GetPathNoExt(zipPath);
                //Console.WriteLine("解压文件：" + zipPath);
                //Console.WriteLine("解压至目录：" + targetPath);


                ZipInputStream zipStream = null;
                FileStream bos = null;

                zipStream = new ZipInputStream(File.OpenRead(zipPath));
                if (Password != null && !Password.Equals(""))
                    zipStream.Password = Password;

                ZipEntry entry = null;
                while ((entry = zipStream.GetNextEntry()) != null)
                {
                    if (targeFileNames != null && targeFileNames.Length > 0)                // 若指定了目标解压文件
                    {
                        if (!ContainsIgnoreName(entry.Name, targeFileNames))
                            continue;      // 跳过非指定的文件
                    }

                    string target = Path.Combine(targetPath, entry.Name);
                    if (entry.IsDirectory)
                        ResSystemHelper.Mkdirs(target); // 创建目标路径
                    if (entry.IsFile)
                    {
                        ResSystemHelper.Mkdirs(ResSystemHelper.GetParent(target));

                        bos = File.Create(target);
                        //Console.WriteLine("解压生成文件：" + target);

                        int read = 0;
                        byte[] buffer = new byte[10240];
                        while ((read = zipStream.Read(buffer, 0, 10240)) > 0)
                        {
                            bos.Write(buffer, 0, read);
                        }
                        bos.Flush();
                        bos.Close();
                    }
                }
                zipStream.CloseEntry();

                //Console.WriteLine("解压完成！");
                return true;
            }

            return false;
        }
    }
}
