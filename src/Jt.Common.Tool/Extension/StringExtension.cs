using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Jt.Common.Tool.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 判断字符串是否为空或只有空格
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// 判断字符串是否不为空或只有空格
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static bool IsNotNullOrWhiteSpace(this string input)
        {
            return !input.IsNullOrWhiteSpace();
        }

        /// <summary>
        /// 反序列化为指定类型的实例
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <returns></returns>
        public static T ToObj<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strInput">输入字符串（明文）</param>
        /// <returns></returns>
        public static string ToMD5(this string strInput)
        {
            string result = "";
            if (strInput.IsNotNullOrWhiteSpace())
            {
                MD5 md5 = MD5.Create();
                byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(strInput), 0, strInput.Length);
                char[] temp = new char[res.Length];
                Array.Copy(res, temp, res.Length);
                result = Encoding.Default.GetBytes(temp).BytesToHexString(false);
            }

            return result;
        }
    }
}
