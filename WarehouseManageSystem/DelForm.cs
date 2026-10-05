using BLL;
using Models1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Util;

namespace WarehouseManageSystemUI
{
    public partial class DelForm : Form
    {
        public DelForm()
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

        private void DelForm_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e) //点击删除
        {
            try
            {
                Material material = new Material();
                material.MaterialCode = label_Code.Text;

                DialogResult dr = MessageBox.Show($"确定要删除编号为 [{label_Code.Text}] 的物料吗？此操作不可恢复！", "确认删除",
                            MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question
                 );


                if (dr == DialogResult.Yes)
                {
                    MaterialManageBLL materialManageBLL = new MaterialManageBLL();
                    bool result = materialManageBLL.DeleteMaterial(material);
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
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("删除物料失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        //定义一个全局变量存储当前所有数据，方便后续筛选或重新绑定
        private DataTable dtAllData;
        private void RefreshGrid()  //删除后调用此方法刷新界面
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
    }
}
