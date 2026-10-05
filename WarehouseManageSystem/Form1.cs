using BLL;
using Models;
using System.Data;
using Util;
using WarehouseManageSystemUI;

namespace WarehouseManageSystem
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            try
            {
                InitializeComponent();
                UITheme.ApplyForm(this);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        UserManageBLL umb = new UserManageBLL();

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("请输入完整信息！");
                    return;
                }
                string un = textBox1.Text;
                string pwd = textBox2.Text;

                User user = new User();
                user.UserName = un;
                user.UserPassword = pwd;

                int result = umb.Login(user);
                if (result == 0)
                {
                    MessageBox.Show("账号或密码错误！");
                    return;
                }
                else if(result == 1) 
                {
                    MessageBox.Show("用户登录成功！");
                    UserInformation.userIdentity = "普通用户";
                    
                }
                else
                {
                    MessageBox.Show("管理员登录成功！");
                    UserInformation.userIdentity = "管理员";
                }

                UserInformation.userInfo = un;

                ////获取用户创建时间信息
                UserManageBLL userManageBLL = new UserManageBLL();
                DataTable dt = userManageBLL.GetUsersByName(un);
                UserInformation.CreateTime = Convert.ToDateTime(dt.Rows[0]["CreateTime"]);
                


                this.Hide();
                MainFrom mainfrom = new MainFrom();
                mainfrom.StartPosition = FormStartPosition.CenterScreen;
                mainfrom.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("登录失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                RegistFrom registFrom = new RegistFrom();
                registFrom.StartPosition = FormStartPosition.CenterParent;
                registFrom.ShowDialog();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("打开注册窗口失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
