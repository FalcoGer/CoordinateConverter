namespace CoordinateConverter.DCS.Tools
{
    partial class FormUnitImporter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUnitImporter));
            this.label1 = new System.Windows.Forms.Label();
            this.cb_CoalitionFilter = new System.Windows.Forms.ComboBox();
            this.cb_TypeFilter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_RadiusUnit = new System.Windows.Forms.ComboBox();
            this.cb_RadiusCenter = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_WithRadiusFilter = new System.Windows.Forms.CheckBox();
            this.dgv_Units = new System.Windows.Forms.DataGridView();
            this.dgvColId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColCoalition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColUnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColImport = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.nud_RadiusValue = new System.Windows.Forms.NumericUpDown();
            this.btn_ApplyFilter = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Units)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RadiusValue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Get units in";
            // 
            // cb_CoalitionFilter
            // 
            this.cb_CoalitionFilter.FormattingEnabled = true;
            this.cb_CoalitionFilter.Location = new System.Drawing.Point(81, 12);
            this.cb_CoalitionFilter.Name = "cb_CoalitionFilter";
            this.cb_CoalitionFilter.Size = new System.Drawing.Size(121, 21);
            this.cb_CoalitionFilter.TabIndex = 1;
            // 
            // cb_TypeFilter
            // 
            this.cb_TypeFilter.FormattingEnabled = true;
            this.cb_TypeFilter.Location = new System.Drawing.Point(298, 12);
            this.cb_TypeFilter.Name = "cb_TypeFilter";
            this.cb_TypeFilter.Size = new System.Drawing.Size(121, 21);
            this.cb_TypeFilter.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(208, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "coalition, of type";
            // 
            // cb_RadiusUnit
            // 
            this.cb_RadiusUnit.FormattingEnabled = true;
            this.cb_RadiusUnit.Location = new System.Drawing.Point(166, 39);
            this.cb_RadiusUnit.Name = "cb_RadiusUnit";
            this.cb_RadiusUnit.Size = new System.Drawing.Size(51, 21);
            this.cb_RadiusUnit.TabIndex = 5;
            // 
            // cb_RadiusCenter
            // 
            this.cb_RadiusCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_RadiusCenter.FormattingEnabled = true;
            this.cb_RadiusCenter.Location = new System.Drawing.Point(245, 39);
            this.cb_RadiusCenter.Name = "cb_RadiusCenter";
            this.cb_RadiusCenter.Size = new System.Drawing.Size(773, 21);
            this.cb_RadiusCenter.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(223, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "of";
            // 
            // cb_WithRadiusFilter
            // 
            this.cb_WithRadiusFilter.AutoSize = true;
            this.cb_WithRadiusFilter.Checked = true;
            this.cb_WithRadiusFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_WithRadiusFilter.Location = new System.Drawing.Point(19, 41);
            this.cb_WithRadiusFilter.Name = "cb_WithRadiusFilter";
            this.cb_WithRadiusFilter.Size = new System.Drawing.Size(56, 17);
            this.cb_WithRadiusFilter.TabIndex = 3;
            this.cb_WithRadiusFilter.Text = "Within";
            this.cb_WithRadiusFilter.UseVisualStyleBackColor = true;
            this.cb_WithRadiusFilter.CheckedChanged += new System.EventHandler(this.cb_WithRadiusFilter_CheckedChanged);
            // 
            // dgv_Units
            // 
            this.dgv_Units.AllowUserToAddRows = false;
            this.dgv_Units.AllowUserToDeleteRows = false;
            this.dgv_Units.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Units.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Units.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColId,
            this.dgvColCoalition,
            this.dgvColTypeName,
            this.dgvColClass,
            this.dgvColUnitName,
            this.dgvColPosition,
            this.dgvColImport});
            this.dgv_Units.Location = new System.Drawing.Point(12, 66);
            this.dgv_Units.Name = "dgv_Units";
            this.dgv_Units.ReadOnly = true;
            this.dgv_Units.Size = new System.Drawing.Size(1087, 354);
            this.dgv_Units.TabIndex = 8;
            this.dgv_Units.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Units_CellContentClick);
            this.dgv_Units.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgv_Units_KeyPress);
            // 
            // dgvColId
            // 
            this.dgvColId.Frozen = true;
            this.dgvColId.HeaderText = "ID";
            this.dgvColId.MinimumWidth = 60;
            this.dgvColId.Name = "dgvColId";
            this.dgvColId.ReadOnly = true;
            this.dgvColId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvColId.Width = 60;
            // 
            // dgvColCoalition
            // 
            this.dgvColCoalition.HeaderText = "Coalition";
            this.dgvColCoalition.MinimumWidth = 55;
            this.dgvColCoalition.Name = "dgvColCoalition";
            this.dgvColCoalition.ReadOnly = true;
            this.dgvColCoalition.Width = 55;
            // 
            // dgvColTypeName
            // 
            this.dgvColTypeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvColTypeName.FillWeight = 10F;
            this.dgvColTypeName.HeaderText = "Type";
            this.dgvColTypeName.MinimumWidth = 100;
            this.dgvColTypeName.Name = "dgvColTypeName";
            this.dgvColTypeName.ReadOnly = true;
            // 
            // dgvColClass
            // 
            this.dgvColClass.FillWeight = 30F;
            this.dgvColClass.HeaderText = "Class";
            this.dgvColClass.Name = "dgvColClass";
            this.dgvColClass.ReadOnly = true;
            // 
            // dgvColUnitName
            // 
            this.dgvColUnitName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvColUnitName.FillWeight = 15F;
            this.dgvColUnitName.HeaderText = "Unit/Group Name";
            this.dgvColUnitName.MinimumWidth = 50;
            this.dgvColUnitName.Name = "dgvColUnitName";
            this.dgvColUnitName.ReadOnly = true;
            // 
            // dgvColPosition
            // 
            this.dgvColPosition.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvColPosition.FillWeight = 25F;
            this.dgvColPosition.HeaderText = "Position";
            this.dgvColPosition.MinimumWidth = 170;
            this.dgvColPosition.Name = "dgvColPosition";
            this.dgvColPosition.ReadOnly = true;
            // 
            // dgvColImport
            // 
            this.dgvColImport.HeaderText = "Import";
            this.dgvColImport.MinimumWidth = 40;
            this.dgvColImport.Name = "dgvColImport";
            this.dgvColImport.ReadOnly = true;
            this.dgvColImport.Width = 40;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Refresh.Location = new System.Drawing.Point(12, 426);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_Refresh.TabIndex = 9;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(943, 426);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 10;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Import
            // 
            this.btn_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Import.Location = new System.Drawing.Point(1024, 426);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(75, 23);
            this.btn_Import.TabIndex = 11;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // nud_RadiusValue
            // 
            this.nud_RadiusValue.Location = new System.Drawing.Point(81, 40);
            this.nud_RadiusValue.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.nud_RadiusValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_RadiusValue.Name = "nud_RadiusValue";
            this.nud_RadiusValue.Size = new System.Drawing.Size(79, 20);
            this.nud_RadiusValue.TabIndex = 4;
            this.nud_RadiusValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btn_ApplyFilter
            // 
            this.btn_ApplyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ApplyFilter.Location = new System.Drawing.Point(1024, 37);
            this.btn_ApplyFilter.Name = "btn_ApplyFilter";
            this.btn_ApplyFilter.Size = new System.Drawing.Size(75, 23);
            this.btn_ApplyFilter.TabIndex = 7;
            this.btn_ApplyFilter.Text = "Apply";
            this.btn_ApplyFilter.UseVisualStyleBackColor = true;
            this.btn_ApplyFilter.Click += new System.EventHandler(this.btn_ApplyFilter_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 426);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(375, 26);
            this.label2.TabIndex = 18;
            this.label2.Text = "Refresh asks DCS for a new unit list. Use apply above to apply a filter change.\r\n" +
    "CTRL/Shift+Click to highlight multiple and use space to select/deselect.";
            // 
            // FormUnitImporter
            // 
            this.AcceptButton = this.btn_ApplyFilter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(1111, 461);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_ApplyFilter);
            this.Controls.Add(this.nud_RadiusValue);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.dgv_Units);
            this.Controls.Add(this.cb_WithRadiusFilter);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_RadiusCenter);
            this.Controls.Add(this.cb_RadiusUnit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cb_TypeFilter);
            this.Controls.Add(this.cb_CoalitionFilter);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormUnitImporter";
            this.Text = "Unit Import";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Units)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RadiusValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_CoalitionFilter;
        private System.Windows.Forms.ComboBox cb_TypeFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_RadiusUnit;
        private System.Windows.Forms.ComboBox cb_RadiusCenter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cb_WithRadiusFilter;
        private System.Windows.Forms.DataGridView dgv_Units;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.NumericUpDown nud_RadiusValue;
        private System.Windows.Forms.Button btn_ApplyFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColCoalition;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColUnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColPosition;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvColImport;
        private System.Windows.Forms.Label label2;
    }
}