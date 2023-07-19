using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jt.Common.Tool
{
    public class NameHelper
    {
        /// <summary>
        /// 下划线命名 user_name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ToUnderline(string name)
        {
            //将名称改为下划线命名规则
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            StringBuilder strTarget = new StringBuilder();
            strTarget.Append(name[0].ToString().ToLower());
            for (int j = 1; j < name.Length; j++)
            {
                string temp = name[j].ToString();
                if (Regex.IsMatch(temp, "[A-Z]"))
                {
                    temp = "_" + temp.ToLower();
                }
                strTarget.Append(temp);
            }
            return strTarget.ToString();
        }

        /// <summary>
        /// 驼峰命名 userName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ToCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            string[] strItems = name.Split('_');
            StringBuilder strTarget = new StringBuilder();
            for (int j = 0; j < strItems.Length; j++)
            {
                string temp = strItems[j];
                if (j == 0)
                {
                    strTarget.Append(temp);
                }
                else
                {
                    string temp2 = temp[0].ToString().ToUpper() + temp.Remove(0, 1);
                    strTarget.Append(temp2);
                }

            }
            return strTarget.ToString();
        }

        /// <summary>
        /// Pascal命名 UserName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ToPascal(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            string[] strItems = name.Split('_');
            StringBuilder strTarget = new StringBuilder();
            for (int j = 0; j < strItems.Length; j++)
            {
                string temp = strItems[j];
                string temp2 = temp[0].ToString().ToUpper() + temp.Remove(0, 1);
                strTarget.Append(temp2);
            }
            return strTarget.ToString();
        }
    }
}
