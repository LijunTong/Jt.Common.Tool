using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Jt.Common.Tool.Helper
{
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举的每一项
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string[] GetEnumItem(Type type)
        {
            if (!type.IsEnum)
            {
                throw new Exception("该类型不是enum");
            }
            return Enum.GetNames(type);
        }

        /// <summary>
        /// 获取枚举值上的描述
        /// </summary>
        /// <param name="t">枚举类型</param>
        /// <param name="val">枚举值</param>
        /// <returns>枚举元素上面的描述</returns>
        public static string GetEnumDesp(Type t, int val)
        {
            string value = "";

            if (t.IsEnum && Enum.IsDefined(t, val))
            {
                object enumObj = Enum.Parse(t, val.ToString());
                FieldInfo field = t.GetField(enumObj.ToString());

                if (field.IsDefined(typeof(DescriptionAttribute)))
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    value = attr.Description;
                }
            }

            return value;
        }

        /// <summary>
        /// 枚举类型转换为List
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回包含键(Key)、值(Value)、描述(Desp)的元素集合</returns>
        public static List<EnumKeyValue> EnumToList(Type t)
        {
            List<EnumKeyValue> enumList = new List<EnumKeyValue>();

            if (t.IsEnum)
            {
                foreach (var v in t.GetEnumValues())
                {
                    object obj = Enum.Parse(t, v.ToString());
                    enumList.Add(new EnumKeyValue
                    {
                        Key = obj.ToString(),
                        Value = (int)v,
                        Des = GetEnumDesp(t, (int)v)
                    });
                }
            }

            return enumList;
        }
    }

    public class EnumKeyValue
    {
        public string Key { get; set; }

        public int Value { get; set; }

        public string Des { get; set; }
    }
}
