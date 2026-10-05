

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Util
{

    public static class utilLogManage
    {
        
        private const string PreferredLogDirectory = @"D:\Logs";

        //写文件时加异步锁
        private static readonly object SyncRoot = new object();
        public static string LogDirectory { get; } = ResolveLogDirectory();
        public static string CurrentLogFile =>
            Path.Combine(LogDirectory, $"Log_{DateTime.Now:yyyy-MM-dd}.txt");


        public static void WriteLog(string message, [CallerMemberName] string methodName = "")
        {
            try
            {
                Append(BuildEntry("INFO", methodName, message, null, null));
            }
            catch
            {
                // 记录日志本身不抛出异常
            }
        }

        /// <summary>
        /// 记录一个异常。方法名会自动记录为调用者的方法名，extra 可附加自定义说明。
        /// </summary>
        public static void WriteLog(Exception ex, string? extra = null, [CallerMemberName] string methodName = "")
        {
            try
            {
                string message = ex?.Message ?? "未知异常";
                string detail = ex?.ToString() ?? string.Empty;
                Append(BuildEntry("ERROR", methodName, message, extra, detail));
            }
            catch
            {
                // 记录日志本身不抛出异常
            }
        }

        /// <summary>拼装一条日志文本，包含时间、级别、方法名、消息、堆栈等信息。</summary>
        private static string BuildEntry(string level, string methodName, string message, string? extra, string? detail)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(new string('=', 60));
                sb.AppendLine($"[时间] {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
                sb.AppendLine($"[级别] {level}");
                sb.AppendLine($"[方法] {(string.IsNullOrWhiteSpace(methodName) ? "未知" : methodName)}");
                if (!string.IsNullOrEmpty(extra))
                {
                    sb.AppendLine($"[说明] {extra}");
                }
                sb.AppendLine($"[消息] {message}");
                if (!string.IsNullOrEmpty(detail))
                {
                    sb.AppendLine($"[详情] {detail}");
                }
                sb.AppendLine($"[进程] {Environment.ProcessId}  [线程] {Environment.CurrentManagedThreadId}");
                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>把日志内容追加写入当天日志文件。</summary>
        private static void Append(string content)
        {
            try
            {
                lock (SyncRoot)
                {
                    Directory.CreateDirectory(LogDirectory);
                    File.AppendAllText(CurrentLogFile, content, new UTF8Encoding(false));
                }
            }
            catch
            {
                // 日志写入失败时不再向外抛出异常，避免影响主程序运行
            }
        }

        /// <summary>确定日志目录：优先 D:\Logs，不可用时依次降级，保证日志一定能落盘。</summary>
        private static string ResolveLogDirectory()
        {
            try
            {
                string[] candidates =
                {
                    PreferredLogDirectory,                                          // D:\Logs
                    Path.Combine(AppContext.BaseDirectory, "Logs"),                 // 程序目录\Logs
                    Path.Combine(Path.GetTempPath(), "WarehouseManageSystemLogs")  // 临时目录
                };

                foreach (string dir in candidates)
                {
                    try
                    {
                        Directory.CreateDirectory(dir);
                        return dir;
                    }
                    catch
                    {
                        
                    }
                }

                return AppContext.BaseDirectory;
            }
            catch
            {
                return AppContext.BaseDirectory;
            }
        }
    }
}
