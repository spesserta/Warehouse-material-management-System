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
    public partial class guanyu : Form
    {
        public guanyu()
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

        private void label3_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }
    }
}
