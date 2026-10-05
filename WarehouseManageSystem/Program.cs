using Util;

namespace WarehouseManageSystem
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // 捕获未处理异常并写入日志，避免程序直接崩溃且无法排查
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += (sender, e) => utilLogManage.WriteLog(e.Exception, "UI线程未处理异常");
                AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                {
                    if (e.ExceptionObject is Exception ex)
                    {
                        utilLogManage.WriteLog(ex, "未处理异常");
                    }
                    else
                    {
                        utilLogManage.WriteLog($"未处理异常：{e.ExceptionObject}");
                    }
                };

                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }
    }
    public static class UserInformation  //信息全窗体共享
    {
        public static string userInfo { get; set; }
        public static string userIdentity { get; set; }
        public static DateTime CreateTime {  get; set; }
    }
}
