using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Common.Tool.Helper
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// 构建表达式目录树
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="compare">比较符</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Expression Build<T>(string field, object value, EnumCompare compare)
        {
            Type type = typeof(T);
            ParameterExpression parameter = Expression.Parameter(type);
            ConstantExpression constant = Expression.Constant(value);
            MemberInfo memberInfo = type.GetProperty(field);
            if (memberInfo == null)
            {
                throw new Exception($"{type.Name}不包含成员{field}");
            }
            MemberExpression member = Expression.MakeMemberAccess(parameter, memberInfo);
            Expression binaryExpression = Combine(member, constant, compare);
            return binaryExpression;
        }

        private static Expression Combine(Expression left, Expression right, EnumCompare compare)
        {
            switch (compare)
            {
                case EnumCompare.Equal:
                    return Expression.Equal(left, right);
                case EnumCompare.NotEqual:
                    return Expression.NotEqual(left, right);
                case EnumCompare.Contain:
                    var contains = typeof(string).GetMethod("Contains");
                    return Expression.Call(left, contains, right);
                case EnumCompare.GreaterThan:
                    return Expression.GreaterThan(left, right);
                case EnumCompare.LessThan:
                    return Expression.LessThan(left, right);
                case EnumCompare.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(left, right);
                case EnumCompare.LessThanOrEqual:
                    return Expression.LessThanOrEqual(left, right);
                default:
                    return Expression.Equal(left, right);
            }
        }

        public enum EnumCompare
        {
            Equal,
            NotEqual,
            Contain,
            GreaterThan,
            LessThan,
            GreaterThanOrEqual,
            LessThanOrEqual,
        }
    }
}
