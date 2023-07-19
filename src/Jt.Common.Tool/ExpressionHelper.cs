using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Common.Tool
{
    public static class ExpressionHelper
    {
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
            BinaryExpression binaryExpression = Combine(member, constant, compare);
            return binaryExpression;
        }

        private static BinaryExpression Combine(Expression left, Expression right, EnumCompare compare)
        {
            switch (compare)
            {
                case EnumCompare.Equal:
                    return Expression.Equal(left, right);
                case EnumCompare.NotEqual:
                    return Expression.NotEqual(left, right);
                default:
                    return Expression.Equal(left, right);
            }
        }

        public enum EnumCompare
        {
            Equal,
            NotEqual
        }
    }
}
