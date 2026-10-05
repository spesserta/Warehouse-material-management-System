using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Util;

namespace DAL
{
    public class DBHelper
    {
        //数据库连接字符串
        public static string connString = @"Data Source=PC-20260526FYWZ\SQLEXPRESS;Initial Catalog=WarehouseManageSystemDB;Integrated Security=True";

        public static SqlConnection conn = new SqlConnection(connString);

        //查询语句
        public static DataTable GetDataTable(string SqlStr)  
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SqlStr, conn);
                SqlDataAdapter dapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dapt.Fill(dt);
                return dt;
            }catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                return null;

            }finally 
            { 
                conn.Close();
            }
        }



        //参数化查询select语句
        public static DataTable GetDataTable(string SqlStr , SqlParameter[] param)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SqlStr, conn);
                cmd.Parameters.AddRange(param);
                SqlDataAdapter dapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dapt.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                return null;
            }
            finally
            {
                conn.Close();
            }

        }



        //数据库的增删改
        public static bool ExcuteCommand(string SqlStr)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SqlStr, conn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool ExcuteCommand(string SqlStr , SqlParameter[] param)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand (SqlStr, conn);
                cmd.Parameters.AddRange(param);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                return false;
            }
            finally
            {
                conn.Close();
            }

        }



        

    }
}
