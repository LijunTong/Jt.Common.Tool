using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Common.Tool
{
    public class ObjectHelper
    {
        public static TOut ObjValueCopy<TIn, TOut>(TIn tin)
        {
            Type typeIn = typeof(TIn);
            Type typeOut = typeof(TOut);
            var tout = (TOut)Activator.CreateInstance(typeOut);
            foreach (var item in typeOut.GetProperties())
            {
                var propIn = typeIn.GetProperty(item.Name);
                if (propIn != null)
                {
                    item.SetValue(tout, propIn.GetValue(tin));
                }
            }
            return tout;
        }
        public static List<TOut> ObjsValueCopy<TIn, TOut>(List<TIn> tins)
        {
            Type typeIn = typeof(TIn);
            Type typeOut = typeof(TOut);
            List<TOut> outs = new List<TOut>();
            var outProps = typeOut.GetProperties();
            foreach (var item in tins)
            {
                var tout = (TOut)Activator.CreateInstance(typeOut);
                foreach (var outProp in outProps)
                {
                    var propIn = typeIn.GetProperty(outProp.Name);
                    if (propIn != null)
                    {
                        outProp.SetValue(tout, propIn.GetValue(item));
                    }
                }
                outs.Add(tout);
            }
            return outs;
        }
        public static T ObjDeepCopy<T>(T tin)
        {
            Type type = typeof(T);
            var tout = (T)Activator.CreateInstance(type);
            foreach (var item in type.GetProperties())
            {
                var propIn = type.GetProperty(item.Name);
                if (propIn != null)
                {
                    item.SetValue(tout, propIn.GetValue(tin));
                }
            }
            return tout;
        }

        public static T FillEmptyString<T>(T t)
        {
            Type type = t.GetType();
            foreach (var item in type.GetProperties())
            {
                if (item.PropertyType == typeof(string))
                {
                    var value = item.GetValue(t);
                    if (value == null)
                    {
                        item.SetValue(t, "");
                    }
                }
            }
            return t;
        }
    }
}
