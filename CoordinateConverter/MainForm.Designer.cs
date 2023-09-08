using System;
using System.Windows.Forms;

namespace CoordinateConverter
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GrpBox_Output = new System.Windows.Forms.GroupBox();
            this.btn_Replace = new System.Windows.Forms.Button();
            this.btn_MoveUp = new System.Windows.Forms.Button();
            this.btn_MoveDown = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.nud_MGRS_Precision = new System.Windows.Forms.NumericUpDown();
            this.rb_Format_BE = new System.Windows.Forms.RadioButton();
            this.rb_Format_UTM = new System.Windows.Forms.RadioButton();
            this.rb_Format_MGRS = new System.Windows.Forms.RadioButton();
            this.rb_Format_LLDec = new System.Windows.Forms.RadioButton();
            this.rb_Format_LL = new System.Windows.Forms.RadioButton();
            this.btn_SetBE = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TB_Out_Bulls = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TB_Out_UTM = new System.Windows.Forms.TextBox();
            this.TB_Out_MGRS = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TB_Out_LL = new System.Windows.Forms.TextBox();
            this.TB_Out_LLDec = new System.Windows.Forms.TextBox();
            this.GrpBox_Input = new System.Windows.Forms.GroupBox();
            this.cb_altitudeIsAGL = new System.Windows.Forms.CheckBox();
            this.cb_pointOption = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cb_pointType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tb_Label = new System.Windows.Forms.TextBox();
            this.cb_AltitudeUnit = new System.Windows.Forms.ComboBox();
            this.tb_Altitude = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.TC_Input = new System.Windows.Forms.TabControl();
            this.TabPage_LatLon = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TB_LL_Lon = new System.Windows.Forms.TextBox();
            this.RB_LL_W = new System.Windows.Forms.RadioButton();
            this.RB_LL_E = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TB_LL_Lat = new System.Windows.Forms.TextBox();
            this.RB_LL_N = new System.Windows.Forms.RadioButton();
            this.RB_LL_S = new System.Windows.Forms.RadioButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TB_LLDec_Lon = new System.Windows.Forms.TextBox();
            this.RB_LLDec_W = new System.Windows.Forms.RadioButton();
            this.RB_LLDec_E = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TB_LLDec_Lat = new System.Windows.Forms.TextBox();
            this.RB_LLDec_N = new System.Windows.Forms.RadioButton();
            this.RB_LLDec_S = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.TB_MGRS_Digraph = new System.Windows.Forms.TextBox();
            this.TB_MGRS_Fraction = new System.Windows.Forms.TextBox();
            this.TB_MGRS_LatZone = new System.Windows.Forms.TextBox();
            this.TB_MGRS_LongZone = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.TB_UTM_LatZone = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TB_UTM_Easting = new System.Windows.Forms.TextBox();
            this.TB_UTM_Northing = new System.Windows.Forms.TextBox();
            this.TB_UTM_LongZone = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lbl_BEEasting = new System.Windows.Forms.Label();
            this.lbl_BENorthing = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Bulls_Range = new System.Windows.Forms.TextBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Bulls_Bearing = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_Error = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_DCS_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.pb_Transfer = new System.Windows.Forms.ToolStripProgressBar();
            this.dgv_CoordinateList = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAltitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXFer = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dCSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fetchF10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aircraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.a10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aH64PLTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aH64CPGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aV8BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.f15EPLTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.f15EWSOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.f16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.f18ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kA50ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m2000ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dCSMainScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reticleSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whenInF10MapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.neverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmr250ms = new System.Windows.Forms.Timer(this.components);
            this.cameraPositionModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terrainElevationUnderCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraAltitudeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GrpBox_Output.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_MGRS_Precision)).BeginInit();
            this.GrpBox_Input.SuspendLayout();
            this.TC_Input.SuspendLayout();
            this.TabPage_LatLon.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CoordinateList)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpBox_Output
            // 
            this.GrpBox_Output.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GrpBox_Output.Controls.Add(this.btn_Replace);
            this.GrpBox_Output.Controls.Add(this.btn_MoveUp);
            this.GrpBox_Output.Controls.Add(this.btn_MoveDown);
            this.GrpBox_Output.Controls.Add(this.label14);
            this.GrpBox_Output.Controls.Add(this.nud_MGRS_Precision);
            this.GrpBox_Output.Controls.Add(this.rb_Format_BE);
            this.GrpBox_Output.Controls.Add(this.rb_Format_UTM);
            this.GrpBox_Output.Controls.Add(this.rb_Format_MGRS);
            this.GrpBox_Output.Controls.Add(this.rb_Format_LLDec);
            this.GrpBox_Output.Controls.Add(this.rb_Format_LL);
            this.GrpBox_Output.Controls.Add(this.btn_SetBE);
            this.GrpBox_Output.Controls.Add(this.btn_Add);
            this.GrpBox_Output.Controls.Add(this.label12);
            this.GrpBox_Output.Controls.Add(this.label11);
            this.GrpBox_Output.Controls.Add(this.TB_Out_Bulls);
            this.GrpBox_Output.Controls.Add(this.label10);
            this.GrpBox_Output.Controls.Add(this.TB_Out_UTM);
            this.GrpBox_Output.Controls.Add(this.TB_Out_MGRS);
            this.GrpBox_Output.Controls.Add(this.label9);
            this.GrpBox_Output.Controls.Add(this.label8);
            this.GrpBox_Output.Controls.Add(this.TB_Out_LL);
            this.GrpBox_Output.Controls.Add(this.TB_Out_LLDec);
            this.GrpBox_Output.Location = new System.Drawing.Point(0, 298);
            this.GrpBox_Output.Name = "GrpBox_Output";
            this.GrpBox_Output.Size = new System.Drawing.Size(499, 183);
            this.GrpBox_Output.TabIndex = 2;
            this.GrpBox_Output.TabStop = false;
            this.GrpBox_Output.Text = "Output";
            // 
            // btn_Replace
            // 
            this.btn_Replace.Location = new System.Drawing.Point(87, 150);
            this.btn_Replace.Name = "btn_Replace";
            this.btn_Replace.Size = new System.Drawing.Size(75, 23);
            this.btn_Replace.TabIndex = 16;
            this.btn_Replace.Text = "Replace";
            this.btn_Replace.UseVisualStyleBackColor = true;
            this.btn_Replace.Click += new System.EventHandler(this.btn_Replace_Click);
            // 
            // btn_MoveUp
            // 
            this.btn_MoveUp.Location = new System.Drawing.Point(392, 152);
            this.btn_MoveUp.Name = "btn_MoveUp";
            this.btn_MoveUp.Size = new System.Drawing.Size(47, 23);
            this.btn_MoveUp.TabIndex = 19;
            this.btn_MoveUp.Text = "Up";
            this.btn_MoveUp.UseVisualStyleBackColor = true;
            this.btn_MoveUp.Click += new System.EventHandler(this.btn_MoveUp_Click);
            // 
            // btn_MoveDown
            // 
            this.btn_MoveDown.Location = new System.Drawing.Point(445, 152);
            this.btn_MoveDown.Name = "btn_MoveDown";
            this.btn_MoveDown.Size = new System.Drawing.Size(47, 23);
            this.btn_MoveDown.TabIndex = 20;
            this.btn_MoveDown.Text = "Down";
            this.btn_MoveDown.UseVisualStyleBackColor = true;
            this.btn_MoveDown.Click += new System.EventHandler(this.btn_MoveDown_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(249, 155);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "MGRS Precision:";
            // 
            // nud_MGRS_Precision
            // 
            this.nud_MGRS_Precision.Location = new System.Drawing.Point(343, 153);
            this.nud_MGRS_Precision.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nud_MGRS_Precision.Name = "nud_MGRS_Precision";
            this.nud_MGRS_Precision.Size = new System.Drawing.Size(27, 20);
            this.nud_MGRS_Precision.TabIndex = 18;
            this.nud_MGRS_Precision.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nud_MGRS_Precision.ValueChanged += new System.EventHandler(this.nud_MGRS_Precision_ValueChanged);
            // 
            // rb_Format_BE
            // 
            this.rb_Format_BE.AutoSize = true;
            this.rb_Format_BE.Location = new System.Drawing.Point(479, 127);
            this.rb_Format_BE.Name = "rb_Format_BE";
            this.rb_Format_BE.Size = new System.Drawing.Size(14, 13);
            this.rb_Format_BE.TabIndex = 14;
            this.rb_Format_BE.UseVisualStyleBackColor = true;
            this.rb_Format_BE.CheckedChanged += new System.EventHandler(this.rb_Format_BE_CheckedChanged);
            // 
            // rb_Format_UTM
            // 
            this.rb_Format_UTM.AutoSize = true;
            this.rb_Format_UTM.Location = new System.Drawing.Point(479, 101);
            this.rb_Format_UTM.Name = "rb_Format_UTM";
            this.rb_Format_UTM.Size = new System.Drawing.Size(14, 13);
            this.rb_Format_UTM.TabIndex = 13;
            this.rb_Format_UTM.UseVisualStyleBackColor = true;
            this.rb_Format_UTM.CheckedChanged += new System.EventHandler(this.rb_Format_UTM_CheckedChanged);
            // 
            // rb_Format_MGRS
            // 
            this.rb_Format_MGRS.AutoSize = true;
            this.rb_Format_MGRS.Location = new System.Drawing.Point(479, 75);
            this.rb_Format_MGRS.Name = "rb_Format_MGRS";
            this.rb_Format_MGRS.Size = new System.Drawing.Size(14, 13);
            this.rb_Format_MGRS.TabIndex = 12;
            this.rb_Format_MGRS.UseVisualStyleBackColor = true;
            this.rb_Format_MGRS.CheckedChanged += new System.EventHandler(this.rb_Format_MGRS_CheckedChanged);
            // 
            // rb_Format_LLDec
            // 
            this.rb_Format_LLDec.AutoSize = true;
            this.rb_Format_LLDec.Location = new System.Drawing.Point(479, 49);
            this.rb_Format_LLDec.Name = "rb_Format_LLDec";
            this.rb_Format_LLDec.Size = new System.Drawing.Size(14, 13);
            this.rb_Format_LLDec.TabIndex = 11;
            this.rb_Format_LLDec.UseVisualStyleBackColor = true;
            this.rb_Format_LLDec.CheckedChanged += new System.EventHandler(this.rb_Format_LLDec_CheckedChanged);
            // 
            // rb_Format_LL
            // 
            this.rb_Format_LL.AutoSize = true;
            this.rb_Format_LL.Checked = true;
            this.rb_Format_LL.Location = new System.Drawing.Point(479, 23);
            this.rb_Format_LL.Name = "rb_Format_LL";
            this.rb_Format_LL.Size = new System.Drawing.Size(14, 13);
            this.rb_Format_LL.TabIndex = 10;
            this.rb_Format_LL.TabStop = true;
            this.rb_Format_LL.UseVisualStyleBackColor = true;
            this.rb_Format_LL.CheckedChanged += new System.EventHandler(this.rb_Format_LL_CheckedChanged);
            // 
            // btn_SetBE
            // 
            this.btn_SetBE.Location = new System.Drawing.Point(168, 150);
            this.btn_SetBE.Name = "btn_SetBE";
            this.btn_SetBE.Size = new System.Drawing.Size(75, 23);
            this.btn_SetBE.TabIndex = 17;
            this.btn_SetBE.Text = "Set Bullseye";
            this.btn_SetBE.UseVisualStyleBackColor = true;
            this.btn_SetBE.Click += new System.EventHandler(this.btn_SetBE_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(6, 150);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 15;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "B/E";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 101);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "UTM";
            // 
            // TB_Out_Bulls
            // 
            this.TB_Out_Bulls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Out_Bulls.Location = new System.Drawing.Point(50, 124);
            this.TB_Out_Bulls.Name = "TB_Out_Bulls";
            this.TB_Out_Bulls.ReadOnly = true;
            this.TB_Out_Bulls.Size = new System.Drawing.Size(423, 20);
            this.TB_Out_Bulls.TabIndex = 4;
            this.TB_Out_Bulls.TabStop = false;
            this.TB_Out_Bulls.Text = "Enter Coordinates";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 7;
            this.label10.Text = "MGRS";
            // 
            // TB_Out_UTM
            // 
            this.TB_Out_UTM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Out_UTM.Location = new System.Drawing.Point(50, 98);
            this.TB_Out_UTM.Name = "TB_Out_UTM";
            this.TB_Out_UTM.ReadOnly = true;
            this.TB_Out_UTM.Size = new System.Drawing.Size(423, 20);
            this.TB_Out_UTM.TabIndex = 3;
            this.TB_Out_UTM.TabStop = false;
            this.TB_Out_UTM.Text = "Enter Coordinates";
            // 
            // TB_Out_MGRS
            // 
            this.TB_Out_MGRS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Out_MGRS.Location = new System.Drawing.Point(50, 72);
            this.TB_Out_MGRS.Name = "TB_Out_MGRS";
            this.TB_Out_MGRS.ReadOnly = true;
            this.TB_Out_MGRS.Size = new System.Drawing.Size(423, 20);
            this.TB_Out_MGRS.TabIndex = 2;
            this.TB_Out_MGRS.TabStop = false;
            this.TB_Out_MGRS.Text = "Enter Coordinates";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "L/L";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "L/L Dec";
            // 
            // TB_Out_LL
            // 
            this.TB_Out_LL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Out_LL.Location = new System.Drawing.Point(50, 20);
            this.TB_Out_LL.Name = "TB_Out_LL";
            this.TB_Out_LL.ReadOnly = true;
            this.TB_Out_LL.Size = new System.Drawing.Size(423, 20);
            this.TB_Out_LL.TabIndex = 0;
            this.TB_Out_LL.TabStop = false;
            this.TB_Out_LL.Text = "Enter Coordinates";
            // 
            // TB_Out_LLDec
            // 
            this.TB_Out_LLDec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Out_LLDec.Location = new System.Drawing.Point(50, 46);
            this.TB_Out_LLDec.Name = "TB_Out_LLDec";
            this.TB_Out_LLDec.ReadOnly = true;
            this.TB_Out_LLDec.Size = new System.Drawing.Size(423, 20);
            this.TB_Out_LLDec.TabIndex = 1;
            this.TB_Out_LLDec.TabStop = false;
            this.TB_Out_LLDec.Text = "Enter Coordinates";
            // 
            // GrpBox_Input
            // 
            this.GrpBox_Input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GrpBox_Input.Controls.Add(this.cb_altitudeIsAGL);
            this.GrpBox_Input.Controls.Add(this.cb_pointOption);
            this.GrpBox_Input.Controls.Add(this.label18);
            this.GrpBox_Input.Controls.Add(this.cb_pointType);
            this.GrpBox_Input.Controls.Add(this.label17);
            this.GrpBox_Input.Controls.Add(this.tb_Label);
            this.GrpBox_Input.Controls.Add(this.cb_AltitudeUnit);
            this.GrpBox_Input.Controls.Add(this.tb_Altitude);
            this.GrpBox_Input.Controls.Add(this.label16);
            this.GrpBox_Input.Controls.Add(this.label15);
            this.GrpBox_Input.Controls.Add(this.TC_Input);
            this.GrpBox_Input.Location = new System.Drawing.Point(12, 12);
            this.GrpBox_Input.Name = "GrpBox_Input";
            this.GrpBox_Input.Size = new System.Drawing.Size(487, 280);
            this.GrpBox_Input.TabIndex = 0;
            this.GrpBox_Input.TabStop = false;
            this.GrpBox_Input.Text = "Input";
            // 
            // cb_altitudeIsAGL
            // 
            this.cb_altitudeIsAGL.AutoSize = true;
            this.cb_altitudeIsAGL.Checked = true;
            this.cb_altitudeIsAGL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_altitudeIsAGL.Location = new System.Drawing.Point(199, 256);
            this.cb_altitudeIsAGL.Name = "cb_altitudeIsAGL";
            this.cb_altitudeIsAGL.Size = new System.Drawing.Size(47, 17);
            this.cb_altitudeIsAGL.TabIndex = 8;
            this.cb_altitudeIsAGL.Text = "AGL";
            this.cb_altitudeIsAGL.UseVisualStyleBackColor = true;
            this.cb_altitudeIsAGL.CheckedChanged += new System.EventHandler(this.cb_altitutudeIsAGL_CheckedChanged);
            // 
            // cb_pointOption
            // 
            this.cb_pointOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_pointOption.AutoCompleteCustomSource.AddRange(new string[] {
            "Waypoint"});
            this.cb_pointOption.Enabled = false;
            this.cb_pointOption.FormattingEnabled = true;
            this.cb_pointOption.Location = new System.Drawing.Point(308, 227);
            this.cb_pointOption.Name = "cb_pointOption";
            this.cb_pointOption.Size = new System.Drawing.Size(172, 21);
            this.cb_pointOption.TabIndex = 12;
            this.cb_pointOption.SelectedIndexChanged += new System.EventHandler(this.cb_PointOption_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(264, 230);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(38, 13);
            this.label18.TabIndex = 11;
            this.label18.Text = "Option";
            // 
            // cb_pointType
            // 
            this.cb_pointType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_pointType.AutoCompleteCustomSource.AddRange(new string[] {
            "Waypoint"});
            this.cb_pointType.Enabled = false;
            this.cb_pointType.FormattingEnabled = true;
            this.cb_pointType.Items.AddRange(new object[] {
            "Waypoint"});
            this.cb_pointType.Location = new System.Drawing.Point(79, 227);
            this.cb_pointType.Name = "cb_pointType";
            this.cb_pointType.Size = new System.Drawing.Size(179, 21);
            this.cb_pointType.TabIndex = 10;
            this.cb_pointType.Text = "Waypoint";
            this.cb_pointType.SelectedIndexChanged += new System.EventHandler(this.cb_pointType_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 230);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(58, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Point Type";
            // 
            // tb_Label
            // 
            this.tb_Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Label.Location = new System.Drawing.Point(308, 254);
            this.tb_Label.MaxLength = 12;
            this.tb_Label.Name = "tb_Label";
            this.tb_Label.Size = new System.Drawing.Size(172, 20);
            this.tb_Label.TabIndex = 9;
            this.tb_Label.TextChanged += new System.EventHandler(this.tb_Label_TextChanged);
            this.tb_Label.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Label_KeyPress);
            // 
            // cb_AltitudeUnit
            // 
            this.cb_AltitudeUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_AltitudeUnit.FormattingEnabled = true;
            this.cb_AltitudeUnit.Items.AddRange(new object[] {
            "ft",
            "m"});
            this.cb_AltitudeUnit.Location = new System.Drawing.Point(156, 254);
            this.cb_AltitudeUnit.Name = "cb_AltitudeUnit";
            this.cb_AltitudeUnit.Size = new System.Drawing.Size(36, 21);
            this.cb_AltitudeUnit.TabIndex = 7;
            this.cb_AltitudeUnit.Text = "ft";
            this.cb_AltitudeUnit.SelectedIndexChanged += new System.EventHandler(this.cb_AltitudeUnit_SelectedIndexChanged);
            // 
            // tb_Altitude
            // 
            this.tb_Altitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_Altitude.Location = new System.Drawing.Point(79, 254);
            this.tb_Altitude.MaxLength = 6;
            this.tb_Altitude.Name = "tb_Altitude";
            this.tb_Altitude.Size = new System.Drawing.Size(71, 20);
            this.tb_Altitude.TabIndex = 6;
            this.tb_Altitude.Text = "0";
            this.tb_Altitude.TextChanged += new System.EventHandler(this.tb_Altitude_TextChanged);
            this.tb_Altitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Altitude_KeyPress);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(269, 257);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Label";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(31, 257);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Altitude";
            // 
            // TC_Input
            // 
            this.TC_Input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TC_Input.Controls.Add(this.TabPage_LatLon);
            this.TC_Input.Controls.Add(this.tabPage1);
            this.TC_Input.Controls.Add(this.tabPage2);
            this.TC_Input.Controls.Add(this.tabPage3);
            this.TC_Input.Controls.Add(this.tabPage4);
            this.TC_Input.Location = new System.Drawing.Point(3, 16);
            this.TC_Input.Name = "TC_Input";
            this.TC_Input.SelectedIndex = 0;
            this.TC_Input.Size = new System.Drawing.Size(481, 205);
            this.TC_Input.TabIndex = 0;
            // 
            // TabPage_LatLon
            // 
            this.TabPage_LatLon.Controls.Add(this.label7);
            this.TabPage_LatLon.Controls.Add(this.panel1);
            this.TabPage_LatLon.Controls.Add(this.panel2);
            this.TabPage_LatLon.Location = new System.Drawing.Point(4, 22);
            this.TabPage_LatLon.Name = "TabPage_LatLon";
            this.TabPage_LatLon.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_LatLon.Size = new System.Drawing.Size(473, 179);
            this.TabPage_LatLon.TabIndex = 0;
            this.TabPage_LatLon.Text = "L/L";
            this.TabPage_LatLon.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(227, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Example: 57° 25\' 46.23\" N / 047° 55\' 42.52\" E";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.TB_LL_Lon);
            this.panel1.Controls.Add(this.RB_LL_W);
            this.panel1.Controls.Add(this.RB_LL_E);
            this.panel1.Location = new System.Drawing.Point(6, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 27);
            this.panel1.TabIndex = 1;
            // 
            // TB_LL_Lon
            // 
            this.TB_LL_Lon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_LL_Lon.Location = new System.Drawing.Point(3, 3);
            this.TB_LL_Lon.MaxLength = 16;
            this.TB_LL_Lon.Name = "TB_LL_Lon";
            this.TB_LL_Lon.Size = new System.Drawing.Size(375, 20);
            this.TB_LL_Lon.TabIndex = 2;
            this.TB_LL_Lon.TextChanged += new System.EventHandler(this.TB_LL_Lon_TextChanged);
            this.TB_LL_Lon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_KeyPress);
            this.TB_LL_Lon.Leave += new System.EventHandler(this.TB_Input_Leave);
            // 
            // RB_LL_W
            // 
            this.RB_LL_W.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LL_W.AutoSize = true;
            this.RB_LL_W.Location = new System.Drawing.Point(387, 4);
            this.RB_LL_W.Name = "RB_LL_W";
            this.RB_LL_W.Size = new System.Drawing.Size(36, 17);
            this.RB_LL_W.TabIndex = 5;
            this.RB_LL_W.Text = "W";
            this.RB_LL_W.UseVisualStyleBackColor = true;
            this.RB_LL_W.CheckedChanged += new System.EventHandler(this.RB_LL_CheckedChanged);
            // 
            // RB_LL_E
            // 
            this.RB_LL_E.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LL_E.AutoSize = true;
            this.RB_LL_E.Checked = true;
            this.RB_LL_E.Location = new System.Drawing.Point(426, 4);
            this.RB_LL_E.Name = "RB_LL_E";
            this.RB_LL_E.Size = new System.Drawing.Size(32, 17);
            this.RB_LL_E.TabIndex = 6;
            this.RB_LL_E.TabStop = true;
            this.RB_LL_E.Text = "E";
            this.RB_LL_E.UseVisualStyleBackColor = true;
            this.RB_LL_E.CheckedChanged += new System.EventHandler(this.RB_LL_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.TB_LL_Lat);
            this.panel2.Controls.Add(this.RB_LL_N);
            this.panel2.Controls.Add(this.RB_LL_S);
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(461, 27);
            this.panel2.TabIndex = 0;
            // 
            // TB_LL_Lat
            // 
            this.TB_LL_Lat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_LL_Lat.Location = new System.Drawing.Point(3, 3);
            this.TB_LL_Lat.MaxLength = 16;
            this.TB_LL_Lat.Name = "TB_LL_Lat";
            this.TB_LL_Lat.Size = new System.Drawing.Size(375, 20);
            this.TB_LL_Lat.TabIndex = 1;
            this.TB_LL_Lat.TextChanged += new System.EventHandler(this.TB_LL_Lat_TextChanged);
            this.TB_LL_Lat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_KeyPress);
            this.TB_LL_Lat.Leave += new System.EventHandler(this.TB_Input_Leave);
            // 
            // RB_LL_N
            // 
            this.RB_LL_N.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LL_N.AutoSize = true;
            this.RB_LL_N.Checked = true;
            this.RB_LL_N.Location = new System.Drawing.Point(387, 4);
            this.RB_LL_N.Name = "RB_LL_N";
            this.RB_LL_N.Size = new System.Drawing.Size(33, 17);
            this.RB_LL_N.TabIndex = 3;
            this.RB_LL_N.TabStop = true;
            this.RB_LL_N.Text = "N";
            this.RB_LL_N.UseVisualStyleBackColor = true;
            this.RB_LL_N.CheckedChanged += new System.EventHandler(this.RB_LL_CheckedChanged);
            // 
            // RB_LL_S
            // 
            this.RB_LL_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LL_S.AutoSize = true;
            this.RB_LL_S.Location = new System.Drawing.Point(426, 4);
            this.RB_LL_S.Name = "RB_LL_S";
            this.RB_LL_S.Size = new System.Drawing.Size(32, 17);
            this.RB_LL_S.TabIndex = 4;
            this.RB_LL_S.Text = "S";
            this.RB_LL_S.UseVisualStyleBackColor = true;
            this.RB_LL_S.CheckedChanged += new System.EventHandler(this.RB_LL_CheckedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(473, 179);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "L/L Decimal";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(211, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Example: 42° 07.9623\' N / 175° 45.0295\' E";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.TB_LLDec_Lon);
            this.panel3.Controls.Add(this.RB_LLDec_W);
            this.panel3.Controls.Add(this.RB_LLDec_E);
            this.panel3.Location = new System.Drawing.Point(6, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(464, 27);
            this.panel3.TabIndex = 1;
            // 
            // TB_LLDec_Lon
            // 
            this.TB_LLDec_Lon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_LLDec_Lon.Location = new System.Drawing.Point(3, 3);
            this.TB_LLDec_Lon.MaxLength = 16;
            this.TB_LLDec_Lon.Name = "TB_LLDec_Lon";
            this.TB_LLDec_Lon.Size = new System.Drawing.Size(378, 20);
            this.TB_LLDec_Lon.TabIndex = 2;
            this.TB_LLDec_Lon.TextChanged += new System.EventHandler(this.TB_LLDecimal_Lon_TextChanged);
            this.TB_LLDec_Lon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_Decimal_KeyPress);
            this.TB_LLDec_Lon.Leave += new System.EventHandler(this.TB_Input_Leave);
            // 
            // RB_LLDec_W
            // 
            this.RB_LLDec_W.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LLDec_W.AutoSize = true;
            this.RB_LLDec_W.Location = new System.Drawing.Point(387, 4);
            this.RB_LLDec_W.Name = "RB_LLDec_W";
            this.RB_LLDec_W.Size = new System.Drawing.Size(36, 17);
            this.RB_LLDec_W.TabIndex = 5;
            this.RB_LLDec_W.Text = "W";
            this.RB_LLDec_W.UseVisualStyleBackColor = true;
            this.RB_LLDec_W.CheckedChanged += new System.EventHandler(this.RB_LLDecimal_CheckedChanged);
            // 
            // RB_LLDec_E
            // 
            this.RB_LLDec_E.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LLDec_E.AutoSize = true;
            this.RB_LLDec_E.Checked = true;
            this.RB_LLDec_E.Location = new System.Drawing.Point(429, 4);
            this.RB_LLDec_E.Name = "RB_LLDec_E";
            this.RB_LLDec_E.Size = new System.Drawing.Size(32, 17);
            this.RB_LLDec_E.TabIndex = 6;
            this.RB_LLDec_E.TabStop = true;
            this.RB_LLDec_E.Text = "E";
            this.RB_LLDec_E.UseVisualStyleBackColor = true;
            this.RB_LLDec_E.CheckedChanged += new System.EventHandler(this.RB_LLDecimal_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.TB_LLDec_Lat);
            this.panel4.Controls.Add(this.RB_LLDec_N);
            this.panel4.Controls.Add(this.RB_LLDec_S);
            this.panel4.Location = new System.Drawing.Point(6, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(464, 27);
            this.panel4.TabIndex = 0;
            // 
            // TB_LLDec_Lat
            // 
            this.TB_LLDec_Lat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_LLDec_Lat.Location = new System.Drawing.Point(3, 3);
            this.TB_LLDec_Lat.MaxLength = 16;
            this.TB_LLDec_Lat.Name = "TB_LLDec_Lat";
            this.TB_LLDec_Lat.Size = new System.Drawing.Size(378, 20);
            this.TB_LLDec_Lat.TabIndex = 1;
            this.TB_LLDec_Lat.TextChanged += new System.EventHandler(this.TB_LLDecimal_Lat_TextChanged);
            this.TB_LLDec_Lat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_Decimal_KeyPress);
            this.TB_LLDec_Lat.Leave += new System.EventHandler(this.TB_Input_Leave);
            // 
            // RB_LLDec_N
            // 
            this.RB_LLDec_N.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LLDec_N.AutoSize = true;
            this.RB_LLDec_N.Checked = true;
            this.RB_LLDec_N.Location = new System.Drawing.Point(387, 4);
            this.RB_LLDec_N.Name = "RB_LLDec_N";
            this.RB_LLDec_N.Size = new System.Drawing.Size(33, 17);
            this.RB_LLDec_N.TabIndex = 3;
            this.RB_LLDec_N.TabStop = true;
            this.RB_LLDec_N.Text = "N";
            this.RB_LLDec_N.UseVisualStyleBackColor = true;
            this.RB_LLDec_N.CheckedChanged += new System.EventHandler(this.RB_LLDecimal_CheckedChanged);
            // 
            // RB_LLDec_S
            // 
            this.RB_LLDec_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LLDec_S.AutoSize = true;
            this.RB_LLDec_S.Location = new System.Drawing.Point(429, 4);
            this.RB_LLDec_S.Name = "RB_LLDec_S";
            this.RB_LLDec_S.Size = new System.Drawing.Size(32, 17);
            this.RB_LLDec_S.TabIndex = 4;
            this.RB_LLDec_S.Text = "S";
            this.RB_LLDec_S.UseVisualStyleBackColor = true;
            this.RB_LLDec_S.CheckedChanged += new System.EventHandler(this.RB_LLDecimal_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(473, 179);
            this.tabPage2.TabIndex = 6;
            this.tabPage2.Text = "MGRS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox7
            // 
            this.textBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox7.Location = new System.Drawing.Point(3, 60);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(467, 111);
            this.textBox7.TabIndex = 2;
            this.textBox7.TabStop = false;
            this.textBox7.Text = resources.GetString("textBox7.Text");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Example: 37 T GG 433245";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.TB_MGRS_Digraph);
            this.panel5.Controls.Add(this.TB_MGRS_Fraction);
            this.panel5.Controls.Add(this.TB_MGRS_LatZone);
            this.panel5.Controls.Add(this.TB_MGRS_LongZone);
            this.panel5.Location = new System.Drawing.Point(3, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(467, 35);
            this.panel5.TabIndex = 0;
            // 
            // TB_MGRS_Digraph
            // 
            this.TB_MGRS_Digraph.Location = new System.Drawing.Point(80, 8);
            this.TB_MGRS_Digraph.MaxLength = 2;
            this.TB_MGRS_Digraph.Name = "TB_MGRS_Digraph";
            this.TB_MGRS_Digraph.Size = new System.Drawing.Size(34, 20);
            this.TB_MGRS_Digraph.TabIndex = 3;
            this.TB_MGRS_Digraph.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.TB_MGRS_Digraph.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_MGRS_Digraph_KeyPress);
            // 
            // TB_MGRS_Fraction
            // 
            this.TB_MGRS_Fraction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_MGRS_Fraction.Location = new System.Drawing.Point(120, 8);
            this.TB_MGRS_Fraction.MaxLength = 11;
            this.TB_MGRS_Fraction.Name = "TB_MGRS_Fraction";
            this.TB_MGRS_Fraction.Size = new System.Drawing.Size(344, 20);
            this.TB_MGRS_Fraction.TabIndex = 4;
            this.TB_MGRS_Fraction.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.TB_MGRS_Fraction.Enter += new System.EventHandler(this.TB_MGRS_Fraction_Enter);
            this.TB_MGRS_Fraction.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_MGRS_Fraction_KeyPress);
            this.TB_MGRS_Fraction.Leave += new System.EventHandler(this.TB_MGRS_Fraction_Leave);
            // 
            // TB_MGRS_LatZone
            // 
            this.TB_MGRS_LatZone.Location = new System.Drawing.Point(40, 8);
            this.TB_MGRS_LatZone.MaxLength = 1;
            this.TB_MGRS_LatZone.Name = "TB_MGRS_LatZone";
            this.TB_MGRS_LatZone.Size = new System.Drawing.Size(34, 20);
            this.TB_MGRS_LatZone.TabIndex = 2;
            this.TB_MGRS_LatZone.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.TB_MGRS_LatZone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_LatZone_KeyPress);
            // 
            // TB_MGRS_LongZone
            // 
            this.TB_MGRS_LongZone.Location = new System.Drawing.Point(3, 8);
            this.TB_MGRS_LongZone.MaxLength = 2;
            this.TB_MGRS_LongZone.Name = "TB_MGRS_LongZone";
            this.TB_MGRS_LongZone.Size = new System.Drawing.Size(34, 20);
            this.TB_MGRS_LongZone.TabIndex = 1;
            this.TB_MGRS_LongZone.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.TB_MGRS_LongZone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_LongZone_KeyPress);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox9);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.panel8);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(473, 179);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "UTM";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox9
            // 
            this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox9.Location = new System.Drawing.Point(3, 60);
            this.textBox9.Multiline = true;
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(464, 111);
            this.textBox9.TabIndex = 2;
            this.textBox9.TabStop = false;
            this.textBox9.Text = resources.GetString("textBox9.Text");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(201, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Example: 37 T 377298.745 1483034.794";
            // 
            // panel8
            // 
            this.panel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel8.Controls.Add(this.TB_UTM_LatZone);
            this.panel8.Controls.Add(this.tableLayoutPanel1);
            this.panel8.Controls.Add(this.TB_UTM_LongZone);
            this.panel8.Location = new System.Drawing.Point(3, 6);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(464, 35);
            this.panel8.TabIndex = 0;
            // 
            // TB_UTM_LatZone
            // 
            this.TB_UTM_LatZone.Location = new System.Drawing.Point(40, 8);
            this.TB_UTM_LatZone.MaxLength = 1;
            this.TB_UTM_LatZone.Name = "TB_UTM_LatZone";
            this.TB_UTM_LatZone.Size = new System.Drawing.Size(34, 20);
            this.TB_UTM_LatZone.TabIndex = 2;
            this.TB_UTM_LatZone.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.TB_UTM_LatZone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_LatZone_KeyPress);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.TB_UTM_Easting, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TB_UTM_Northing, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(80, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(378, 26);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // TB_UTM_Easting
            // 
            this.TB_UTM_Easting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_UTM_Easting.Location = new System.Drawing.Point(3, 3);
            this.TB_UTM_Easting.MaxLength = 16;
            this.TB_UTM_Easting.Name = "TB_UTM_Easting";
            this.TB_UTM_Easting.Size = new System.Drawing.Size(183, 20);
            this.TB_UTM_Easting.TabIndex = 3;
            this.TB_UTM_Easting.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.TB_UTM_Easting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RB_UTM_Northing_Easting_KeyPress);
            this.TB_UTM_Easting.Leave += new System.EventHandler(this.TB_Input_Leave);
            // 
            // TB_UTM_Northing
            // 
            this.TB_UTM_Northing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_UTM_Northing.Location = new System.Drawing.Point(192, 3);
            this.TB_UTM_Northing.MaxLength = 16;
            this.TB_UTM_Northing.Name = "TB_UTM_Northing";
            this.TB_UTM_Northing.Size = new System.Drawing.Size(183, 20);
            this.TB_UTM_Northing.TabIndex = 4;
            this.TB_UTM_Northing.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.TB_UTM_Northing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RB_UTM_Northing_Easting_KeyPress);
            this.TB_UTM_Northing.Leave += new System.EventHandler(this.TB_Input_Leave);
            // 
            // TB_UTM_LongZone
            // 
            this.TB_UTM_LongZone.Location = new System.Drawing.Point(3, 8);
            this.TB_UTM_LongZone.MaxLength = 2;
            this.TB_UTM_LongZone.Name = "TB_UTM_LongZone";
            this.TB_UTM_LongZone.Size = new System.Drawing.Size(34, 20);
            this.TB_UTM_LongZone.TabIndex = 1;
            this.TB_UTM_LongZone.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.TB_UTM_LongZone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_LongZone_KeyPress);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lbl_BEEasting);
            this.tabPage4.Controls.Add(this.lbl_BENorthing);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.panel9);
            this.tabPage4.Controls.Add(this.panel10);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(473, 179);
            this.tabPage4.TabIndex = 5;
            this.tabPage4.Text = "Bullseye";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lbl_BEEasting
            // 
            this.lbl_BEEasting.AutoSize = true;
            this.lbl_BEEasting.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.lbl_BEEasting.Location = new System.Drawing.Point(7, 115);
            this.lbl_BEEasting.Name = "lbl_BEEasting";
            this.lbl_BEEasting.Size = new System.Drawing.Size(63, 16);
            this.lbl_BEEasting.TabIndex = 5;
            this.lbl_BEEasting.Text = "Not Set";
            // 
            // lbl_BENorthing
            // 
            this.lbl_BENorthing.AutoSize = true;
            this.lbl_BENorthing.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BENorthing.Location = new System.Drawing.Point(7, 99);
            this.lbl_BENorthing.Name = "lbl_BENorthing";
            this.lbl_BENorthing.Size = new System.Drawing.Size(63, 16);
            this.lbl_BENorthing.TabIndex = 4;
            this.lbl_BENorthing.Text = "Not Set";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Current Bullseye:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Example: 206° / 23 nmi";
            // 
            // panel9
            // 
            this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel9.Controls.Add(this.label2);
            this.panel9.Controls.Add(this.TB_Bulls_Range);
            this.panel9.Location = new System.Drawing.Point(6, 39);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(153, 27);
            this.panel9.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "nmi";
            // 
            // TB_Bulls_Range
            // 
            this.TB_Bulls_Range.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Bulls_Range.Location = new System.Drawing.Point(3, 3);
            this.TB_Bulls_Range.MaxLength = 6;
            this.TB_Bulls_Range.Name = "TB_Bulls_Range";
            this.TB_Bulls_Range.Size = new System.Drawing.Size(118, 20);
            this.TB_Bulls_Range.TabIndex = 2;
            this.TB_Bulls_Range.TextChanged += new System.EventHandler(this.TB_Bulls_Range_TextChanged);
            this.TB_Bulls_Range.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_Bulls_Range_KeyPress);
            this.TB_Bulls_Range.Leave += new System.EventHandler(this.TB_Input_Leave);
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.Controls.Add(this.label1);
            this.panel10.Controls.Add(this.TB_Bulls_Bearing);
            this.panel10.Location = new System.Drawing.Point(6, 6);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(153, 27);
            this.panel10.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Deg";
            // 
            // TB_Bulls_Bearing
            // 
            this.TB_Bulls_Bearing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Bulls_Bearing.Location = new System.Drawing.Point(3, 3);
            this.TB_Bulls_Bearing.MaxLength = 5;
            this.TB_Bulls_Bearing.Name = "TB_Bulls_Bearing";
            this.TB_Bulls_Bearing.Size = new System.Drawing.Size(118, 20);
            this.TB_Bulls_Bearing.TabIndex = 1;
            this.TB_Bulls_Bearing.TextChanged += new System.EventHandler(this.TB_Bulls_Bearing_TextChanged);
            this.TB_Bulls_Bearing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_Bulls_Bearing_KeyPress);
            this.TB_Bulls_Bearing.Leave += new System.EventHandler(this.TB_Input_Leave);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_Error,
            this.lbl_DCS_Status,
            this.pb_Transfer});
            this.statusStrip1.Location = new System.Drawing.Point(0, 484);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1189, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbl_Error
            // 
            this.lbl_Error.BackColor = System.Drawing.Color.Tomato;
            this.lbl_Error.Name = "lbl_Error";
            this.lbl_Error.Size = new System.Drawing.Size(67, 17);
            this.lbl_Error.Text = "<ERROR>";
            this.lbl_Error.Visible = false;
            // 
            // lbl_DCS_Status
            // 
            this.lbl_DCS_Status.BackColor = System.Drawing.Color.Yellow;
            this.lbl_DCS_Status.Name = "lbl_DCS_Status";
            this.lbl_DCS_Status.Size = new System.Drawing.Size(127, 17);
            this.lbl_DCS_Status.Text = "DCS not connected";
            this.lbl_DCS_Status.BackColorChanged += new System.EventHandler(this.lbl_DCS_Status_BackColorChanged);
            this.lbl_DCS_Status.Click += new System.EventHandler(this.fetchF10ToolStripMenuItem_Click);
            // 
            // pb_Transfer
            // 
            this.pb_Transfer.Name = "pb_Transfer";
            this.pb_Transfer.Size = new System.Drawing.Size(300, 16);
            this.pb_Transfer.Step = 1;
            this.pb_Transfer.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb_Transfer.Visible = false;
            // 
            // dgv_CoordinateList
            // 
            this.dgv_CoordinateList.AllowUserToAddRows = false;
            this.dgv_CoordinateList.AllowUserToDeleteRows = false;
            this.dgv_CoordinateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_CoordinateList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CoordinateList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colLabel,
            this.colCoordinates,
            this.colAltitude,
            this.colXFer,
            this.colDelete});
            this.dgv_CoordinateList.Location = new System.Drawing.Point(505, 13);
            this.dgv_CoordinateList.MultiSelect = false;
            this.dgv_CoordinateList.Name = "dgv_CoordinateList";
            this.dgv_CoordinateList.Size = new System.Drawing.Size(672, 468);
            this.dgv_CoordinateList.TabIndex = 30;
            this.dgv_CoordinateList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CoordinateList_CellContentClick);
            this.dgv_CoordinateList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CoordinateList_CellDoubleClick);
            // 
            // colId
            // 
            this.colId.DataPropertyName = "ID";
            this.colId.Frozen = true;
            this.colId.HeaderText = "#";
            this.colId.MinimumWidth = 35;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colId.ToolTipText = "ID of the point.";
            this.colId.Width = 35;
            // 
            // colLabel
            // 
            this.colLabel.DataPropertyName = "Name";
            this.colLabel.HeaderText = "Label";
            this.colLabel.MaxInputLength = 12;
            this.colLabel.MinimumWidth = 30;
            this.colLabel.Name = "colLabel";
            this.colLabel.ToolTipText = "User defined label for the point";
            // 
            // colCoordinates
            // 
            this.colCoordinates.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCoordinates.DataPropertyName = "CoordinateStr";
            this.colCoordinates.HeaderText = "Coordinates";
            this.colCoordinates.MinimumWidth = 180;
            this.colCoordinates.Name = "colCoordinates";
            this.colCoordinates.ReadOnly = true;
            this.colCoordinates.ToolTipText = "Coordinate of the point.";
            // 
            // colAltitude
            // 
            this.colAltitude.DataPropertyName = "Altitude";
            this.colAltitude.HeaderText = "Altitude";
            this.colAltitude.MaxInputLength = 5;
            this.colAltitude.MinimumWidth = 100;
            this.colAltitude.Name = "colAltitude";
            this.colAltitude.ToolTipText = "Altitude of the point.";
            // 
            // colXFer
            // 
            this.colXFer.DataPropertyName = "XFER";
            this.colXFer.FalseValue = "";
            this.colXFer.HeaderText = "XFER";
            this.colXFer.IndeterminateValue = "";
            this.colXFer.MinimumWidth = 40;
            this.colXFer.Name = "colXFer";
            this.colXFer.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colXFer.ToolTipText = "Check to enable transfer to DCS.";
            this.colXFer.TrueValue = "";
            this.colXFer.Width = 40;
            // 
            // colDelete
            // 
            this.colDelete.HeaderText = "Del";
            this.colDelete.MinimumWidth = 30;
            this.colDelete.Name = "colDelete";
            this.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDelete.Text = "-";
            this.colDelete.ToolTipText = "Remove a point from the list.";
            this.colDelete.UseColumnTextForButtonValue = true;
            this.colDelete.Width = 30;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dCSToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1189, 24);
            this.menuStrip1.TabIndex = 31;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.loadToolStripMenuItem.Text = "Load file...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // dCSToolStripMenuItem
            // 
            this.dCSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transferToolStripMenuItem,
            this.fetchF10ToolStripMenuItem,
            this.aircraftToolStripMenuItem});
            this.dCSToolStripMenuItem.Name = "dCSToolStripMenuItem";
            this.dCSToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.dCSToolStripMenuItem.Text = "DCS";
            // 
            // transferToolStripMenuItem
            // 
            this.transferToolStripMenuItem.Name = "transferToolStripMenuItem";
            this.transferToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.transferToolStripMenuItem.Text = "Transfer";
            this.transferToolStripMenuItem.Click += new System.EventHandler(this.transferToolStripMenuItem_Click);
            // 
            // fetchF10ToolStripMenuItem
            // 
            this.fetchF10ToolStripMenuItem.Name = "fetchF10ToolStripMenuItem";
            this.fetchF10ToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.fetchF10ToolStripMenuItem.Text = "Fetch F10";
            this.fetchF10ToolStripMenuItem.Click += new System.EventHandler(this.fetchF10ToolStripMenuItem_Click);
            // 
            // aircraftToolStripMenuItem
            // 
            this.aircraftToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoToolStripMenuItem,
            this.toolStripSeparator1,
            this.a10ToolStripMenuItem,
            this.aH64PLTToolStripMenuItem,
            this.aH64CPGToolStripMenuItem,
            this.aV8BToolStripMenuItem,
            this.f15EPLTToolStripMenuItem,
            this.f15EWSOToolStripMenuItem,
            this.f16ToolStripMenuItem,
            this.f18ToolStripMenuItem,
            this.kA50ToolStripMenuItem,
            this.m2000ToolStripMenuItem});
            this.aircraftToolStripMenuItem.Name = "aircraftToolStripMenuItem";
            this.aircraftToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.aircraftToolStripMenuItem.Text = "Aircraft";
            // 
            // autoToolStripMenuItem
            // 
            this.autoToolStripMenuItem.Checked = true;
            this.autoToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoToolStripMenuItem.Name = "autoToolStripMenuItem";
            this.autoToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.autoToolStripMenuItem.Text = "Auto";
            this.autoToolStripMenuItem.Click += new System.EventHandler(this.autoAircraftToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // a10ToolStripMenuItem
            // 
            this.a10ToolStripMenuItem.Name = "a10ToolStripMenuItem";
            this.a10ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.a10ToolStripMenuItem.Text = "A10";
            this.a10ToolStripMenuItem.Click += new System.EventHandler(this.aircraftSelectionToolStripMenuItem_Click);
            // 
            // aH64PLTToolStripMenuItem
            // 
            this.aH64PLTToolStripMenuItem.Name = "aH64PLTToolStripMenuItem";
            this.aH64PLTToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.aH64PLTToolStripMenuItem.Text = "AH64-PLT";
            this.aH64PLTToolStripMenuItem.Click += new System.EventHandler(this.aircraftSelectionToolStripMenuItem_Click);
            // 
            // aH64CPGToolStripMenuItem
            // 
            this.aH64CPGToolStripMenuItem.Name = "aH64CPGToolStripMenuItem";
            this.aH64CPGToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.aH64CPGToolStripMenuItem.Text = "AH64-CPG";
            this.aH64CPGToolStripMenuItem.Click += new System.EventHandler(this.aircraftSelectionToolStripMenuItem_Click);
            // 
            // aV8BToolStripMenuItem
            // 
            this.aV8BToolStripMenuItem.Name = "aV8BToolStripMenuItem";
            this.aV8BToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.aV8BToolStripMenuItem.Text = "AV8B";
            this.aV8BToolStripMenuItem.Click += new System.EventHandler(this.aircraftSelectionToolStripMenuItem_Click);
            // 
            // f15EPLTToolStripMenuItem
            // 
            this.f15EPLTToolStripMenuItem.Name = "f15EPLTToolStripMenuItem";
            this.f15EPLTToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.f15EPLTToolStripMenuItem.Text = "F15E-PLT";
            this.f15EPLTToolStripMenuItem.Click += new System.EventHandler(this.aircraftSelectionToolStripMenuItem_Click);
            // 
            // f15EWSOToolStripMenuItem
            // 
            this.f15EWSOToolStripMenuItem.Name = "f15EWSOToolStripMenuItem";
            this.f15EWSOToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.f15EWSOToolStripMenuItem.Text = "F15E-WSO";
            this.f15EWSOToolStripMenuItem.Click += new System.EventHandler(this.aircraftSelectionToolStripMenuItem_Click);
            // 
            // f16ToolStripMenuItem
            // 
            this.f16ToolStripMenuItem.Name = "f16ToolStripMenuItem";
            this.f16ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.f16ToolStripMenuItem.Text = "F16";
            this.f16ToolStripMenuItem.Click += new System.EventHandler(this.aircraftSelectionToolStripMenuItem_Click);
            // 
            // f18ToolStripMenuItem
            // 
            this.f18ToolStripMenuItem.Name = "f18ToolStripMenuItem";
            this.f18ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.f18ToolStripMenuItem.Text = "F18";
            this.f18ToolStripMenuItem.Click += new System.EventHandler(this.aircraftSelectionToolStripMenuItem_Click);
            // 
            // kA50ToolStripMenuItem
            // 
            this.kA50ToolStripMenuItem.Name = "kA50ToolStripMenuItem";
            this.kA50ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.kA50ToolStripMenuItem.Text = "KA50";
            this.kA50ToolStripMenuItem.Click += new System.EventHandler(this.aircraftSelectionToolStripMenuItem_Click);
            // 
            // m2000ToolStripMenuItem
            // 
            this.m2000ToolStripMenuItem.Name = "m2000ToolStripMenuItem";
            this.m2000ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.m2000ToolStripMenuItem.Text = "M2000";
            this.m2000ToolStripMenuItem.Click += new System.EventHandler(this.aircraftSelectionToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dCSMainScreenToolStripMenuItem,
            this.reticleSettingToolStripMenuItem,
            this.cameraPositionModeToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // dCSMainScreenToolStripMenuItem
            // 
            this.dCSMainScreenToolStripMenuItem.Name = "dCSMainScreenToolStripMenuItem";
            this.dCSMainScreenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dCSMainScreenToolStripMenuItem.Text = "DCS Main Screen";
            // 
            // reticleSettingToolStripMenuItem
            // 
            this.reticleSettingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whenInF10MapToolStripMenuItem,
            this.alwaysToolStripMenuItem,
            this.neverToolStripMenuItem});
            this.reticleSettingToolStripMenuItem.Name = "reticleSettingToolStripMenuItem";
            this.reticleSettingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reticleSettingToolStripMenuItem.Text = "Reticle Setting";
            // 
            // whenInF10MapToolStripMenuItem
            // 
            this.whenInF10MapToolStripMenuItem.Name = "whenInF10MapToolStripMenuItem";
            this.whenInF10MapToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.whenInF10MapToolStripMenuItem.Text = "When in F10 Map";
            this.whenInF10MapToolStripMenuItem.Click += new System.EventHandler(this.whenInF10MapToolStripMenuItem_Click);
            // 
            // alwaysToolStripMenuItem
            // 
            this.alwaysToolStripMenuItem.Name = "alwaysToolStripMenuItem";
            this.alwaysToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.alwaysToolStripMenuItem.Text = "Always";
            this.alwaysToolStripMenuItem.Click += new System.EventHandler(this.alwaysToolStripMenuItem_Click);
            // 
            // neverToolStripMenuItem
            // 
            this.neverToolStripMenuItem.Name = "neverToolStripMenuItem";
            this.neverToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.neverToolStripMenuItem.Text = "Never";
            this.neverToolStripMenuItem.Click += new System.EventHandler(this.neverToolStripMenuItem_Click);
            // 
            // tmr250ms
            // 
            this.tmr250ms.Interval = 250;
            this.tmr250ms.Tick += new System.EventHandler(this.tmr250ms_Tick);
            // 
            // cameraPositionModeToolStripMenuItem
            // 
            this.cameraPositionModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terrainElevationUnderCameraToolStripMenuItem,
            this.cameraAltitudeToolStripMenuItem});
            this.cameraPositionModeToolStripMenuItem.Name = "cameraPositionModeToolStripMenuItem";
            this.cameraPositionModeToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.cameraPositionModeToolStripMenuItem.Text = "Camera Position Mode";
            // 
            // terrainElevationUnderCameraToolStripMenuItem
            // 
            this.terrainElevationUnderCameraToolStripMenuItem.Checked = true;
            this.terrainElevationUnderCameraToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.terrainElevationUnderCameraToolStripMenuItem.Name = "terrainElevationUnderCameraToolStripMenuItem";
            this.terrainElevationUnderCameraToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.terrainElevationUnderCameraToolStripMenuItem.Text = "Terrain elevation under Camera";
            this.terrainElevationUnderCameraToolStripMenuItem.Click += new System.EventHandler(this.terrainElevationUnderCameraToolStripMenuItem_Click);
            // 
            // cameraAltitudeToolStripMenuItem
            // 
            this.cameraAltitudeToolStripMenuItem.Name = "cameraAltitudeToolStripMenuItem";
            this.cameraAltitudeToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.cameraAltitudeToolStripMenuItem.Text = "Camera altitude";
            this.cameraAltitudeToolStripMenuItem.Click += new System.EventHandler(this.cameraAltitudeToolStripMenuItem_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 506);
            this.Controls.Add(this.dgv_CoordinateList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.GrpBox_Input);
            this.Controls.Add(this.GrpBox_Output);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Coordinate Converter";
            this.GrpBox_Output.ResumeLayout(false);
            this.GrpBox_Output.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_MGRS_Precision)).EndInit();
            this.GrpBox_Input.ResumeLayout(false);
            this.GrpBox_Input.PerformLayout();
            this.TC_Input.ResumeLayout(false);
            this.TabPage_LatLon.ResumeLayout(false);
            this.TabPage_LatLon.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CoordinateList)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox GrpBox_Output;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TB_Out_Bulls;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TB_Out_UTM;
        private System.Windows.Forms.TextBox TB_Out_MGRS;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TB_Out_LL;
        private System.Windows.Forms.TextBox TB_Out_LLDec;
        private System.Windows.Forms.GroupBox GrpBox_Input;
        private System.Windows.Forms.TabControl TC_Input;
        private System.Windows.Forms.TabPage TabPage_LatLon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TB_LL_Lon;
        private System.Windows.Forms.RadioButton RB_LL_W;
        private System.Windows.Forms.RadioButton RB_LL_E;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TB_LL_Lat;
        private System.Windows.Forms.RadioButton RB_LL_N;
        private System.Windows.Forms.RadioButton RB_LL_S;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox TB_LLDec_Lon;
        private System.Windows.Forms.RadioButton RB_LLDec_W;
        private System.Windows.Forms.RadioButton RB_LLDec_E;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox TB_LLDec_Lat;
        private System.Windows.Forms.RadioButton RB_LLDec_N;
        private System.Windows.Forms.RadioButton RB_LLDec_S;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox TB_UTM_Northing;
        private System.Windows.Forms.TextBox TB_UTM_Easting;
        private System.Windows.Forms.TextBox TB_UTM_LongZone;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Bulls_Range;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Bulls_Bearing;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox TB_UTM_LatZone;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox TB_MGRS_Fraction;
        private System.Windows.Forms.TextBox TB_MGRS_LatZone;
        private System.Windows.Forms.TextBox TB_MGRS_LongZone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox TB_MGRS_Digraph;
        private Button btn_Add;
        private Button btn_SetBE;
        private Label lbl_BEEasting;
        private Label lbl_BENorthing;
        private Label label4;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lbl_Error;
        private DataGridView dgv_CoordinateList;
        private RadioButton rb_Format_LL;
        private RadioButton rb_Format_BE;
        private RadioButton rb_Format_UTM;
        private RadioButton rb_Format_MGRS;
        private RadioButton rb_Format_LLDec;
        private Label label14;
        private NumericUpDown nud_MGRS_Precision;
        private Button btn_MoveUp;
        private Button btn_MoveDown;
        private Label label15;
        private ComboBox cb_AltitudeUnit;
        private TextBox tb_Altitude;
        private Label label16;
        private TextBox tb_Label;
        private Button btn_Replace;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem dCSToolStripMenuItem;
        private ToolStripMenuItem aircraftToolStripMenuItem;
        private ToolStripMenuItem autoToolStripMenuItem;
        private ToolStripMenuItem a10ToolStripMenuItem;
        private ToolStripMenuItem aH64PLTToolStripMenuItem;
        private ToolStripMenuItem aH64CPGToolStripMenuItem;
        private ToolStripMenuItem transferToolStripMenuItem;
        private ToolStripMenuItem aV8BToolStripMenuItem;
        private ToolStripMenuItem f15EWSOToolStripMenuItem;
        private ToolStripMenuItem f16ToolStripMenuItem;
        private ToolStripMenuItem f18ToolStripMenuItem;
        private ToolStripMenuItem kA50ToolStripMenuItem;
        private ToolStripMenuItem m2000ToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem fetchF10ToolStripMenuItem;
        private ToolStripStatusLabel lbl_DCS_Status;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colLabel;
        private DataGridViewTextBoxColumn colCoordinates;
        private DataGridViewTextBoxColumn colAltitude;
        private DataGridViewCheckBoxColumn colXFer;
        private DataGridViewButtonColumn colDelete;
        private Label label18;
        private ComboBox cb_pointType;
        private Label label17;
        private ComboBox cb_pointOption;
        private CheckBox cb_altitudeIsAGL;
        private ToolStripMenuItem f15EPLTToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private Timer tmr250ms;
        private ToolStripProgressBar pb_Transfer;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem dCSMainScreenToolStripMenuItem;
        private ToolStripMenuItem reticleSettingToolStripMenuItem;
        private ToolStripMenuItem whenInF10MapToolStripMenuItem;
        private ToolStripMenuItem alwaysToolStripMenuItem;
        private ToolStripMenuItem neverToolStripMenuItem;
        private ToolStripMenuItem cameraPositionModeToolStripMenuItem;
        private ToolStripMenuItem terrainElevationUnderCameraToolStripMenuItem;
        private ToolStripMenuItem cameraAltitudeToolStripMenuItem;
    }
}

