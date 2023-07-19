using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;

namespace Jt.Common.Tool
{
    /// <summary>
    /// DES加解密辅助类
    /// </summary>
    public class MD5Helper
    {

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strInput">输入字符串（明文）</param>
        /// <returns></returns>
        public static string EncryptMD5(string strInput)
        {
            string result = "";
            if (strInput != "")
            {
                MD5 md5 = MD5.Create();
                byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(strInput), 0, strInput.Length);
                char[] temp = new char[res.Length];
                Array.Copy(res, temp, res.Length);
                result = StringHelper.BytesToHexString(Encoding.Default.GetBytes(temp), false);
            }

            return result;
        }
    }

}
