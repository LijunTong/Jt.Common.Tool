using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jt.Common.Tool
{
    /// <summary>
    /// JSON格式相关数据的操作帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 通过序列化对象获取Json格式的字符串数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object objValue)
        {
            string strJson = JsonConvert.SerializeObject(objValue);
            return strJson;
        }

        /// <summary>
        /// 反序列化json数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string strJson)
        {
            return JsonConvert.DeserializeObject<T>(strJson);
        }
    }
}
