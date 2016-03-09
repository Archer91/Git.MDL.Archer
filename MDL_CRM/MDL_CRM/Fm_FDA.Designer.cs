namespace MDL_CRM
{
    partial class Fm_FDA
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.编辑toolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lst_fda = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.prod_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZPROD_FDAM_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Com_Fda = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.zprod_att_fda_qty = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.pcat_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_desc_chi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_createdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_lmodby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prod_lmoddate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.prod_code,
            this.ZPROD_FDAM_CODE,
            this.Com_Fda,
            this.zprod_att_fda_qty,
            this.pcat_code,
            this.prod_desc,
            this.prod_desc_chi,
            this.prod_createdate,
            this.prod_lmodby,
            this.prod_lmoddate});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(10, 91);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(822, 453);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编辑toolMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(95, 26);
            // 
            // 编辑toolMenuItem
            // 
            this.编辑toolMenuItem.Name = "编辑toolMenuItem";
            this.编辑toolMenuItem.Size = new System.Drawing.Size(94, 22);
            this.编辑toolMenuItem.Text = "编辑";
            this.编辑toolMenuItem.Click += new System.EventHandler(this.编辑toolMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "产品表信息(Product)";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(836, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "FDA列表信息";
            // 
            // lst_fda
            // 
            this.lst_fda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lst_fda.FormattingEnabled = true;
            this.lst_fda.HorizontalScrollbar = true;
            this.lst_fda.ItemHeight = 12;
            this.lst_fda.Location = new System.Drawing.Point(838, 91);
            this.lst_fda.Name = "lst_fda";
            this.lst_fda.Size = new System.Drawing.Size(202, 448);
            this.lst_fda.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1035, 55);
            this.panel1.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(420, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "保存(S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "请输入编号:";
            // 
            // txtCode
            // 
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Location = new System.Drawing.Point(98, 17);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(188, 21);
            this.txtCode.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(327, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 23);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "搜寻";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.button1_Click);
            // 
            // prod_code
            // 
            this.prod_code.DataPropertyName = "prod_code";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.prod_code.DefaultCellStyle = dataGridViewCellStyle2;
            this.prod_code.HeaderText = "手工编号";
            this.prod_code.Name = "prod_code";
            this.prod_code.ReadOnly = true;
            // 
            // ZPROD_FDAM_CODE
            // 
            this.ZPROD_FDAM_CODE.DataPropertyName = "ZPROD_FDAM_CODE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ZPROD_FDAM_CODE.DefaultCellStyle = dataGridViewCellStyle3;
            this.ZPROD_FDAM_CODE.HeaderText = "FDA_CODE";
            this.ZPROD_FDAM_CODE.Name = "ZPROD_FDAM_CODE";
            this.ZPROD_FDAM_CODE.Width = 80;
            // 
            // Com_Fda
            // 
            this.Com_Fda.DataPropertyName = "ZPROD_FDAM_CODE";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Com_Fda.DefaultCellStyle = dataGridViewCellStyle4;
            this.Com_Fda.HeaderText = "选择FDA";
            this.Com_Fda.Name = "Com_Fda";
            this.Com_Fda.Width = 300;
            // 
            // zprod_att_fda_qty
            // 
            this.zprod_att_fda_qty.DataPropertyName = "zprod_att_fda_qty";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.zprod_att_fda_qty.DefaultCellStyle = dataGridViewCellStyle5;
            this.zprod_att_fda_qty.HeaderText = "是否FDA带数量";
            this.zprod_att_fda_qty.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.zprod_att_fda_qty.Name = "zprod_att_fda_qty";
            this.zprod_att_fda_qty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.zprod_att_fda_qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.zprod_att_fda_qty.Width = 110;
            // 
            // pcat_code
            // 
            this.pcat_code.DataPropertyName = "pcat_code";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pcat_code.DefaultCellStyle = dataGridViewCellStyle6;
            this.pcat_code.HeaderText = "小类";
            this.pcat_code.Name = "pcat_code";
            this.pcat_code.ReadOnly = true;
            // 
            // prod_desc
            // 
            this.prod_desc.DataPropertyName = "prod_desc";
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.prod_desc.DefaultCellStyle = dataGridViewCellStyle7;
            this.prod_desc.HeaderText = "描述信息";
            this.prod_desc.Name = "prod_desc";
            this.prod_desc.ReadOnly = true;
            this.prod_desc.Width = 300;
            // 
            // prod_desc_chi
            // 
            this.prod_desc_chi.DataPropertyName = "prod_desc_chi";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.prod_desc_chi.DefaultCellStyle = dataGridViewCellStyle8;
            this.prod_desc_chi.HeaderText = "中文描述信息";
            this.prod_desc_chi.Name = "prod_desc_chi";
            this.prod_desc_chi.ReadOnly = true;
            this.prod_desc_chi.Width = 260;
            // 
            // prod_createdate
            // 
            this.prod_createdate.DataPropertyName = "prod_createdate";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.prod_createdate.DefaultCellStyle = dataGridViewCellStyle9;
            this.prod_createdate.HeaderText = "创建时间";
            this.prod_createdate.Name = "prod_createdate";
            this.prod_createdate.ReadOnly = true;
            this.prod_createdate.Width = 120;
            // 
            // prod_lmodby
            // 
            this.prod_lmodby.DataPropertyName = "prod_lmodby";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.prod_lmodby.DefaultCellStyle = dataGridViewCellStyle10;
            this.prod_lmodby.HeaderText = "修改人";
            this.prod_lmodby.Name = "prod_lmodby";
            this.prod_lmodby.ReadOnly = true;
            // 
            // prod_lmoddate
            // 
            this.prod_lmoddate.DataPropertyName = "prod_lmoddate";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.prod_lmoddate.DefaultCellStyle = dataGridViewCellStyle11;
            this.prod_lmoddate.HeaderText = "修改时间";
            this.prod_lmoddate.Name = "prod_lmoddate";
            this.prod_lmoddate.ReadOnly = true;
            this.prod_lmoddate.Width = 120;
            // 
            // Fm_FDA
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1037, 547);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lst_fda);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.KeyPreview = true;
            this.Name = "Fm_FDA";
            this.Text = "FDA维护";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Fm_FDA_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Fm_FDA_FormClosing);
            this.Load += new System.EventHandler(this.Fm_FDA_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Fm_FDA_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem 编辑toolMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lst_fda;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZPROD_FDAM_CODE;
        private System.Windows.Forms.DataGridViewComboBoxColumn Com_Fda;
        private System.Windows.Forms.DataGridViewComboBoxColumn zprod_att_fda_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn pcat_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_desc_chi;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_createdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_lmodby;
        private System.Windows.Forms.DataGridViewTextBoxColumn prod_lmoddate;
    }
}