using BLL;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Util;
using WarehouseManageSystem;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WarehouseManageSystemUI
{
    public partial class userManageForm : Form
    {
        public userManageForm()
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

        private void userManageForm_Load(object sender, EventArgs e)
        {
            try
            {
                UserManageBLL userManageBLL = new UserManageBLL();
                DataTable allUsers = userManageBLL.AllUsersBLL();

                dataGridView1.DataSource = allUsers;
                dataGridView1.Columns["UserId"].HeaderText = "用户ID";
                dataGridView1.Columns["UserName"].HeaderText = "用户名";
                dataGridView1.Columns["CreateTime"].HeaderText = "创建时间";
                dataGridView1.Columns["isAdmin"].HeaderText = "是否为管理员";

                comboBox1_isAdmin.Items.Add("是");
                comboBox1_isAdmin.Items.Add("否");


            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("加载数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //将选中的数据行填充到下方的label控件中
        private void FillFormByRow(DataRow row)
        {
            try
            {
                if (row == null) return;

                string id = Convert.ToString(row["UserId"]);
                string name = Convert.ToString(row["UserName"]);
                string time = Convert.ToString(row["CreateTime"]);
                string isAdmin = Convert.ToString(row["isAdmin"]);

                label_id.Text = id;
                label_name.Text = name;
                label_time.Text = time;

                if (isAdmin == "1")
                {
                    comboBox1_isAdmin.Text = "是";
                }
                else
                {
                    comboBox1_isAdmin.Text = "否";
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }


        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // 判断是否有选中的行
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
                {
                    DataRowView drv = dataGridView1.CurrentRow.DataBoundItem as DataRowView;

                    if (drv != null)
                    {
                        FillFormByRow(drv.Row);
                    }
                }
                else
                {
                    label_id.Text = string.Empty;
                    label_name.Text = string.Empty;
                    label_time.Text = string.Empty;
                    comboBox1_isAdmin.Text = string.Empty;

                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }


        //刷新表格
        private DataTable dtAllData;
        private void RefreshGrid()  //删除后调用此方法刷新界面
        {
            try
            {
                UserManageBLL userManageBLL = new UserManageBLL();
                //获取最新数据
                DataTable users =  userManageBLL.AllUsersBLL();
                //绑定到 DataGridView 
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = users;
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("刷新数据失败：" + ex.Message);
            }
        }



        private void button3_Click(object sender, EventArgs e)  //更改权限
        {
            try
            {
                if (UserInformation.userIdentity == "管理员")
                {
                    User user = new User();
                    user.UserName = label_name.Text;
                    if (comboBox1_isAdmin.Text == "是")
                    {
                        user.IsAdmin = 1;
                    }
                    else
                    {
                        user.IsAdmin = 0;
                    }
                    UserManageBLL userManageBLL = new UserManageBLL();
                    bool result = userManageBLL.UpdateUserRole(user);
                    if (result)
                    {
                        MessageBox.Show("修改成功！");
                        RefreshGrid();
                    }
                    else
                    {
                        MessageBox.Show("修改失败！");
                    }
                }
                else
                {
                    MessageBox.Show("非管理员禁止操作！");
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("修改权限失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void button2_Click(object sender, EventArgs e)  //按名字查询
        {
            try
            {
                string name = label_name.Text;
                UserManageBLL bll = new UserManageBLL();

                //获取筛选后的数据
                dtAllData = bll.GetUsersByName(name);
                //绑定到 DataGridView 
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dtAllData;
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("刷新数据失败：" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)  //删除用户
        {
            try
            {
                if (UserInformation.userIdentity == "管理员")
                {
                    User user = new User();
                    user.UserName = label_name.Text;
                    UserManageBLL userManageBLL = new UserManageBLL();

                    if (comboBox1_isAdmin.Text == "是")
                    {
                        MessageBox.Show("不能删除管理员");
                    }
                    else
                    {
                        bool result = userManageBLL.DeleteUser(user);
                        if (result)
                        {
                            MessageBox.Show("删除成功！");
                            RefreshGrid();
                        }
                        else
                        {
                            MessageBox.Show("删除失败！");
                        }
                    }


                }
                else
                {
                    MessageBox.Show("非管理员禁止操作！");
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("删除用户失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
