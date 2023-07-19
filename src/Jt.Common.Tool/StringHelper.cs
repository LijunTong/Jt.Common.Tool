using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Jt.Common.Tool
{
    /// <summary>
    /// 字符串的一些操作类
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 获取指定长度的字符串(先将输入字符串转化为字节序列)
        /// </summary>
        /// <param name="strLine">输入字符串</param>
        /// <param name="index">字节数组索引</param>
        /// <param name="count">字节数组个数</param>
        /// <returns></returns>
        public static string GetStrByBytesIndex(string strLine, int index, int count)
        {
            string result = string.Empty;
            if (strLine != "")
            {
                byte[] bytes = Encoding.Default.GetBytes(strLine);
                if (bytes.Length < count)
                {
                    result = strLine;
                }
                else
                {
                    result = Encoding.Default.GetString(bytes, index, count);
                }
            }
            return result;
        }

        /// <summary>
        /// 在字符串右边填充字符，使其达到指定长度（汉字按两字节算）
        /// </summary>
        /// <param name="strSrc">原字符串</param>
        /// <param name="totalLen">欲达到的总长度</param>
        /// <param name="padChar">需补字符</param>
        /// <returns></returns>
        public static string AddStrRight(string strSrc, int totalLen, char padChar)
        {
            byte[] temp = Encoding.Default.GetBytes(strSrc);
            int srcLen = temp.Length;
            int needPadLen = totalLen - srcLen;

            string strOut = strSrc;

            if (srcLen > totalLen)
            {
                strOut = GetStrByBytesIndex(strSrc, 0, totalLen);
                if (strOut.EndsWith("?"))
                {
                    strOut = GetStrByBytesIndex(strSrc, 0, totalLen - 1) + " ";
                }
            }
            else
            {
                strOut = strOut + new string(padChar, needPadLen);
            }

            return strOut;
        }

        /// <summary>
        /// 在字符串左边填充字符，使其达到指定长度（汉字按两字节算）
        /// </summary>
        /// <param name="strSrc">原字符串</param>
        /// <param name="totalLen">欲达到的总长度</param>
        /// <param name="padChar">需补字符</param>
        /// <returns></returns>
        public static string AddStrLeft(string strSrc, int totalLen, char padChar)
        {
            byte[] temp = Encoding.Default.GetBytes(strSrc);
            int srcLen = temp.Length;
            int needPadLen = totalLen - srcLen;

            string strOut = strSrc;
            if (srcLen > totalLen)
            {
                strOut = GetStrByBytesIndex(strSrc, 0, totalLen);
            }
            else
            {
                strOut = new string(padChar, needPadLen) + strOut;
            }

            return strOut;
        }

        /// <summary>
        /// byte数组转换为对应的16进制字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="isSpace">是否有空格,默认为有:true</param>
        /// <returns></returns>
        public static string BytesToHexString(byte[] buffer, bool isSpace = true)
        {
            StringBuilder sb = new StringBuilder(buffer.Length * 3);
            for (int i = 0; i < buffer.Length; i++)
            {
                byte value = buffer[i];
                if (isSpace)
                {
                    sb.Append(Convert.ToString(value, 16).PadLeft(2, '0').PadRight(3, ' '));
                }
                else
                {
                    sb.Append(Convert.ToString(value, 16).PadLeft(2, '0'));
                }
            }
            return sb.ToString().Trim().ToUpper();
        }

        /// <summary>
        /// byte数组转换为对应的16进制字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="padChar">填充字符</param>
        /// <returns></returns>
        public static string BytesToHexString(byte[] buffer, char padChar)
        {
            StringBuilder sb = new StringBuilder(buffer.Length * 3);
            for (int i = 0; i < buffer.Length; i++)
            {
                byte value = buffer[i];
                sb.Append(Convert.ToString(value, 16).PadLeft(2, '0').PadRight(3, padChar));
            }
            string result = sb.ToString().ToUpper();
            result = result.Substring(0, result.Length - 1);//最后一个填充符不要了
            return result;
        }

        /// <summary>
        /// 将byte类型的转换为16进制字符串
        /// </summary>
        /// <param name="bData"></param>
        /// <returns></returns>
        public static string ByteToHexString(byte bData)
        {
            return Convert.ToString(bData, 16).PadLeft(2, '0').ToUpper();
        }

        /// <summary>
        /// 16进制的字符串转换为Byte数组
        /// </summary>
        /// <param name="strHexData"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string strHexData)
        {
            if (strHexData.Length % 2 != 0)
            {
                throw new Exception("asc 数据不是2的倍数！");
            }
            byte[] bytes = new byte[strHexData.Length / 2];
            for (int i = 0; i < strHexData.Length / 2; i++)
            {
                bytes[i] = Convert.ToByte(strHexData.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        /// <summary>
        /// byte数组转换为字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string BytesToString(byte[] buffer)
        {
            return Encoding.Default.GetString(buffer);
        }

        /// <summary>
        /// 字符串转换为byte数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] StringToBytes(string str)
        {
            return !string.IsNullOrEmpty(str) ? Encoding.Default.GetBytes(str) : null;
        }

        /// <summary>
        /// 将byte数组每一位转换为10进制的字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string BytesToIntString(byte[] buffer)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                int value = buffer[i];
                sb.Append(value.ToString().PadLeft(2, '0'));
            }
            return sb.ToString().Trim().ToUpper();
        }

        /// <summary>
        /// 全是数字的字符串转换为16进制bytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] IntStringToBytes(string strValue)
        {
            if (Regex.IsMatch(strValue, "[a-z|A-Z]"))
            {
                throw new Exception("传入参数[" + strValue + "]有非数字");
            }

            if (strValue.Length % 2 != 0)
            {
                throw new Exception("传入参数[" + strValue + "]的长度必须是2的倍数");
            }

            byte[] result = new byte[strValue.Length / 2];
            for (int i = 0; i < strValue.Length / 2; i++)
            {
                result[i] = Convert.ToByte(strValue.Substring(i * 2, 2));
            }

            return result;
        }

        /// <summary>
        /// 将char数组转换成对应的字符串
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string CharsToString(char[] chars)
        {
            return Encoding.Default.GetString(Encoding.Default.GetBytes(chars));
        }

        /// <summary>
        /// 移除字节数组
        /// 指定位置的指定字符
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] RemoveByte(byte[] buffer, bool isLeft, byte bAdd)
        {
            List<byte> lstTemp = new List<byte>();
            lstTemp.AddRange(buffer);

            int startIndex = 0;
            if (isLeft)
            {
                for (int i = 0; i < lstTemp.Count; i++)
                {
                    if (lstTemp[i] != bAdd)
                    {
                        startIndex = i;
                        break;
                    }
                }
                if (startIndex > 0)
                {
                    lstTemp.RemoveRange(0, startIndex);
                }
            }
            else
            {
                for (int i = lstTemp.Count - 1; i >= 0; i--)
                {
                    if (lstTemp[i] != bAdd)
                    {
                        startIndex = i;
                        break;
                    }
                }
                if (startIndex < lstTemp.Count - 1)
                {
                    lstTemp.RemoveRange(startIndex + 1, lstTemp.Count - startIndex - 1);
                }
            }


            return lstTemp.ToArray();
        }

        /// <summary>
        /// byte数组末尾去零
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static byte[] RemoveZerosEnd(byte[] buffer)
        {
            List<byte> lstTmps = new List<byte>();
            int endPos = 0;
            for (int i = buffer.Length - 1; i >= 0; i--)
            {
                if (buffer[i] != 0X00)
                {
                    endPos = i;
                    break;
                }
            }

            for (int i = 0; i <= endPos; i++)
            {
                lstTmps.Add(buffer[i]);
            }

            return lstTmps.ToArray();
        }

        /// <summary>
        /// 从 startIndex 开始的四个字节构成的单精度浮点数。
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <param name="startIndex">起始索引</param>
        /// <returns></returns>
        public static float BytesToFloat(byte[] buffer, int startIndex)
        {
            if (buffer == null || startIndex + 4 > buffer.Length)
            {
                throw new Exception("参数buffer，格式有误!");
            }
            float fValue = BitConverter.ToSingle(buffer, startIndex);
            return fValue;
        }

        /// <summary>
        /// 将浮点数转换成bytes
        /// </summary>
        /// <param name="fValue"></param>
        /// <returns></returns>
        public static byte[] FloatToBytes(float fValue)
        {
            byte[] bytes = BitConverter.GetBytes(fValue);
            return bytes;
        }

        /// <summary>
        /// 字符串转换为指定格式的列表
        /// </summary>
        /// <param name="value">字符串内容</param>
        /// <param name="delimiter">分隔符号</param>
        /// <returns></returns>
        public static List<T> ToDelimitedList<T>(string value, string delimiter)
        {
            if (value == null)
            {
                return new List<T>();
            }

            var output = value.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            return output.Select(x => (T)Convert.ChangeType(x, typeof(T))).ToList();
        }
    }
}
