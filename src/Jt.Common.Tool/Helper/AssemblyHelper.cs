using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Common.Tool.Helper
{
    public class AssemblyHelper
    {
        /// <summary>
        /// 获取程序集的所有方法
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns>MethodInfo</returns>
        public static List<MethodInfo> GetMethodInfos(Assembly assembly)
        {
            List<MethodInfo> methodInfos = new List<MethodInfo>();
            foreach (var type in assembly.GetTypes())
            {
                methodInfos.AddRange(GetMethodInfos(type));
            }
            return methodInfos;
        }

        /// <summary>
        /// 获取类型的所有方法
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>MethodInfo</returns>
        public static List<MethodInfo> GetMethodInfos(Type type)
        {
            return type.GetMethods().ToList();
        }

        /// <summary>
        /// 获取程序集存在指定特性的方法
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="attributeType">特性</param>
        /// <returns>MethodInfo</returns>
        public static List<MethodInfo> GetMethodInfosWithAttribute(Assembly assembly, Type attributeType)
        {
            List<MethodInfo> methodInfos = new List<MethodInfo>();
            foreach (var type in assembly.GetTypes())
            {
                methodInfos.AddRange(GetMethodInfosWithAttribute(type, attributeType));
            }
            return methodInfos;
        }

        /// <summary>
        /// 获取类型存在指定特性的方法
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="attributeType">特性</param>
        /// <returns></returns>
        public static List<MethodInfo> GetMethodInfosWithAttribute(Type type, Type attributeType)
        {
            return type.GetMethods().Where(x => x.CustomAttributes.Any(x => x.AttributeType == attributeType)).ToList();
        }

        /// <summary>
        /// 获取程序集继承了指定类型的子类的类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static List<Type> GetDerived(Assembly assembly, Type type)
        {
            return assembly.GetTypes().Where(x => x.BaseType == type).ToList();
        }

        /// <summary>
        /// 获取多个程序集继承了指定类型的子类的类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static List<Type> GetDerived(Assembly[] assembly, Type type)
        {
            var types = assembly.SelectMany(x => x.GetTypes()).Where(x => x.BaseType == type);
            return types.ToList();
        }

        /// <summary>
        /// 获取程序集存在指定特性的类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static List<Type> GetTypesByAttribute(Assembly assembly, Type type)
        {
            var types = assembly.GetTypes().Where(x => x.CustomAttributes.Any(a => a.AttributeType == type));
            return types.ToList();
        }

        /// <summary>
        /// 获取多个程序集存在指定特性的类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static List<Type> GetTypesByAttribute(Assembly[] assembly, Type type)
        {
            var types = assembly.SelectMany(x => x.GetTypes()).Where(x => x.CustomAttributes.Any(a => a.AttributeType == type));
            return types.ToList();
        }
    }
}
