using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Util;

namespace WarehouseManageSystemUI
{

    internal static class UITheme
    {
        // ---------------- 调色板 ----------------
        public static readonly Color Primary      = Color.FromArgb(47, 111, 237);    // 主蓝
        public static readonly Color PrimaryDark  = Color.FromArgb(36, 88, 196);     // 深蓝（悬停）
        public static readonly Color PrimaryLight = Color.FromArgb(224, 235, 255);   // 浅蓝（选中）
        public static readonly Color Navy         = Color.FromArgb(27, 44, 82);      // 深藏青（菜单栏/表头）
        public static readonly Color Background   = Color.FromArgb(243, 246, 251);   // 窗体背景
        public static readonly Color CardBg       = Color.White;                     // 卡片背景
        public static readonly Color Border       = Color.FromArgb(216, 223, 236);   // 边框
        public static readonly Color TextMain     = Color.FromArgb(46, 54, 68);      // 主文字
        public static readonly Color TextSub      = Color.FromArgb(128, 140, 160);   // 次要文字
        public static readonly Color Success      = Color.FromArgb(34, 167, 96);     // 入库/成功
        public static readonly Color Danger       = Color.FromArgb(224, 70, 70);     // 删除/危险
        public static readonly Color Warning      = Color.FromArgb(240, 147, 43);    // 出库/警告
        public static readonly Color FieldBg      = Color.White;
        public static readonly Color RowAlt       = Color.FromArgb(246, 248, 252);   // 表格隔行色

        // ---------------- 字体 ----------------
        public static Font BaseFont     => new Font("Microsoft YaHei UI", 9F);
        public static Font FieldFont    => new Font("Microsoft YaHei UI", 9.5F);
        public static Font BoldFont     => new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        public static Font TitleFont    => new Font("Microsoft YaHei UI", 15F, FontStyle.Bold);
        public static Font BigTitleFont => new Font("Microsoft YaHei UI", 20F, FontStyle.Bold);

        // 记录按钮的基准颜色，用于悬停还原
        private static readonly Dictionary<Button, Color> _btnBase = new Dictionary<Button, Color>();

        /// <summary>对整个窗体应用主题（在构造函数 InitializeComponent() 之后调用）。</summary>
        public static void ApplyForm(Form form)
        {
            try
            {
                form.BackColor = Background;
                form.ForeColor = TextMain;
                // 使用与系统默认 Segoe UI 等高的字体，避免触发额外自动缩放
                form.Font = BaseFont;
                ApplyControls(form);
                form.PerformLayout();
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        private static void ApplyControls(Control parent)
        {
            try
            {
                foreach (Control c in parent.Controls)
                {
                    if (c is MenuStrip menu)      StyleMenu(menu);
                    else if (c is Button btn)     StyleButton(btn);
                    else if (c is TextBox tb)     StyleTextBox(tb);
                    else if (c is ComboBox cb)    StyleComboBox(cb);
                    else if (c is DataGridView dgv) StyleGrid(dgv);
                    else if (c is Panel panel)    StylePanel(panel);
                    else if (c is Label lb)       StyleLabel(lb);
                    else if (c is PictureBox pb)  pb.BackColor = CardBg;
                    ApplyControls(c);
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        // ---------------- 按钮 ----------------
        private static void StyleButton(Button btn)
        {
            try
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.UseVisualStyleBackColor = false;
                btn.Font = FieldFont;
                btn.Cursor = Cursors.Hand;
                if (btn.Height < 32) btn.Height = 32;

                string t = btn.Text ?? "";
                Color baseColor, hoverColor, textColor = Color.White;

                if (t.Contains("删除") || t == "点我清空")
                {
                    baseColor = Danger; hoverColor = ControlPaint.Dark(Danger);
                }
                else if (t == "入库")
                {
                    baseColor = Success; hoverColor = ControlPaint.Dark(Success);
                }
                else if (t == "出库")
                {
                    baseColor = Warning; hoverColor = ControlPaint.Dark(Warning);
                }
                else if (t.Contains("清空") || t == "取消" || t == "返回" || t == "退出")
                {
                    baseColor = Color.FromArgb(233, 238, 246);
                    hoverColor = Color.FromArgb(220, 227, 238);
                    textColor = TextMain;
                }
                else
                {
                    baseColor = Primary; hoverColor = PrimaryDark;
                }

                btn.BackColor = baseColor;
                btn.ForeColor = textColor;
                _btnBase[btn] = baseColor;
                btn.MouseEnter += (s, e) => { if (_btnBase.TryGetValue(btn, out _)) btn.BackColor = hoverColor; };
                btn.MouseLeave += (s, e) => { if (_btnBase.TryGetValue(btn, out _)) btn.BackColor = _btnBase[btn]; };
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        // ---------------- 输入框 ----------------
        private static void StyleTextBox(TextBox tb)
        {
            try
            {
                tb.BorderStyle = BorderStyle.FixedSingle;
                tb.BackColor = FieldBg;
                tb.ForeColor = TextMain;
                tb.Font = FieldFont;
                if (!tb.Multiline && tb.Height < 30) tb.Height = 30;
                // 登录窗体的密码框显示掩码
                if (tb.Name == "textBox2" && tb.Parent != null && tb.Parent.Name == "Form1")
                    tb.PasswordChar = '●';
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        private static void StyleComboBox(ComboBox cb)
        {
            try
            {
                cb.FlatStyle = FlatStyle.Flat;
                cb.BackColor = FieldBg;
                cb.ForeColor = TextMain;
                cb.Font = FieldFont;
                if (cb.Height < 30) cb.Height = 30;
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        // ---------------- 数据表格 ----------------
        private static void StyleGrid(DataGridView dgv)
        {
            try
            {
                dgv.BackgroundColor = Color.White;
                dgv.BorderStyle = BorderStyle.None;
                dgv.EnableHeadersVisualStyles = false;
                dgv.RowHeadersVisible = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgv.ColumnHeadersDefaultCellStyle.BackColor = Navy;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.Font = BoldFont;
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.ColumnHeadersHeight = 38;
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                dgv.RowTemplate.Height = 34;
                dgv.RowsDefaultCellStyle.BackColor = Color.White;
                dgv.RowsDefaultCellStyle.ForeColor = TextMain;
                dgv.RowsDefaultCellStyle.SelectionBackColor = PrimaryLight;
                dgv.RowsDefaultCellStyle.SelectionForeColor = PrimaryDark;
                dgv.AlternatingRowsDefaultCellStyle.BackColor = RowAlt;
                dgv.GridColor = Color.FromArgb(230, 235, 243);
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        // ---------------- 面板（白色卡片 + 细边框） ----------------
        private static void StylePanel(Panel panel)
        {
            try
            {
                panel.BackColor = CardBg;
                panel.Paint += (s, e) =>
                {
                    ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle, Border, ButtonBorderStyle.Solid);
                };
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        // ---------------- 标签 ----------------
        private static void StyleLabel(Label lb)
        {
            try
            {
                Font f = lb.Font ?? BaseFont;
                // 大标题
                if (f.Size >= 18F)
                {
                    lb.Font = BigTitleFont;
                    lb.ForeColor = Primary;
                    return;
                }
                // 中标题
                if (f.Size >= 13F && f.Bold)
                {
                    lb.Font = TitleFont;
                    lb.ForeColor = Primary;
                    return;
                }
                // 必填红星等红色提示：保留红色，统一字体
                if (lb.ForeColor.ToArgb() == Color.FromArgb(192, 0, 0).ToArgb())
                {
                    lb.Font = new Font("Microsoft YaHei UI", 9.5F, FontStyle.Bold);
                    return;
                }
                // 设计时已指定强调色/次要色：保留颜色，统一字体
                if (lb.ForeColor.ToArgb() != SystemColors.ControlText.ToArgb())
                {
                    lb.Font = new Font("Microsoft YaHei UI", f.Bold ? 9.5F : 9F, f.Bold ? FontStyle.Bold : FontStyle.Regular);
                    return;
                }
                // 默认前景：统一主文字色
                lb.ForeColor = TextMain;
                lb.Font = new Font("Microsoft YaHei UI", f.Bold ? 9.5F : 9F, f.Bold ? FontStyle.Bold : FontStyle.Regular);
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        // ---------------- 菜单栏 ----------------
        private static void StyleMenu(MenuStrip menu)
        {
            try
            {
                menu.BackColor = Navy;
                menu.ForeColor = Color.White;
                menu.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
                menu.Padding = new Padding(12, 6, 12, 6);
                foreach (ToolStripItem item in menu.Items)
                {
                    StyleMenuItem(item, false);
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        private static void StyleMenuItem(ToolStripItem item, bool isDropDown)
        {
            try
            {
                item.Font = new Font("Microsoft YaHei UI", 9.5F);
                item.ForeColor = isDropDown ? TextMain : Color.White;
                if (item is ToolStripMenuItem mi)
                {
                    foreach (ToolStripItem sub in mi.DropDownItems)
                    {
                        StyleMenuItem(sub, true);
                    }
                }
            }
            catch (Exception ex)
            {
                utilLogManage.WriteLog(ex);
            }
        }

        private sealed class MenuColorTable : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin => Navy;
            public override Color MenuStripGradientEnd => Navy;
            public override Color MenuItemSelected => Primary;
            public override Color MenuItemSelectedGradientBegin => Primary;
            public override Color MenuItemSelectedGradientEnd => PrimaryDark;
            public override Color MenuItemBorder => Color.Transparent;
            public override Color ToolStripDropDownBackground => Color.White;
            public override Color ImageMarginGradientBegin => Color.White;
            public override Color ImageMarginGradientMiddle => Color.White;
            public override Color ImageMarginGradientEnd => Color.White;
            public override Color MenuBorder => Border;
            public override Color MenuItemPressedGradientBegin => PrimaryLight;
            public override Color MenuItemPressedGradientMiddle => PrimaryLight;
            public override Color MenuItemPressedGradientEnd => PrimaryLight;
            public override Color SeparatorDark => Border;
            public override Color SeparatorLight => Color.White;
        }
    }
}
