using BLL;
using Models;
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

namespace WarehouseManageSystemUI
{
    public partial class InAndOutForm : Form
    {
        public InAndOutForm()
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

        private void InAndOutForm_Load(object sender, EventArgs e)
        {
            try
            {
                //调用 BLL 层获取所有物料数据
                MaterialManageBLL materialBll = new MaterialManageBLL();
                DataTable dtMaterials = materialBll.GetAllMaterial();

                //将 DataTable 绑定到 DataGridView
                dataGridView1.DataSource = dtMaterials;

                dataGridView1.Columns["MaterialCode"].HeaderText = "物料编号";
                dataGridView1.Columns["MaterialName"].HeaderText = "物料名称";
                dataGridView1.Columns["Specification"].HeaderText = "规格";
                dataGridView1.Columns["Unit"].HeaderText = "单位";
                dataGridView1.Columns["CurrentStock"].HeaderText = "当前储量";
                dataGridView1.Columns["MinStock"].HeaderText = "最小储量";
                dataGridView1.Columns["MaxStock"].HeaderText = "最大储量";
                dataGridView1.Columns["MaterialDescription"].HeaderText = "物料描述";
                dataGridView1.Columns["categoryId"].HeaderText = "物料分类";

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

                string name = Convert.ToString(row["MaterialName"]);
                string code = Convert.ToString(row["MaterialCode"]);
                string category = Convert.ToString(row["categoryId"]);
                string spec = Convert.ToString(row["Specification"]);
                string desc = Convert.ToString(row["MaterialDescription"]);
                string currentStock = Convert.ToString(row["CurrentStock"]);
                string maxStock = Convert.ToString(row["MaxStock"]);
                string minStock = Convert.ToString(row["MinStock"]);
                string uunit = Convert.ToString(row["Unit"]);


                label_name.Text = name;
                label_Code.Text = code;
                label_Category.Text = category;
                label_Spec.Text = spec;
                label_Desc.Text = desc;
                label_CurrentStock.Text = currentStock;
                label_MaxStock.Text = maxStock;
                label_Uint.Text = uunit;
                label_MinStock.Text = minStock;
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
                    label_Desc.Text = "空";
                    label_CurrentStock.Text = "空";
                    label_MaxStock.Text = "空";
                    label_Uint.Text = "空";
                    label_MinStock.Text = "空";
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
                MaterialManageBLL bll = new MaterialManageBLL();
                //获取最新数据
                dtAllData = bll.GetAllMaterial();
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                MaterialManageBLL bll = new MaterialManageBLL();
                //获取筛选后的数据
                dtAllData = bll.GetMaterialByName(name);
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

        private void button1_Click(object sender, EventArgs e)  //出库
        {
            try
            {
                Material material = new Material();
                material.MaterialCode = label_Code.Text;
                material.CurrentStock = Convert.ToDecimal(label_CurrentStock.Text) - Convert.ToDecimal(textBox2.Text);

                if (material.CurrentStock < 0)  //没那么多库存
                {
                    MessageBox.Show("库存不足！");
                    return;
                }

                if (material.CurrentStock < Convert.ToDecimal(label_MinStock.Text))  //MinStock验证
                {
                    MessageBox.Show("出库后数量低于最小数量！");
                    return;
                }

                if (Convert.ToDecimal(textBox2.Text) <= 0)
                {
                    MessageBox.Show("禁止输入负数！");
                    return;
                }

                MaterialManageBLL materialManageBLL = new MaterialManageBLL();
                bool result = materialManageBLL.MaterialStockInOut(material);
                if (result)
                {
                    MessageBox.Show("出库成功！");
                    //更新到日志
                    Logs logs = new Logs();
                    logs.MaterialName = label_name.Text;
                    logs.UserName = UserInformation.userInfo;
                    logs.OperationTime = DateTime.Now.ToString();
                    logs.OperationType = "出库";
                    LogsManageBLL logsManageBLL = new LogsManageBLL();
                    logsManageBLL.AddLogs(logs);
                    RefreshGrid();
                    textBox2.Text = string.Empty;

                    
                    
                }
                else
                {
                    MessageBox.Show("出库失败！");
                }
            }
            catch(Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("出库失败！你是不是输入非数字了？");
            }

            

        }

        private void button3_Click(object sender, EventArgs e)  //入库
        {
            try
            {
                Material material = new Material();
                material.MaterialCode = label_Code.Text;
                material.CurrentStock = Convert.ToDecimal(label_CurrentStock.Text) + Convert.ToDecimal(textBox3.Text);

                if (Convert.ToDecimal(textBox3.Text) <= 0)
                {
                    MessageBox.Show("禁止输入负数！");
                    return;
                }


                if (material.CurrentStock > Convert.ToDecimal(label_MaxStock.Text))  //MaxStock验证
                {
                    MessageBox.Show("仓库容量不足！");
                    return;
                }

                MaterialManageBLL materialManageBLL = new MaterialManageBLL();
                bool result = materialManageBLL.MaterialStockInOut(material);
                if (result)
                {
                    MessageBox.Show("入库成功！");
                    //更新到日志
                    Logs logs = new Logs();
                    logs.MaterialName = label_name.Text;
                    logs.UserName = UserInformation.userInfo;
                    logs.OperationTime = DateTime.Now.ToString();
                    logs.OperationType = "入库";
                    LogsManageBLL logsManageBLL = new LogsManageBLL();
                    logsManageBLL.AddLogs(logs);
                    RefreshGrid();
                    textBox3.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("入库失败！");
                }
            }
            catch(Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("入库失败！你是不是输入非数字了？");
            }

        }
    }
}
