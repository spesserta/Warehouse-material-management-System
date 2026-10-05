using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Util;
using WarehouseManageSystem;

namespace WarehouseManageSystemUI
{

    public partial class MainFrom : Form
    {
        public MainFrom()
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

        private void 物料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        private void 添加物料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserInformation.userIdentity == "管理员")
                {
                    AddForm addForm = new AddForm();
                    addForm.StartPosition = FormStartPosition.CenterScreen;
                    addForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("非管理员禁止操作！");
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("打开添加物料窗口失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void 删除物料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserInformation.userIdentity == "管理员")
                {
                    DelForm delForm = new DelForm();
                    delForm.StartPosition = FormStartPosition.CenterScreen;
                    delForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("非管理员禁止操作！");
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("打开删除物料窗口失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private System.Windows.Forms.Timer updataTimer;
        private void MainFrom_Load(object sender, EventArgs e)  //加载主窗体时顺便加载标签
        {
            try
            {
                label2.Text = UserInformation.userInfo;
                label3.Text = UserInformation.userIdentity;
                label8.Text = UserInformation.CreateTime.ToString();

                updataTimer = new System.Windows.Forms.Timer();
                updataTimer.Interval = 1000; //每秒间隔更新一次
                updataTimer.Tick += UpdateTimer_Tick;
                updataTimer.Start();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("加载主窗体失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                label6.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }


        private void 修改物料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserInformation.userIdentity == "管理员")
                {
                    EditForm editForm = new EditForm();
                    editForm.StartPosition = FormStartPosition.CenterScreen;
                    editForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("非管理员禁止操作！");
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("打开修改物料窗口失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SearchForm searchForm = new SearchForm();
                searchForm.StartPosition = FormStartPosition.CenterScreen;
                searchForm.ShowDialog();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("打开查询窗口失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                InAndOutForm inAndOutForm = new InAndOutForm();
                inAndOutForm.StartPosition = FormStartPosition.CenterScreen;
                inAndOutForm.ShowDialog();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("打开出入库窗口失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 推出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LogsForm logsForm = new LogsForm();
                logsForm.StartPosition = FormStartPosition.CenterScreen;
                logsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("打开日志窗口失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 个人信息管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                guanyu guanyu1 = new guanyu();
                guanyu1.StartPosition = FormStartPosition.CenterScreen;
                guanyu1.ShowDialog();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("打开个人信息窗口失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                userManageForm userManageForm = new userManageForm();
                userManageForm.StartPosition = FormStartPosition.CenterScreen;
                userManageForm.ShowDialog();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("打开用户管理窗口失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
