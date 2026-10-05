using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Util;

namespace WarehouseManageSystemUI
{
    public partial class RegistFrom : Form
    {
        public RegistFrom()
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

        private void label_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        UserManageBLL userManageBLL = new UserManageBLL();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("请输入完整信息！");
                    return;
                }

                if (textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show("前后密码不一致！");
                    return;
                }

                User user = new User();
                user.UserName = textBox1.Text;
                user.UserPassword = textBox2.Text;
                if (textBox4.Text == "miyao")
                {
                    user.IsAdmin = 1;
                }
                else
                {
                    user.IsAdmin = 0;
                }


                bool result = userManageBLL.Register(user);
                if (result)
                {
                    if (user.IsAdmin == 1)
                        MessageBox.Show("管理员注册成功！");
                    else
                        MessageBox.Show("用户注册成功！");
                }
                else
                {
                    MessageBox.Show("注册失败！用户已存在！");
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("注册失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
