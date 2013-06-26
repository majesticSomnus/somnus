using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Somnus.Common.Helper
{
    /// <summary>
    /// EVENT LOG 工具
    /// </summary>
    public class EventLogHelper
    {

        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="line">日志</param>
        public static void Info(string source, string line)
        {
            Log(source, line, EventLogEntryType.Information);
        }

        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="line">日志内容</param>
        public static void Info(string line)
        {
            Log(line, EventLogEntryType.Information);
        }


        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="line">日志内容</param>
        public static void Warring(string source, string line)
        {
            Log(source, line, EventLogEntryType.Warning);
        }


        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="line">日志内容</param>
        public static void Warring(string line)
        {
            Log(line, EventLogEntryType.Warning);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="sourc">源</param>
        /// <param name="line">日志信息</param>
        public static void Err(string sourc, string line)
        {
            Log(sourc, line, EventLogEntryType.Error);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="line">日志信息</param>
        public static void Err(string line)
        {
            Log(line, EventLogEntryType.Error);
        }

        /// <summary>
        /// 记录事件日志
        /// </summary>
        /// <param name="line">日志信息</param>
        /// <param name="type">日志级别</param>
        public static void Log(string line, EventLogEntryType type)
        {
            string source = "";
            Log(source, line, type);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="line">日志内容</param>
        /// <param name="type">类别</param>
        public static void Log(string source, string line, EventLogEntryType type)
        {
            try
            {

                if (!EventLog.SourceExists(source))
                {
                    EventLog.CreateEventSource(source, source);
                }
                EventLog aLog = new EventLog();

                aLog.Source = source;

                if (aLog.MaximumKilobytes < 16384)
                {
                    aLog.MaximumKilobytes = 16384;
                }
                if (aLog.OverflowAction != OverflowAction.OverwriteAsNeeded)
                {
                    ///EventLog 满了就把最早的那一笔log 删掉。
                    aLog.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 1);

                }

                if (!string.IsNullOrEmpty(line))
                    EventLog.WriteEntry(source, string.Format("[{0}] {1}", source, line), type);
                aLog.Close();

            }
            catch
            {

            }
        }

    }
}
