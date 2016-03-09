namespace MDL_CRM
{
    partial class Fm_EntitySecutity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fm_EntitySecutity));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.cmbEntitySecutity = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbRefreshSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCloseSet = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSite = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvEntity = new System.Windows.Forms.DataGridView();
            this.btnAddSite = new System.Windows.Forms.Button();
            this.btnAddEntity = new System.Windows.Forms.Button();
            this.txtSiteName = new System.Windows.Forms.TextBox();
            this.txtSiteCode = new System.Windows.Forms.TextBox();
            this.cmbEntity = new System.Windows.Forms.ComboBox();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.txtEntityCode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ccbSiteSecutity = new CheckComboBoxTest.CheckedComboBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbRefreshSecutity = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCloseSecutity = new System.Windows.Forms.ToolStripButton();
            this.btnAuthorize = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvEntityValue = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbEntityObjCode = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSite)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntity)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntityValue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "角色：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "公司：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "工厂：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(428, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "权限级别：";
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(117, 43);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(121, 20);
            this.cmbRole.TabIndex = 7;
            this.cmbRole.SelectedIndexChanged += new System.EventHandler(this.cmbRole_SelectedIndexChanged);
            // 
            // cmbEntitySecutity
            // 
            this.cmbEntitySecutity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntitySecutity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEntitySecutity.FormattingEnabled = true;
            this.cmbEntitySecutity.Location = new System.Drawing.Point(117, 69);
            this.cmbEntitySecutity.Name = "cmbEntitySecutity";
            this.cmbEntitySecutity.Size = new System.Drawing.Size(121, 20);
            this.cmbEntitySecutity.TabIndex = 4;
            this.cmbEntitySecutity.SelectedIndexChanged += new System.EventHandler(this.cmbEntitySecutity_SelectedIndexChanged);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cmbType.Location = new System.Drawing.Point(487, 43);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(52, 20);
            this.cmbType.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(717, 601);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage1.Controls.Add(this.cmbEntityObjCode);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.btnAuthorize);
            this.tabPage1.Controls.Add(this.cmbRole);
            this.tabPage1.Controls.Add(this.ccbSiteSecutity);
            this.tabPage1.Controls.Add(this.cmbEntitySecutity);
            this.tabPage1.Controls.Add(this.toolStrip2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cmbType);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(709, 575);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "安全控件授权";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage2.Controls.Add(this.toolStrip1);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.btnAddSite);
            this.tabPage2.Controls.Add(this.btnAddEntity);
            this.tabPage2.Controls.Add(this.txtSiteName);
            this.tabPage2.Controls.Add(this.txtSiteCode);
            this.tabPage2.Controls.Add(this.cmbEntity);
            this.tabPage2.Controls.Add(this.txtEntityName);
            this.tabPage2.Controls.Add(this.txtEntityCode);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(709, 575);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "安全控件配置";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefreshSet,
            this.toolStripSeparator1,
            this.tsbCloseSet});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(703, 25);
            this.toolStrip1.TabIndex = 16;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbRefreshSet
            // 
            this.tsbRefreshSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRefreshSet.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshSet.Image")));
            this.tsbRefreshSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshSet.Name = "tsbRefreshSet";
            this.tsbRefreshSet.Size = new System.Drawing.Size(36, 22);
            this.tsbRefreshSet.Text = "刷新";
            this.tsbRefreshSet.Click += new System.EventHandler(this.tsbRefreshSet_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCloseSet
            // 
            this.tsbCloseSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCloseSet.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseSet.Image")));
            this.tsbCloseSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseSet.Name = "tsbCloseSet";
            this.tsbCloseSet.Size = new System.Drawing.Size(36, 22);
            this.tsbCloseSet.Text = "关闭";
            this.tsbCloseSet.Click += new System.EventHandler(this.tsbCloseSet_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvSite);
            this.groupBox2.Location = new System.Drawing.Point(3, 343);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(703, 227);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工厂列表";
            // 
            // dgvSite
            // 
            this.dgvSite.AllowUserToAddRows = false;
            this.dgvSite.AllowUserToDeleteRows = false;
            this.dgvSite.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSite.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSite.Location = new System.Drawing.Point(3, 17);
            this.dgvSite.MultiSelect = false;
            this.dgvSite.Name = "dgvSite";
            this.dgvSite.ReadOnly = true;
            this.dgvSite.RowTemplate.Height = 23;
            this.dgvSite.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSite.Size = new System.Drawing.Size(697, 207);
            this.dgvSite.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvEntity);
            this.groupBox1.Location = new System.Drawing.Point(3, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(703, 239);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "公司列表";
            // 
            // dgvEntity
            // 
            this.dgvEntity.AllowUserToAddRows = false;
            this.dgvEntity.AllowUserToDeleteRows = false;
            this.dgvEntity.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEntity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEntity.Location = new System.Drawing.Point(3, 17);
            this.dgvEntity.MultiSelect = false;
            this.dgvEntity.Name = "dgvEntity";
            this.dgvEntity.ReadOnly = true;
            this.dgvEntity.RowTemplate.Height = 23;
            this.dgvEntity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEntity.Size = new System.Drawing.Size(697, 219);
            this.dgvEntity.TabIndex = 12;
            this.dgvEntity.SelectionChanged += new System.EventHandler(this.dgvEntity_SelectionChanged);
            // 
            // btnAddSite
            // 
            this.btnAddSite.Location = new System.Drawing.Point(552, 69);
            this.btnAddSite.Name = "btnAddSite";
            this.btnAddSite.Size = new System.Drawing.Size(75, 23);
            this.btnAddSite.TabIndex = 11;
            this.btnAddSite.Text = "添加工厂";
            this.btnAddSite.UseVisualStyleBackColor = true;
            this.btnAddSite.Click += new System.EventHandler(this.btnAddSite_Click);
            // 
            // btnAddEntity
            // 
            this.btnAddEntity.Location = new System.Drawing.Point(552, 37);
            this.btnAddEntity.Name = "btnAddEntity";
            this.btnAddEntity.Size = new System.Drawing.Size(75, 23);
            this.btnAddEntity.TabIndex = 10;
            this.btnAddEntity.Text = "添加公司";
            this.btnAddEntity.UseVisualStyleBackColor = true;
            this.btnAddEntity.Click += new System.EventHandler(this.btnAddEntity_Click);
            // 
            // txtSiteName
            // 
            this.txtSiteName.Location = new System.Drawing.Point(439, 71);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(100, 21);
            this.txtSiteName.TabIndex = 9;
            // 
            // txtSiteCode
            // 
            this.txtSiteCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSiteCode.Location = new System.Drawing.Point(262, 72);
            this.txtSiteCode.Name = "txtSiteCode";
            this.txtSiteCode.Size = new System.Drawing.Size(100, 21);
            this.txtSiteCode.TabIndex = 8;
            // 
            // cmbEntity
            // 
            this.cmbEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEntity.FormattingEnabled = true;
            this.cmbEntity.Location = new System.Drawing.Point(84, 72);
            this.cmbEntity.Name = "cmbEntity";
            this.cmbEntity.Size = new System.Drawing.Size(100, 20);
            this.cmbEntity.TabIndex = 7;
            // 
            // txtEntityName
            // 
            this.txtEntityName.Location = new System.Drawing.Point(439, 37);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(100, 21);
            this.txtEntityName.TabIndex = 6;
            // 
            // txtEntityCode
            // 
            this.txtEntityCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEntityCode.Location = new System.Drawing.Point(262, 37);
            this.txtEntityCode.Name = "txtEntityCode";
            this.txtEntityCode.Size = new System.Drawing.Size(100, 21);
            this.txtEntityCode.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(368, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "工厂名称：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(192, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "工厂编码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "公司：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(370, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "公司名称：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(191, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "公司编码：";
            // 
            // ccbSiteSecutity
            // 
            this.ccbSiteSecutity.CheckOnClick = true;
            this.ccbSiteSecutity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ccbSiteSecutity.DropDownHeight = 1;
            this.ccbSiteSecutity.FormattingEnabled = true;
            this.ccbSiteSecutity.IntegralHeight = false;
            this.ccbSiteSecutity.Location = new System.Drawing.Point(301, 69);
            this.ccbSiteSecutity.Name = "ccbSiteSecutity";
            this.ccbSiteSecutity.Size = new System.Drawing.Size(238, 22);
            this.ccbSiteSecutity.TabIndex = 5;
            this.ccbSiteSecutity.ValueSeparator = ", ";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefreshSecutity,
            this.toolStripSeparator2,
            this.tsbCloseSecutity});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(703, 25);
            this.toolStrip2.TabIndex = 10;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbRefreshSecutity
            // 
            this.tsbRefreshSecutity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRefreshSecutity.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshSecutity.Image")));
            this.tsbRefreshSecutity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshSecutity.Name = "tsbRefreshSecutity";
            this.tsbRefreshSecutity.Size = new System.Drawing.Size(36, 22);
            this.tsbRefreshSecutity.Text = "刷新";
            this.tsbRefreshSecutity.Click += new System.EventHandler(this.tsbRefreshSecutity_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbCloseSecutity
            // 
            this.tsbCloseSecutity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCloseSecutity.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseSecutity.Image")));
            this.tsbCloseSecutity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseSecutity.Name = "tsbCloseSecutity";
            this.tsbCloseSecutity.Size = new System.Drawing.Size(36, 22);
            this.tsbCloseSecutity.Text = "关闭";
            this.tsbCloseSecutity.Click += new System.EventHandler(this.tsbCloseSecutity_Click);
            // 
            // btnAuthorize
            // 
            this.btnAuthorize.Location = new System.Drawing.Point(552, 67);
            this.btnAuthorize.Name = "btnAuthorize";
            this.btnAuthorize.Size = new System.Drawing.Size(75, 23);
            this.btnAuthorize.TabIndex = 11;
            this.btnAuthorize.Text = "授权";
            this.btnAuthorize.UseVisualStyleBackColor = true;
            this.btnAuthorize.Click += new System.EventHandler(this.btnAuthorize_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvEntityValue);
            this.groupBox3.Location = new System.Drawing.Point(6, 105);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(697, 467);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "角色授权信息";
            // 
            // dgvEntityValue
            // 
            this.dgvEntityValue.AllowUserToAddRows = false;
            this.dgvEntityValue.AllowUserToDeleteRows = false;
            this.dgvEntityValue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEntityValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntityValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEntityValue.Location = new System.Drawing.Point(3, 17);
            this.dgvEntityValue.MultiSelect = false;
            this.dgvEntityValue.Name = "dgvEntityValue";
            this.dgvEntityValue.ReadOnly = true;
            this.dgvEntityValue.RowTemplate.Height = 23;
            this.dgvEntityValue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEntityValue.Size = new System.Drawing.Size(691, 447);
            this.dgvEntityValue.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(244, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "安全控件：";
            // 
            // cmbEntityObjCode
            // 
            this.cmbEntityObjCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntityObjCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEntityObjCode.FormattingEnabled = true;
            this.cmbEntityObjCode.Location = new System.Drawing.Point(301, 43);
            this.cmbEntityObjCode.Name = "cmbEntityObjCode";
            this.cmbEntityObjCode.Size = new System.Drawing.Size(121, 20);
            this.cmbEntityObjCode.TabIndex = 14;
            // 
            // Fm_EntitySecutity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 601);
            this.Controls.Add(this.tabControl1);
            this.Name = "Fm_EntitySecutity";
            this.Text = "安全控件授权";
            this.Load += new System.EventHandler(this.Fm_EntitySecutity_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSite)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntity)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntityValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.ComboBox cmbEntitySecutity;
        private CheckComboBoxTest.CheckedComboBox ccbSiteSecutity;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnAddSite;
        private System.Windows.Forms.Button btnAddEntity;
        private System.Windows.Forms.TextBox txtSiteName;
        private System.Windows.Forms.TextBox txtSiteCode;
        private System.Windows.Forms.ComboBox cmbEntity;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.TextBox txtEntityCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvSite;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvEntity;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbRefreshSet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbCloseSet;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbRefreshSecutity;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbCloseSecutity;
        private System.Windows.Forms.Button btnAuthorize;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvEntityValue;
        private System.Windows.Forms.ComboBox cmbEntityObjCode;
        private System.Windows.Forms.Label label10;

    }
}