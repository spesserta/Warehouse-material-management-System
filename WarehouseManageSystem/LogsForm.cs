using BLL;
using Models1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Util;
using WarehouseManageSystem;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WarehouseManageSystemUI
{
    public partial class LogsForm : Form
    {
        public LogsForm()
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

        private void LogsForm_Load(object sender, EventArgs e)
        {
            try
            {
                //调用 BLL 层获取所有操作数据
                LogsManageBLL logsManageBLL = new LogsManageBLL();
                DataTable dt = logsManageBLL.GetAllLogs();

                //将 DataTable 绑定到 DataGridView
                dataGridView1.DataSource = dt;

                dataGridView1.Columns["UserName"].HeaderText = "用户名";
                dataGridView1.Columns["MaterialName"].HeaderText = "物料名称";
                dataGridView1.Columns["OperationType"].HeaderText = "操作类型";
                dataGridView1.Columns["OperationTime"].HeaderText = "操作时间";

                //优化列显示：设置列宽、隐藏不需要的列等
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
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

                string username = Convert.ToString(row["UserName"]);
                string materialname = Convert.ToString(row["MaterialName"]);
                string operationTime = Convert.ToString(row["OperationType"]);
                string operationType = Convert.ToString(row["OperationTime"]);



                label_name.Text = username;
                label_Code.Text = materialname;
                label_Category.Text = operationTime;
                label_Spec.Text = operationType;
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }

        }

        //点击某格自动填充表单
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

                    label_name.Text = "空";
                    label_Code.Text = "空";
                    label_Category.Text = "空";
                    label_Spec.Text = "空";
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        //定义一个全局变量存储当前所有数据，方便后续筛选或重新绑定
        private DataTable dtAllData;
        private void RefreshGrid()  //修改后调用此方法刷新界面
        {
            try
            {
                LogsManageBLL bll = new LogsManageBLL();
                //获取最新数据
                dtAllData = bll.GetAllLogs();
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

        private void button2_Click(object sender, EventArgs e)  //用户名查询
        {
            try
            {
                string name = textBox1.Text;
                LogsManageBLL bll = new LogsManageBLL();
                //获取筛选后的数据
                dtAllData = bll.GetLogsByName(name);
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox2.Text;
                LogsManageBLL bll = new LogsManageBLL();
                //获取筛选后的数据
                dtAllData = bll.GetLogsByMaterial(name);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserInformation.userIdentity == "管理员")
                {
                    DialogResult dr = MessageBox.Show($"确定要删除编吗？此操作不可恢复！", "确认删除",
                           MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                    );


                    if (dr == DialogResult.Yes)
                    {
                        LogsManageBLL bll = new LogsManageBLL();
                        bll.DeleteAllLogs();
                        MessageBox.Show("删除成功！");
                        RefreshGrid();
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
                MessageBox.Show("清空日志失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
