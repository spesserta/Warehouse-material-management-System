using Models;
using Models1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Util;

namespace DAL
{
    public class LogsManageDAL
    {
        //新增操作日志
        public bool AddLogs(Logs logs)
        {
            try
            {
                string strSql = @"insert into LogsDB(UserName ,MaterialName , OperationType , OperationTime)
                              values(@UserName,@MaterialName,@OperationType,@OperationTime)";
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("@UserName", logs.UserName),
                    new SqlParameter("@MaterialName",logs.MaterialName),
                    new SqlParameter("@OperationType",logs.OperationType),
                    new SqlParameter("@OperationTime", logs.OperationTime)
                };
                return DBHelper.ExcuteCommand(strSql, sqlParameter);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 新增操作日志失败");
                return false;
            }
        }

        //获取操作日志列表
        public DataTable GetAllLogs()
        {
            try
            {
                string strSql = @"select * from LogsDB";
                return DBHelper.GetDataTable(strSql);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 获取操作日志列表失败");
                return null;
            }
        }



        //查询操作日志
        public DataTable GetLogsByName(string name)
        {
            try
            {
                string strSql = $"select * from LogsDB where UserName = '{name}'";
                return DBHelper.GetDataTable(strSql);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 按用户名查询操作日志失败");
                return null;
            }
        }

        public DataTable GetLogsByMaterial(string materialName)
        {
            try
            {
                string strSql = $"select * from LogsDB where MaterialName = '{materialName}'";
                return DBHelper.GetDataTable(strSql);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 按物料名称查询操作日志失败");
                return null;
            }
        }

        //清空操作日志
        public bool DeleteAllLogs()
        {
            try
            {
                string strSql = "truncate table logsDB";
                return DBHelper.ExcuteCommand(strSql);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 清空操作日志失败");
                return false;
            }
        }

    }
}
