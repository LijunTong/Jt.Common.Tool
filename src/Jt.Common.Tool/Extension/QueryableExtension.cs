using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jt.Common.Tool.Extension
{
    public static class QueryableExtension
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable">数据源</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public static (int Total, List<T> List) Pager<T>(this IQueryable<T> queryable, int pageIndex, int pageSize)
        {
            int total = queryable.Count();
            queryable = queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return (total, queryable.ToList());
        }
    }
}
