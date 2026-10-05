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
    public partial class EditForm : Form
    {
        public EditForm()
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

        private void EditForm_Load(object sender, EventArgs e)
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

                textBox_Code.ReadOnly = true;

                //优化列显示：设置列宽、隐藏不需要的列等
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("加载数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //将选中的数据行填充到下方的textbox控件中
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


                textBox_name.Text = name;
                textBox_Code.Text = code;
                textBox_category.Text = category;
                textBox_Spec.Text = spec;
                textBox_desc.Text = desc;
                textBox_CurrentStock.Text = currentStock;
                textBox_MaxStock.Text = maxStock;
                textBox_Unit.Text = uunit;
                textBox_MinStock.Text = minStock;
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

                    textBox_name.Text = string.Empty;
                    textBox_Code.Text = string.Empty;
                    textBox_category.Text = string.Empty;
                    textBox_Spec.Text = string.Empty;
                    textBox_desc.Text = string.Empty;
                    textBox_CurrentStock.Text = string.Empty;
                    textBox_MaxStock.Text = string.Empty;
                    textBox_Unit.Text = string.Empty;
                    textBox_MinStock.Text = string.Empty;

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

        private void button2_Click(object sender, EventArgs e)  //查询
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

        private void button1_Click(object sender, EventArgs e)  //修改
        {
            try
            {
                Material material = new Material();
                material.MaterialName = textBox_name.Text;
                material.MaterialCode = textBox_Code.Text;
                material.CategoryId = textBox_category.Text;
                material.MaterialDescription = textBox_desc.Text;
                material.Specification = textBox_Spec.Text;
                material.CurrentStock = Convert.ToDecimal(textBox_CurrentStock.Text);
                material.MinStock = Convert.ToDecimal(textBox_MinStock.Text);
                material.MaxStock = Convert.ToDecimal(textBox_MaxStock.Text);
                material.Unit = textBox_Unit.Text;

                MaterialManageBLL materialManageBLL = new MaterialManageBLL();
                bool result = materialManageBLL.UpdateMaterial(material);
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
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
                MessageBox.Show("修改物料失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
