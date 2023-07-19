using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Common.Tool
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 获取时间戳Timestamp  
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="isMillisconds">是否是毫秒，默认为：true</param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dt, bool isMillisconds = true)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            long timeStamp = isMillisconds ? Convert.ToInt64((dt - dateStart).TotalMilliseconds) : Convert.ToInt64((dt - dateStart).TotalSeconds);
            return timeStamp;
        }

        /// <summary>
        /// 时间戳Timestamp转换成日期
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <param name="isMillisconds">是否是毫秒，默认为：true</param>
        /// <returns></returns>
        public static DateTime GetDateTime(string timeStamp, bool isMillisconds = true)
        {
            DateTime dtStart = new DateTime(1970, 1, 1, 8, 0, 0);
            long lTime = isMillisconds ? long.Parse(timeStamp) * 10000 : long.Parse(timeStamp) * 10000000;
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return targetDt;
        }
    }
}
