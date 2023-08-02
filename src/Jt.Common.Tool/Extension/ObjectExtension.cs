using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Jt.Common.Tool.Extension
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 实例对象序列化成json
        /// </summary>
        /// <param name="obj">实例</param>
        /// <returns>string</returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 深拷贝（反射）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">实例</param>
        /// <returns></returns>
        public static T DeepCopyByReflect<T>(this T obj) where T : class
        {
            Type type = typeof(T);
            var tout = (T)Activator.CreateInstance(type);
            foreach (var item in type.GetProperties())
            {
                var propIn = type.GetProperty(item.Name);
                if (propIn != null)
                {
                    item.SetValue(tout, propIn.GetValue(obj));
                }
            }
            return tout;
        }

        /// <summary>
        /// 深拷贝（序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">实例</param>
        /// <returns></returns>
        public static T DeepCopyBySerialize<T>(this T obj) where T : class
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 复制有相同属性的值到另一个实例
        /// </summary>
        /// <param name="obj">实例</param>
        /// <returns></returns>
        public static TOut CopyValue<TIn, TOut>(this TIn obj)
        {
            Type typeIn = typeof(TIn);
            Type typeOut = typeof(TOut);
            var tout = (TOut)Activator.CreateInstance(typeOut);
            foreach (var item in typeOut.GetProperties())
            {
                var propIn = typeIn.GetProperty(item.Name);
                if (propIn != null)
                {
                    item.SetValue(tout, propIn.GetValue(obj));
                }
            }
            return tout;
        }

        /// <summary>
        /// 对列表元素操作，复制有相同属性的值到另一个实例
        /// </summary>
        /// <param name="obj">列表</param>
        /// <returns></returns>
        public static List<TOut> CopyValue<TIn, TOut>(this List<TIn> obj)
        {
            List<TOut> outs = new List<TOut>();
            foreach (var item in obj)
            {
                outs.Add(item.CopyValue<TIn, TOut>());
            }
            return outs;
        }

        /// <summary>
        /// 实例字符串类型字段填充空字符串
        /// </summary>
        /// <param name="obj"><实例/param>
        /// <returns></returns>
        public static T FillEmptyString<T>(this T obj)
        {
            Type type = obj.GetType();
            foreach (var item in type.GetProperties())
            {
                if (item.PropertyType == typeof(string))
                {
                    var value = item.GetValue(obj);
                    if (value == null)
                    {
                        item.SetValue(obj, "");
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// byte数组转换为对应的16进制字符串，是否有空格，默认为有:true
        /// </summary>
        /// <param name="buffer">buffer</param>
        /// <param name="isSpace">是否有空格，默认为有:true</param>
        /// <returns></returns>
        public static string BytesToHexString(this byte[] buffer, bool isSpace = true)
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
        /// byte数组转换为对应的16进制字符串，填充字符
        /// </summary>
        /// <param name="buffer">buffer</param>
        /// <param name="padChar">填充字符</param>
        /// <returns></returns>
        public static string BytesToHexString(this byte[] buffer, char padChar)
        {
            StringBuilder sb = new StringBuilder(buffer.Length * 3);
            for (int i = 0; i < buffer.Length; i++)
            {
                byte value = buffer[i];
                sb.Append(Convert.ToString(value, 16).PadLeft(2, '0').PadRight(3, padChar));
            }
            string result = sb.ToString().ToUpper();
            result = result.Substring(0, result.Length - 1); // 最后一个填充符不要了
            return result;
        }

        /// <summary>
        /// 将byte类型的转换为16进制字符串
        /// </summary>
        /// <param name="bData">bData</param>
        /// <returns></returns>
        public static string ByteToHexString(this byte bData)
        {
            return Convert.ToString(bData, 16).PadLeft(2, '0').ToUpper();
        }

        /// <summary>
        /// 比较两个实例的字段的ToString()值是否完全相等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">实例</param>
        /// <param name="target">目标实例</param>
        /// <returns></returns>
        public static bool ValueEquals<T>(this T t, T target)
        {
            if(t == null || target == null)
            {
                return false;
            }

            Type type = typeof(T);
            foreach (var prop in type.GetProperties())
            {
                object value1 = prop.GetValue(target);
                object value2 = prop.GetValue(t);
                if (value1.ToString() != value2.ToString())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
