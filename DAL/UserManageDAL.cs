using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;
using Util;

namespace DAL
{
    public class UserManageDAL
    {
        //注册
        public bool Register(User user)
        {
            try
            {
                string SqlStr = @"insert into userDB(UserName , UserPassWord , isAdmin) values(@UserName , @UserPassword , @isAdmin)";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@UserName",user.UserName),
                    new SqlParameter("@UserPassword",user.UserPassword),
                    new SqlParameter("@isAdmin" ,user.IsAdmin)
                };
                return DBHelper.ExcuteCommand(SqlStr, param);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 用户注册失败");
                return false;
            }
        }


        //登录
        public int Login(User user)
        {
            try
            {
                string sqlstr = @"select UserName , isAdmin from userDB where UserName = @UserName and UserPassWord = @UserPassWord";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@UserName",user.UserName),
                    new SqlParameter("@UserPassWord",user.UserPassword)
                };
                DataTable dt = DBHelper.GetDataTable(sqlstr,param);
                if (dt.Rows.Count == 0)  //登录失败
                {
                    return 0;
                }

                DataRow dr = dt.Rows[0];
                if (Convert.ToInt32(dr["isAdmin"])== 0) //是普通用户
                {
                    return 1;
                }
                else  //是管理员
                {
                    return 2;
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 用户登录失败");
                return 0;
            }
        }



        //修改密码
        public bool UpdatePwd(User user)
        {
            try
            {
                string sqlStr = @"update userDB set UserPassWord = @UserPassWord where UserName = @UserName";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@UserName",user.UserName),
                    new SqlParameter("@UserPassWord",user.UserPassword)
                };
                return DBHelper.ExcuteCommand(sqlStr, param);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 修改密码失败");
                return false;
            }
        }


        //用户列表
        public DataTable AllUsersDAL()
        {
            try
            {
                string sqlStr = @"select  UserId, UserName, CreateTime, IsAdmin from userDB";
                return DBHelper.GetDataTable(sqlStr);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 获取用户列表失败");
                return null;
            }
        }

        //通过用户名来查找用户信息
        public DataTable SearchUserByName(string username)
        {
            try
            {
                string strsql = "select * from userDB where UserName = @name";

                SqlDataAdapter da = new SqlDataAdapter(strsql, DBHelper.connString);
                da.SelectCommand.Parameters.AddWithValue("@name", username);

                DataSet dt = new DataSet();
                da.Fill(dt);
                return dt.Tables[0];
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 按用户名查询用户失败");
                return null;
            }
        }


        //修改用户管理员权限
        public bool UpdateUserRole(User user)
        {
            try
            {
                string sqlStr = @"update userDB set isAdmin = @isAdmin where UserName = @UserName";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@UserName",user.UserName),
                    new SqlParameter("@isAdmin",user.IsAdmin)
                };
                return DBHelper.ExcuteCommand(sqlStr, param);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 修改用户权限失败");
                return false;
            }
        }


        //删除用户
        public bool DeleteUser(User user)
        {
            try
            {
                string strSql = @"delete  from userDB where UserName = @UserName";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@UserName",user.UserName),
                };
                return DBHelper.ExcuteCommand(strSql, param);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "DAL_ERROR: 删除用户失败");
                return false;
            }
        }

    }
}
