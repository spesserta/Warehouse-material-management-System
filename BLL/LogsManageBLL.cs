using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Util;

namespace BLL
{
    public class LogsManageBLL
    {
        LogsManageDAL logsManageDAL = new LogsManageDAL();
        //新增操作日志
        public bool AddLogs(Logs logs)
        {
            try
            {
                return logsManageDAL.AddLogs(logs);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex,"BLL_ERROR: 新增操作日志失败");
                return false;
            }
        }

        //获取操作日志列表
        public DataTable GetAllLogs()
        {
            try
            {
                return logsManageDAL.GetAllLogs();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 获取操作日志列表失败");
                return null;
            }
        }



        //查询操作日志
        public DataTable GetLogsByName(string name)
        {
            try
            {
                return logsManageDAL.GetLogsByName(name);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 按用户名查询操作日志失败");
                return null;
            }
        }

        public DataTable GetLogsByMaterial(string materialName)
        {
            try
            {
                return logsManageDAL.GetLogsByMaterial(materialName);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 按物料名称查询操作日志失败");
                return null;
            }
        }

        //清空操作日志
        public bool DeleteAllLogs()
        {
            try
            {
                return logsManageDAL.DeleteAllLogs();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 清空操作日志失败");
                return false;
            }
        }
    }
}
