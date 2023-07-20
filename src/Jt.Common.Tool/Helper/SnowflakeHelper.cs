using System;

namespace Jt.Common.Tool.Helper
{
    /// <summary>
    /// 雪花算法生成唯一ID
    /// snowflake的结构如下(每部分用-分开):
    /// 0 - 0000000000 0000000000 0000000000 0000000000 0 - 00000 - 00000 - 000000000000
    /// 第一位为未使用，接下来的41位为毫秒级时间(41位的长度可以使用69年)，然后是5位datacenterId和5位workerId(10位的长度最多支持部署1024个节点） ，最后12位是毫秒内的计数（12位的计数顺序号支持每个节点每毫秒产生4096个ID序号）
    /// 一共加起来刚好64位，为一个Long型。(转换成字符串长度为18)
    /// snowflake生成的ID整体上按照时间自增排序，
    /// 并且整个分布式系统内不会产生ID碰撞（由datacenter和workerId作区分），
    /// 并且效率较高。据说：snowflake每秒能够产生26万个ID。
    /// </summary>
    public class SnowflakeHelper
    {
        /// <summary>
        /// 机器ID
        /// </summary>
        private static long workerId;

        /// <summary>
        /// 唯一时间，这是一个避免重复的随机量，自行设定不要大于当前时间戳
        /// </summary>
        private static long twepoch = 687888001020L;

        /// <summary>
        /// 
        /// </summary>
        private static long sequence = 0L;

        /// <summary>
        /// 机器码字节数。4个字节用来保存机器码(定义为Long类型会出现，最大偏移64位，所以左移64位没有意义)
        /// </summary>
        private static int workerIdBits = 4;

        /// <summary>
        /// 最大机器ID
        /// </summary>
        public static long maxWorkerId = -1L ^ -1L << workerIdBits;

        /// <summary>
        /// 计数器字节数，10个字节用来保存计数码
        /// </summary>
        private static int sequenceBits = 10;

        /// <summary>
        /// 机器码数据左移位数，就是后面计数器占用的位数
        /// </summary>
        private static int workerIdShift = sequenceBits;

        /// <summary>
        /// 时间戳左移动位数就是机器码和计数器总字节数
        /// </summary>
        private static int timestampLeftShift = sequenceBits + workerIdBits;

        /// <summary>
        /// 一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成
        /// </summary>
        public static long sequenceMask = -1L ^ -1L << sequenceBits;

        private long lastTimestamp = -1L;

        public SnowflakeHelper()
        {
            workerId = 1;
        }

        /// <summary>
        /// 机器码
        /// </summary>
        /// <param name="workerId"></param>
        public SnowflakeHelper(long workerId)
        {
            if (workerId > maxWorkerId || workerId < 0)
                throw new Exception(string.Format("worker Id can't be greater than {0} or less than 0 ", workerId));
            SnowflakeHelper.workerId = workerId;
        }

        /// <summary>
        /// 生成雪花id
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public long NextId()
        {
            lock (this)
            {
                long timestamp = TimeGen();
                if (lastTimestamp == timestamp)
                { //同一微妙中生成ID
                    sequence = sequence + 1 & sequenceMask; //用&运算计算该微秒内产生的计数是否已经到达上限
                    if (sequence == 0)
                    {
                        //一微妙内产生的ID计数已达上限，等待下一微妙
                        timestamp = TillNextMillis(lastTimestamp);
                    }
                }
                else
                { //不同微秒生成ID
                    sequence = 0; //计数清0
                }
                if (timestamp < lastTimestamp)
                {
                    //如果当前时间戳比上一次生成ID时时间戳还小，抛出异常，因为不能保证现在生成的ID之前没有生成过
                    throw new Exception(string.Format("Clock moved backwards.  Refusing to generate id for {0} milliseconds",
                        lastTimestamp - timestamp));
                }
                lastTimestamp = timestamp; //把当前时间戳保存为最后生成ID的时间戳
                long nextId = timestamp - twepoch << timestampLeftShift | workerId << workerIdShift | sequence;
                return nextId;
            }
        }

        // <summary>
        /// 获取下一微秒时间戳
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private long TillNextMillis(long lastTimestamp)
        {
            long timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        /// <summary>
        /// 生成当前时间戳
        /// </summary>
        /// <returns></returns>
        private long TimeGen()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}
