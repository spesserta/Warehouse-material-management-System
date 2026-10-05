using DAL;
using Models;
using System;
using System.Data;
using Util;

namespace BLL
{
    public class UserManageBLL
    {
        UserManageDAL userManage = new UserManageDAL();

        //注册
        public bool Register(User user)
        {
            try
            {
                return userManage.Register(user);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 用户注册失败");
                return false;
            }
        }

        //登录
        public int Login(User user)
        {
            try
            {
                return userManage.Login(user);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 用户登录失败");
                return 0;
            }
        }


        //修改密码
        public bool UpdatePwd(User user)
        {
            try
            {
                return userManage.UpdatePwd(user);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 修改密码失败");
                return false;
            }
        }

        //返回用户列表
        public DataTable AllUsersBLL()
        {
            try
            {
                return userManage.AllUsersDAL();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 获取用户列表失败");
                return null;
            }
        }

        //根据用户名进行查找
        public DataTable GetUsersByName(string name)
        {
            try
            {
                return userManage.SearchUserByName(name);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 按用户名查询用户失败");
                return null;
            }
        }

        //修改用户管理员权限
        public bool UpdateUserRole(User user)
        {
            try
            {
                return userManage.UpdateUserRole(user);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 修改用户权限失败");
                return false;
            }
        }


        //删除用户
        public bool DeleteUser(User user)
        {
            try
            {
                return userManage.DeleteUser(user);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex, "BLL_ERROR: 删除用户失败");
                return false;
            }
        }
    }
}
