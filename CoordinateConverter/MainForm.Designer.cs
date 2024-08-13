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
            this.nud_LL_DecimalMinutes_Precision = new System.Windows.Forms.NumericUpDown();
            this.nud_LL_DecimalSeconds_Precision = new System.Windows.Forms.NumericUpDown();
            this.nud_MGRS_Precision = new System.Windows.Forms.NumericUpDown();
            this.rb_Format_Bullseye = new System.Windows.Forms.RadioButton();
            this.rb_Format_UTM = new System.Windows.Forms.RadioButton();
            this.rb_Format_MGRS = new System.Windows.Forms.RadioButton();
            this.rb_Format_LL_DecimalMinutes = new System.Windows.Forms.RadioButton();
            this.rb_Format_LL_DecimalSeconds = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_Out_Bullseye = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_Out_UTM = new System.Windows.Forms.TextBox();
            this.tb_Out_MGRS = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_Out_LL_DecimalSeconds = new System.Windows.Forms.TextBox();
            this.tb_Out_LL_DecimalMinutes = new System.Windows.Forms.TextBox();
            this.GrpBox_Input = new System.Windows.Forms.GroupBox();
            this.cb_AltitudeIsAGL = new System.Windows.Forms.CheckBox();
            this.cb_PointOption = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cb_PointType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tb_Label = new System.Windows.Forms.TextBox();
            this.cb_AltitudeUnit = new System.Windows.Forms.ComboBox();
            this.tb_Altitude = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.TC_Input = new System.Windows.Forms.TabControl();
            this.TabPage_LatLon = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_LL_DecimalSeconds_Longitude = new System.Windows.Forms.TextBox();
            this.rb_LL_DecimalSeconds_W = new System.Windows.Forms.RadioButton();
            this.rb_LL_DecimalSeconds_E = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tb_LL_DecimalSeconds_Latitude = new System.Windows.Forms.TextBox();
            this.rb_LL_DecimalSeconds_N = new System.Windows.Forms.RadioButton();
            this.rb_LL_DecimalSeconds_S = new System.Windows.Forms.RadioButton();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tb_LL_DecimalMinutes_Longitude = new System.Windows.Forms.TextBox();
            this.rb_LL_DecimalMinutes_W = new System.Windows.Forms.RadioButton();
            this.rb_LL_DecimalMinutes_E = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tb_LL_DecimalMinutes_Latitude = new System.Windows.Forms.TextBox();
            this.rb_LL_DecimalMinutes_N = new System.Windows.Forms.RadioButton();
            this.rb_LL_DecimalMinutes_S = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tb_MGRS_Digraph = new System.Windows.Forms.TextBox();
            this.tb_MGRS_Fraction = new System.Windows.Forms.TextBox();
            this.tb_MGRS_LatZone = new System.Windows.Forms.TextBox();
            this.tb_MGRS_LongZone = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tb_UTM_LatZone = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tb_UTM_Easting = new System.Windows.Forms.TextBox();
            this.tb_UTM_Northing = new System.Windows.Forms.TextBox();
            this.tb_UTM_LongZone = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lbl_BEPosition = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Bullseye_Range = new System.Windows.Forms.TextBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Bullseye_Bearing = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pb_Transfer = new System.Windows.Forms.ToolStripProgressBar();
            this.lbl_DCS_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_Error = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgv_CoordinateList = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoordinates = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAltitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colXFer = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmi_FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_recentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_DCSMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Transfer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_StopTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_FetchCoordinates = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ImportUnits = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_AircraftMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Auto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_AH64Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AH64 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AH64_ClearPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AH64_DTC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AV8B = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_F15EMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_F15E_Pilot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_F15E_WSO = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_F16Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_F16 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_F16_SetFirstPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_F18 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_JF17Menu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_JF17 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_JF17_SetFirstPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_KA50 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_M2000 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_OH58DMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_OH58D = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_DCSMainScreenMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ReticleSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Reticle_WhenInF10Map = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Reticle_Always = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Reticle_Never = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_CameraPositionModeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_TerrainElevationUnderCamera = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_CameraAltitude = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_AlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_TransparencyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_opaque = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Opacity75 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Opacity50 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Opacity25 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_CheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_AutoCheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_changeBaseDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.miscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_execute = new System.Windows.Forms.ToolStripMenuItem();
            this.tmr250ms = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_Transfer = new System.Windows.Forms.ToolStripButton();
            this.btn_Stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_FetchCoordinates = new System.Windows.Forms.ToolStripButton();
            this.btn_ImportUnits = new System.Windows.Forms.ToolStripButton();
            this.btn_SetBE = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_AlwaysOnTop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_Add = new System.Windows.Forms.ToolStripButton();
            this.btn_Edit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_MoveDown = new System.Windows.Forms.ToolStripButton();
            this.btn_MoveUp = new System.Windows.Forms.ToolStripButton();
            this.label19 = new System.Windows.Forms.Label();
            this.tsmi_A10C = new System.Windows.Forms.ToolStripMenuItem();
            this.GrpBox_Output.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_LL_DecimalMinutes_Precision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_LL_DecimalSeconds_Precision)).BeginInit();
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
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpBox_Output
            // 
            this.GrpBox_Output.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GrpBox_Output.Controls.Add(this.nud_LL_DecimalMinutes_Precision);
            this.GrpBox_Output.Controls.Add(this.nud_LL_DecimalSeconds_Precision);
            this.GrpBox_Output.Controls.Add(this.nud_MGRS_Precision);
            this.GrpBox_Output.Controls.Add(this.rb_Format_Bullseye);
            this.GrpBox_Output.Controls.Add(this.rb_Format_UTM);
            this.GrpBox_Output.Controls.Add(this.rb_Format_MGRS);
            this.GrpBox_Output.Controls.Add(this.rb_Format_LL_DecimalMinutes);
            this.GrpBox_Output.Controls.Add(this.rb_Format_LL_DecimalSeconds);
            this.GrpBox_Output.Controls.Add(this.label12);
            this.GrpBox_Output.Controls.Add(this.label11);
            this.GrpBox_Output.Controls.Add(this.tb_Out_Bullseye);
            this.GrpBox_Output.Controls.Add(this.label10);
            this.GrpBox_Output.Controls.Add(this.tb_Out_UTM);
            this.GrpBox_Output.Controls.Add(this.tb_Out_MGRS);
            this.GrpBox_Output.Controls.Add(this.label9);
            this.GrpBox_Output.Controls.Add(this.label8);
            this.GrpBox_Output.Controls.Add(this.tb_Out_LL_DecimalSeconds);
            this.GrpBox_Output.Controls.Add(this.tb_Out_LL_DecimalMinutes);
            this.GrpBox_Output.Location = new System.Drawing.Point(0, 316);
            this.GrpBox_Output.Name = "GrpBox_Output";
            this.GrpBox_Output.Size = new System.Drawing.Size(499, 155);
            this.GrpBox_Output.TabIndex = 2;
            this.GrpBox_Output.TabStop = false;
            this.GrpBox_Output.Text = "Output";
            // 
            // nud_LL_DecimalMinutes_Precision
            // 
            this.nud_LL_DecimalMinutes_Precision.Location = new System.Drawing.Point(446, 46);
            this.nud_LL_DecimalMinutes_Precision.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nud_LL_DecimalMinutes_Precision.Name = "nud_LL_DecimalMinutes_Precision";
            this.nud_LL_DecimalMinutes_Precision.Size = new System.Drawing.Size(27, 20);
            this.nud_LL_DecimalMinutes_Precision.TabIndex = 12;
            this.nud_LL_DecimalMinutes_Precision.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nud_LL_DecimalMinutes_Precision.ValueChanged += new System.EventHandler(this.Nud_LL_DecimalMinutes_Precision_ValueChanged);
            // 
            // nud_LL_DecimalSeconds_Precision
            // 
            this.nud_LL_DecimalSeconds_Precision.Location = new System.Drawing.Point(446, 20);
            this.nud_LL_DecimalSeconds_Precision.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nud_LL_DecimalSeconds_Precision.Name = "nud_LL_DecimalSeconds_Precision";
            this.nud_LL_DecimalSeconds_Precision.Size = new System.Drawing.Size(27, 20);
            this.nud_LL_DecimalSeconds_Precision.TabIndex = 10;
            this.nud_LL_DecimalSeconds_Precision.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nud_LL_DecimalSeconds_Precision.ValueChanged += new System.EventHandler(this.Nud_LL_DecimalSeconds_Precision_ValueChanged);
            // 
            // nud_MGRS_Precision
            // 
            this.nud_MGRS_Precision.Location = new System.Drawing.Point(446, 72);
            this.nud_MGRS_Precision.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nud_MGRS_Precision.Name = "nud_MGRS_Precision";
            this.nud_MGRS_Precision.Size = new System.Drawing.Size(27, 20);
            this.nud_MGRS_Precision.TabIndex = 14;
            this.nud_MGRS_Precision.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nud_MGRS_Precision.ValueChanged += new System.EventHandler(this.Nud_MGRS_Precision_ValueChanged);
            // 
            // rb_Format_Bullseye
            // 
            this.rb_Format_Bullseye.AutoSize = true;
            this.rb_Format_Bullseye.Location = new System.Drawing.Point(479, 127);
            this.rb_Format_Bullseye.Name = "rb_Format_Bullseye";
            this.rb_Format_Bullseye.Size = new System.Drawing.Size(14, 13);
            this.rb_Format_Bullseye.TabIndex = 17;
            this.rb_Format_Bullseye.UseVisualStyleBackColor = true;
            this.rb_Format_Bullseye.CheckedChanged += new System.EventHandler(this.Rb_Format_CheckedChanged);
            // 
            // rb_Format_UTM
            // 
            this.rb_Format_UTM.AutoSize = true;
            this.rb_Format_UTM.Location = new System.Drawing.Point(479, 101);
            this.rb_Format_UTM.Name = "rb_Format_UTM";
            this.rb_Format_UTM.Size = new System.Drawing.Size(14, 13);
            this.rb_Format_UTM.TabIndex = 16;
            this.rb_Format_UTM.UseVisualStyleBackColor = true;
            this.rb_Format_UTM.CheckedChanged += new System.EventHandler(this.Rb_Format_CheckedChanged);
            // 
            // rb_Format_MGRS
            // 
            this.rb_Format_MGRS.AutoSize = true;
            this.rb_Format_MGRS.Location = new System.Drawing.Point(479, 75);
            this.rb_Format_MGRS.Name = "rb_Format_MGRS";
            this.rb_Format_MGRS.Size = new System.Drawing.Size(14, 13);
            this.rb_Format_MGRS.TabIndex = 15;
            this.rb_Format_MGRS.UseVisualStyleBackColor = true;
            this.rb_Format_MGRS.CheckedChanged += new System.EventHandler(this.Rb_Format_CheckedChanged);
            // 
            // rb_Format_LL_DecimalMinutes
            // 
            this.rb_Format_LL_DecimalMinutes.AutoSize = true;
            this.rb_Format_LL_DecimalMinutes.Location = new System.Drawing.Point(479, 49);
            this.rb_Format_LL_DecimalMinutes.Name = "rb_Format_LL_DecimalMinutes";
            this.rb_Format_LL_DecimalMinutes.Size = new System.Drawing.Size(14, 13);
            this.rb_Format_LL_DecimalMinutes.TabIndex = 13;
            this.rb_Format_LL_DecimalMinutes.UseVisualStyleBackColor = true;
            this.rb_Format_LL_DecimalMinutes.CheckedChanged += new System.EventHandler(this.Rb_Format_CheckedChanged);
            // 
            // rb_Format_LL_DecimalSeconds
            // 
            this.rb_Format_LL_DecimalSeconds.AutoSize = true;
            this.rb_Format_LL_DecimalSeconds.Checked = true;
            this.rb_Format_LL_DecimalSeconds.Location = new System.Drawing.Point(479, 23);
            this.rb_Format_LL_DecimalSeconds.Name = "rb_Format_LL_DecimalSeconds";
            this.rb_Format_LL_DecimalSeconds.Size = new System.Drawing.Size(14, 13);
            this.rb_Format_LL_DecimalSeconds.TabIndex = 11;
            this.rb_Format_LL_DecimalSeconds.TabStop = true;
            this.rb_Format_LL_DecimalSeconds.UseVisualStyleBackColor = true;
            this.rb_Format_LL_DecimalSeconds.CheckedChanged += new System.EventHandler(this.Rb_Format_CheckedChanged);
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
            // tb_Out_Bullseye
            // 
            this.tb_Out_Bullseye.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Out_Bullseye.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Out_Bullseye.Location = new System.Drawing.Point(50, 124);
            this.tb_Out_Bullseye.Name = "tb_Out_Bullseye";
            this.tb_Out_Bullseye.ReadOnly = true;
            this.tb_Out_Bullseye.Size = new System.Drawing.Size(423, 20);
            this.tb_Out_Bullseye.TabIndex = 4;
            this.tb_Out_Bullseye.TabStop = false;
            this.tb_Out_Bullseye.Text = "Enter Coordinates";
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
            // tb_Out_UTM
            // 
            this.tb_Out_UTM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Out_UTM.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Out_UTM.Location = new System.Drawing.Point(50, 98);
            this.tb_Out_UTM.Name = "tb_Out_UTM";
            this.tb_Out_UTM.ReadOnly = true;
            this.tb_Out_UTM.Size = new System.Drawing.Size(423, 20);
            this.tb_Out_UTM.TabIndex = 3;
            this.tb_Out_UTM.TabStop = false;
            this.tb_Out_UTM.Text = "Enter Coordinates";
            // 
            // tb_Out_MGRS
            // 
            this.tb_Out_MGRS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Out_MGRS.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Out_MGRS.Location = new System.Drawing.Point(50, 72);
            this.tb_Out_MGRS.Name = "tb_Out_MGRS";
            this.tb_Out_MGRS.ReadOnly = true;
            this.tb_Out_MGRS.Size = new System.Drawing.Size(390, 20);
            this.tb_Out_MGRS.TabIndex = 2;
            this.tb_Out_MGRS.TabStop = false;
            this.tb_Out_MGRS.Text = "Enter Coordinates";
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
            // tb_Out_LL_DecimalSeconds
            // 
            this.tb_Out_LL_DecimalSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Out_LL_DecimalSeconds.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Out_LL_DecimalSeconds.Location = new System.Drawing.Point(50, 20);
            this.tb_Out_LL_DecimalSeconds.Name = "tb_Out_LL_DecimalSeconds";
            this.tb_Out_LL_DecimalSeconds.ReadOnly = true;
            this.tb_Out_LL_DecimalSeconds.Size = new System.Drawing.Size(390, 20);
            this.tb_Out_LL_DecimalSeconds.TabIndex = 0;
            this.tb_Out_LL_DecimalSeconds.TabStop = false;
            this.tb_Out_LL_DecimalSeconds.Text = "Enter Coordinates";
            // 
            // tb_Out_LL_DecimalMinutes
            // 
            this.tb_Out_LL_DecimalMinutes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Out_LL_DecimalMinutes.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Out_LL_DecimalMinutes.Location = new System.Drawing.Point(50, 46);
            this.tb_Out_LL_DecimalMinutes.Name = "tb_Out_LL_DecimalMinutes";
            this.tb_Out_LL_DecimalMinutes.ReadOnly = true;
            this.tb_Out_LL_DecimalMinutes.Size = new System.Drawing.Size(390, 20);
            this.tb_Out_LL_DecimalMinutes.TabIndex = 1;
            this.tb_Out_LL_DecimalMinutes.TabStop = false;
            this.tb_Out_LL_DecimalMinutes.Text = "Enter Coordinates";
            // 
            // GrpBox_Input
            // 
            this.GrpBox_Input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GrpBox_Input.Controls.Add(this.cb_AltitudeIsAGL);
            this.GrpBox_Input.Controls.Add(this.cb_PointOption);
            this.GrpBox_Input.Controls.Add(this.label18);
            this.GrpBox_Input.Controls.Add(this.cb_PointType);
            this.GrpBox_Input.Controls.Add(this.label17);
            this.GrpBox_Input.Controls.Add(this.tb_Label);
            this.GrpBox_Input.Controls.Add(this.cb_AltitudeUnit);
            this.GrpBox_Input.Controls.Add(this.tb_Altitude);
            this.GrpBox_Input.Controls.Add(this.label16);
            this.GrpBox_Input.Controls.Add(this.label15);
            this.GrpBox_Input.Controls.Add(this.TC_Input);
            this.GrpBox_Input.Location = new System.Drawing.Point(12, 66);
            this.GrpBox_Input.Name = "GrpBox_Input";
            this.GrpBox_Input.Size = new System.Drawing.Size(487, 244);
            this.GrpBox_Input.TabIndex = 0;
            this.GrpBox_Input.TabStop = false;
            this.GrpBox_Input.Text = "Input";
            // 
            // cb_AltitudeIsAGL
            // 
            this.cb_AltitudeIsAGL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_AltitudeIsAGL.AutoSize = true;
            this.cb_AltitudeIsAGL.Checked = true;
            this.cb_AltitudeIsAGL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_AltitudeIsAGL.Location = new System.Drawing.Point(199, 220);
            this.cb_AltitudeIsAGL.Name = "cb_AltitudeIsAGL";
            this.cb_AltitudeIsAGL.Size = new System.Drawing.Size(47, 17);
            this.cb_AltitudeIsAGL.TabIndex = 8;
            this.cb_AltitudeIsAGL.Text = "AGL";
            this.cb_AltitudeIsAGL.UseVisualStyleBackColor = true;
            this.cb_AltitudeIsAGL.CheckedChanged += new System.EventHandler(this.Cb_AltitudeIsAGL_CheckedChanged);
            // 
            // cb_PointOption
            // 
            this.cb_PointOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_PointOption.AutoCompleteCustomSource.AddRange(new string[] {
            "Waypoint"});
            this.cb_PointOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_PointOption.Enabled = false;
            this.cb_PointOption.FormattingEnabled = true;
            this.cb_PointOption.Location = new System.Drawing.Point(308, 191);
            this.cb_PointOption.Name = "cb_PointOption";
            this.cb_PointOption.Size = new System.Drawing.Size(172, 21);
            this.cb_PointOption.TabIndex = 12;
            this.cb_PointOption.SelectedIndexChanged += new System.EventHandler(this.Cb_PointOption_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(264, 194);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(38, 13);
            this.label18.TabIndex = 11;
            this.label18.Text = "Option";
            // 
            // cb_PointType
            // 
            this.cb_PointType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_PointType.AutoCompleteCustomSource.AddRange(new string[] {
            "Waypoint"});
            this.cb_PointType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_PointType.Enabled = false;
            this.cb_PointType.FormattingEnabled = true;
            this.cb_PointType.Items.AddRange(new object[] {
            "Waypoint"});
            this.cb_PointType.Location = new System.Drawing.Point(79, 191);
            this.cb_PointType.Name = "cb_PointType";
            this.cb_PointType.Size = new System.Drawing.Size(179, 21);
            this.cb_PointType.TabIndex = 10;
            this.cb_PointType.SelectedIndexChanged += new System.EventHandler(this.Cb_pointType_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 194);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(58, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Point Type";
            // 
            // tb_Label
            // 
            this.tb_Label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Label.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Label.Location = new System.Drawing.Point(308, 218);
            this.tb_Label.MaxLength = 50;
            this.tb_Label.Name = "tb_Label";
            this.tb_Label.Size = new System.Drawing.Size(172, 20);
            this.tb_Label.TabIndex = 9;
            this.tb_Label.TextChanged += new System.EventHandler(this.Tb_Label_TextChanged);
            this.tb_Label.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_Label.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_Label_KeyPress);
            // 
            // cb_AltitudeUnit
            // 
            this.cb_AltitudeUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_AltitudeUnit.FormattingEnabled = true;
            this.cb_AltitudeUnit.Items.AddRange(new object[] {
            "ft",
            "m"});
            this.cb_AltitudeUnit.Location = new System.Drawing.Point(156, 218);
            this.cb_AltitudeUnit.Name = "cb_AltitudeUnit";
            this.cb_AltitudeUnit.Size = new System.Drawing.Size(36, 21);
            this.cb_AltitudeUnit.TabIndex = 7;
            this.cb_AltitudeUnit.Text = "ft";
            this.cb_AltitudeUnit.SelectedIndexChanged += new System.EventHandler(this.Cb_AltitudeUnit_SelectedIndexChanged);
            // 
            // tb_Altitude
            // 
            this.tb_Altitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_Altitude.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Altitude.Location = new System.Drawing.Point(79, 218);
            this.tb_Altitude.MaxLength = 6;
            this.tb_Altitude.Name = "tb_Altitude";
            this.tb_Altitude.Size = new System.Drawing.Size(71, 20);
            this.tb_Altitude.TabIndex = 6;
            this.tb_Altitude.Text = "0";
            this.tb_Altitude.TextChanged += new System.EventHandler(this.Tb_Altitude_TextChanged);
            this.tb_Altitude.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_Altitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_Altitude_KeyPress);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(269, 221);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Label";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(31, 221);
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
            this.TC_Input.Size = new System.Drawing.Size(481, 169);
            this.TC_Input.TabIndex = 0;
            // 
            // TabPage_LatLon
            // 
            this.TabPage_LatLon.Controls.Add(this.textBox1);
            this.TabPage_LatLon.Controls.Add(this.label7);
            this.TabPage_LatLon.Controls.Add(this.panel1);
            this.TabPage_LatLon.Controls.Add(this.panel2);
            this.TabPage_LatLon.Location = new System.Drawing.Point(4, 22);
            this.TabPage_LatLon.Name = "TabPage_LatLon";
            this.TabPage_LatLon.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_LatLon.Size = new System.Drawing.Size(473, 143);
            this.TabPage_LatLon.TabIndex = 0;
            this.TabPage_LatLon.Text = "L/L Decimal Seconds";
            this.TabPage_LatLon.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(10, 85);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(454, 52);
            this.textBox1.TabIndex = 3;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Symbols and spaces may be omitted.\r\nDecimal point is required if a decimal value " +
    "is used, it is not implied.\r\nLeading zeroes are also required.";
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
            this.panel1.Controls.Add(this.tb_LL_DecimalSeconds_Longitude);
            this.panel1.Controls.Add(this.rb_LL_DecimalSeconds_W);
            this.panel1.Controls.Add(this.rb_LL_DecimalSeconds_E);
            this.panel1.Location = new System.Drawing.Point(6, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 27);
            this.panel1.TabIndex = 1;
            // 
            // tb_LL_DecimalSeconds_Longitude
            // 
            this.tb_LL_DecimalSeconds_Longitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_LL_DecimalSeconds_Longitude.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_LL_DecimalSeconds_Longitude.Location = new System.Drawing.Point(3, 3);
            this.tb_LL_DecimalSeconds_Longitude.MaxLength = 16;
            this.tb_LL_DecimalSeconds_Longitude.Name = "tb_LL_DecimalSeconds_Longitude";
            this.tb_LL_DecimalSeconds_Longitude.Size = new System.Drawing.Size(375, 20);
            this.tb_LL_DecimalSeconds_Longitude.TabIndex = 2;
            this.tb_LL_DecimalSeconds_Longitude.TextChanged += new System.EventHandler(this.TB_LL_DecimalSeconds_Longitude_TextChanged);
            this.tb_LL_DecimalSeconds_Longitude.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_LL_DecimalSeconds_Longitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_DecimalSeconds_KeyPress);
            this.tb_LL_DecimalSeconds_Longitude.Leave += new System.EventHandler(this.Tb_Input_Leave);
            // 
            // rb_LL_DecimalSeconds_W
            // 
            this.rb_LL_DecimalSeconds_W.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_LL_DecimalSeconds_W.AutoSize = true;
            this.rb_LL_DecimalSeconds_W.Location = new System.Drawing.Point(387, 4);
            this.rb_LL_DecimalSeconds_W.Name = "rb_LL_DecimalSeconds_W";
            this.rb_LL_DecimalSeconds_W.Size = new System.Drawing.Size(36, 17);
            this.rb_LL_DecimalSeconds_W.TabIndex = 5;
            this.rb_LL_DecimalSeconds_W.Text = "W";
            this.rb_LL_DecimalSeconds_W.UseVisualStyleBackColor = true;
            this.rb_LL_DecimalSeconds_W.CheckedChanged += new System.EventHandler(this.RB_LL_DecimalSeconds_CheckedChanged);
            // 
            // rb_LL_DecimalSeconds_E
            // 
            this.rb_LL_DecimalSeconds_E.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_LL_DecimalSeconds_E.AutoSize = true;
            this.rb_LL_DecimalSeconds_E.Checked = true;
            this.rb_LL_DecimalSeconds_E.Location = new System.Drawing.Point(426, 4);
            this.rb_LL_DecimalSeconds_E.Name = "rb_LL_DecimalSeconds_E";
            this.rb_LL_DecimalSeconds_E.Size = new System.Drawing.Size(32, 17);
            this.rb_LL_DecimalSeconds_E.TabIndex = 6;
            this.rb_LL_DecimalSeconds_E.TabStop = true;
            this.rb_LL_DecimalSeconds_E.Text = "E";
            this.rb_LL_DecimalSeconds_E.UseVisualStyleBackColor = true;
            this.rb_LL_DecimalSeconds_E.CheckedChanged += new System.EventHandler(this.RB_LL_DecimalSeconds_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.tb_LL_DecimalSeconds_Latitude);
            this.panel2.Controls.Add(this.rb_LL_DecimalSeconds_N);
            this.panel2.Controls.Add(this.rb_LL_DecimalSeconds_S);
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(461, 27);
            this.panel2.TabIndex = 0;
            // 
            // tb_LL_DecimalSeconds_Latitude
            // 
            this.tb_LL_DecimalSeconds_Latitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_LL_DecimalSeconds_Latitude.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_LL_DecimalSeconds_Latitude.Location = new System.Drawing.Point(3, 3);
            this.tb_LL_DecimalSeconds_Latitude.MaxLength = 16;
            this.tb_LL_DecimalSeconds_Latitude.Name = "tb_LL_DecimalSeconds_Latitude";
            this.tb_LL_DecimalSeconds_Latitude.Size = new System.Drawing.Size(375, 20);
            this.tb_LL_DecimalSeconds_Latitude.TabIndex = 1;
            this.tb_LL_DecimalSeconds_Latitude.TextChanged += new System.EventHandler(this.TB_LL_DecimalSeconds_Latitude_TextChanged);
            this.tb_LL_DecimalSeconds_Latitude.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_LL_DecimalSeconds_Latitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_DecimalSeconds_KeyPress);
            this.tb_LL_DecimalSeconds_Latitude.Leave += new System.EventHandler(this.Tb_Input_Leave);
            // 
            // rb_LL_DecimalSeconds_N
            // 
            this.rb_LL_DecimalSeconds_N.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_LL_DecimalSeconds_N.AutoSize = true;
            this.rb_LL_DecimalSeconds_N.Checked = true;
            this.rb_LL_DecimalSeconds_N.Location = new System.Drawing.Point(387, 4);
            this.rb_LL_DecimalSeconds_N.Name = "rb_LL_DecimalSeconds_N";
            this.rb_LL_DecimalSeconds_N.Size = new System.Drawing.Size(33, 17);
            this.rb_LL_DecimalSeconds_N.TabIndex = 3;
            this.rb_LL_DecimalSeconds_N.TabStop = true;
            this.rb_LL_DecimalSeconds_N.Text = "N";
            this.rb_LL_DecimalSeconds_N.UseVisualStyleBackColor = true;
            this.rb_LL_DecimalSeconds_N.CheckedChanged += new System.EventHandler(this.RB_LL_DecimalSeconds_CheckedChanged);
            // 
            // rb_LL_DecimalSeconds_S
            // 
            this.rb_LL_DecimalSeconds_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_LL_DecimalSeconds_S.AutoSize = true;
            this.rb_LL_DecimalSeconds_S.Location = new System.Drawing.Point(426, 4);
            this.rb_LL_DecimalSeconds_S.Name = "rb_LL_DecimalSeconds_S";
            this.rb_LL_DecimalSeconds_S.Size = new System.Drawing.Size(32, 17);
            this.rb_LL_DecimalSeconds_S.TabIndex = 4;
            this.rb_LL_DecimalSeconds_S.Text = "S";
            this.rb_LL_DecimalSeconds_S.UseVisualStyleBackColor = true;
            this.rb_LL_DecimalSeconds_S.CheckedChanged += new System.EventHandler(this.RB_LL_DecimalSeconds_CheckedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(473, 143);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "L/L Decimal Minutes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(6, 85);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(461, 88);
            this.textBox2.TabIndex = 4;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "Symbols and spaces may be omitted.\r\nDecimal point is required if a decimal value " +
    "is used, it is not implied.\r\nLeading zeroes are also required.";
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
            this.panel3.Controls.Add(this.tb_LL_DecimalMinutes_Longitude);
            this.panel3.Controls.Add(this.rb_LL_DecimalMinutes_W);
            this.panel3.Controls.Add(this.rb_LL_DecimalMinutes_E);
            this.panel3.Location = new System.Drawing.Point(6, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(464, 27);
            this.panel3.TabIndex = 1;
            // 
            // tb_LL_DecimalMinutes_Longitude
            // 
            this.tb_LL_DecimalMinutes_Longitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_LL_DecimalMinutes_Longitude.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_LL_DecimalMinutes_Longitude.Location = new System.Drawing.Point(3, 3);
            this.tb_LL_DecimalMinutes_Longitude.MaxLength = 16;
            this.tb_LL_DecimalMinutes_Longitude.Name = "tb_LL_DecimalMinutes_Longitude";
            this.tb_LL_DecimalMinutes_Longitude.Size = new System.Drawing.Size(378, 20);
            this.tb_LL_DecimalMinutes_Longitude.TabIndex = 2;
            this.tb_LL_DecimalMinutes_Longitude.TextChanged += new System.EventHandler(this.TB_LL_DecimalMinutes_Longitude_TextChanged);
            this.tb_LL_DecimalMinutes_Longitude.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_LL_DecimalMinutes_Longitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_DecimalMinutes_KeyPress);
            this.tb_LL_DecimalMinutes_Longitude.Leave += new System.EventHandler(this.Tb_Input_Leave);
            // 
            // rb_LL_DecimalMinutes_W
            // 
            this.rb_LL_DecimalMinutes_W.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_LL_DecimalMinutes_W.AutoSize = true;
            this.rb_LL_DecimalMinutes_W.Location = new System.Drawing.Point(387, 4);
            this.rb_LL_DecimalMinutes_W.Name = "rb_LL_DecimalMinutes_W";
            this.rb_LL_DecimalMinutes_W.Size = new System.Drawing.Size(36, 17);
            this.rb_LL_DecimalMinutes_W.TabIndex = 5;
            this.rb_LL_DecimalMinutes_W.Text = "W";
            this.rb_LL_DecimalMinutes_W.UseVisualStyleBackColor = true;
            this.rb_LL_DecimalMinutes_W.CheckedChanged += new System.EventHandler(this.RB_LL_DecimalMinutes_CheckedChanged);
            // 
            // rb_LL_DecimalMinutes_E
            // 
            this.rb_LL_DecimalMinutes_E.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_LL_DecimalMinutes_E.AutoSize = true;
            this.rb_LL_DecimalMinutes_E.Checked = true;
            this.rb_LL_DecimalMinutes_E.Location = new System.Drawing.Point(429, 4);
            this.rb_LL_DecimalMinutes_E.Name = "rb_LL_DecimalMinutes_E";
            this.rb_LL_DecimalMinutes_E.Size = new System.Drawing.Size(32, 17);
            this.rb_LL_DecimalMinutes_E.TabIndex = 6;
            this.rb_LL_DecimalMinutes_E.TabStop = true;
            this.rb_LL_DecimalMinutes_E.Text = "E";
            this.rb_LL_DecimalMinutes_E.UseVisualStyleBackColor = true;
            this.rb_LL_DecimalMinutes_E.CheckedChanged += new System.EventHandler(this.RB_LL_DecimalMinutes_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.tb_LL_DecimalMinutes_Latitude);
            this.panel4.Controls.Add(this.rb_LL_DecimalMinutes_N);
            this.panel4.Controls.Add(this.rb_LL_DecimalMinutes_S);
            this.panel4.Location = new System.Drawing.Point(6, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(464, 27);
            this.panel4.TabIndex = 0;
            // 
            // tb_LL_DecimalMinutes_Latitude
            // 
            this.tb_LL_DecimalMinutes_Latitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_LL_DecimalMinutes_Latitude.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_LL_DecimalMinutes_Latitude.Location = new System.Drawing.Point(3, 3);
            this.tb_LL_DecimalMinutes_Latitude.MaxLength = 16;
            this.tb_LL_DecimalMinutes_Latitude.Name = "tb_LL_DecimalMinutes_Latitude";
            this.tb_LL_DecimalMinutes_Latitude.Size = new System.Drawing.Size(378, 20);
            this.tb_LL_DecimalMinutes_Latitude.TabIndex = 1;
            this.tb_LL_DecimalMinutes_Latitude.TextChanged += new System.EventHandler(this.TB_LL_DecimalMinutes_Latitude_TextChanged);
            this.tb_LL_DecimalMinutes_Latitude.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_LL_DecimalMinutes_Latitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_DecimalMinutes_KeyPress);
            this.tb_LL_DecimalMinutes_Latitude.Leave += new System.EventHandler(this.Tb_Input_Leave);
            // 
            // rb_LL_DecimalMinutes_N
            // 
            this.rb_LL_DecimalMinutes_N.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_LL_DecimalMinutes_N.AutoSize = true;
            this.rb_LL_DecimalMinutes_N.Checked = true;
            this.rb_LL_DecimalMinutes_N.Location = new System.Drawing.Point(387, 4);
            this.rb_LL_DecimalMinutes_N.Name = "rb_LL_DecimalMinutes_N";
            this.rb_LL_DecimalMinutes_N.Size = new System.Drawing.Size(33, 17);
            this.rb_LL_DecimalMinutes_N.TabIndex = 3;
            this.rb_LL_DecimalMinutes_N.TabStop = true;
            this.rb_LL_DecimalMinutes_N.Text = "N";
            this.rb_LL_DecimalMinutes_N.UseVisualStyleBackColor = true;
            this.rb_LL_DecimalMinutes_N.CheckedChanged += new System.EventHandler(this.RB_LL_DecimalMinutes_CheckedChanged);
            // 
            // rb_LL_DecimalMinutes_S
            // 
            this.rb_LL_DecimalMinutes_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_LL_DecimalMinutes_S.AutoSize = true;
            this.rb_LL_DecimalMinutes_S.Location = new System.Drawing.Point(429, 4);
            this.rb_LL_DecimalMinutes_S.Name = "rb_LL_DecimalMinutes_S";
            this.rb_LL_DecimalMinutes_S.Size = new System.Drawing.Size(32, 17);
            this.rb_LL_DecimalMinutes_S.TabIndex = 4;
            this.rb_LL_DecimalMinutes_S.Text = "S";
            this.rb_LL_DecimalMinutes_S.UseVisualStyleBackColor = true;
            this.rb_LL_DecimalMinutes_S.CheckedChanged += new System.EventHandler(this.RB_LL_DecimalMinutes_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(473, 143);
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
            this.panel5.Controls.Add(this.tb_MGRS_Digraph);
            this.panel5.Controls.Add(this.tb_MGRS_Fraction);
            this.panel5.Controls.Add(this.tb_MGRS_LatZone);
            this.panel5.Controls.Add(this.tb_MGRS_LongZone);
            this.panel5.Location = new System.Drawing.Point(3, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(467, 35);
            this.panel5.TabIndex = 0;
            // 
            // tb_MGRS_Digraph
            // 
            this.tb_MGRS_Digraph.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_MGRS_Digraph.Location = new System.Drawing.Point(80, 8);
            this.tb_MGRS_Digraph.MaxLength = 2;
            this.tb_MGRS_Digraph.Name = "tb_MGRS_Digraph";
            this.tb_MGRS_Digraph.Size = new System.Drawing.Size(34, 20);
            this.tb_MGRS_Digraph.TabIndex = 3;
            this.tb_MGRS_Digraph.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.tb_MGRS_Digraph.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_MGRS_Digraph.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_MGRS_Digraph_KeyPress);
            // 
            // tb_MGRS_Fraction
            // 
            this.tb_MGRS_Fraction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_MGRS_Fraction.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_MGRS_Fraction.Location = new System.Drawing.Point(120, 8);
            this.tb_MGRS_Fraction.MaxLength = 11;
            this.tb_MGRS_Fraction.Name = "tb_MGRS_Fraction";
            this.tb_MGRS_Fraction.Size = new System.Drawing.Size(344, 20);
            this.tb_MGRS_Fraction.TabIndex = 4;
            this.tb_MGRS_Fraction.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.tb_MGRS_Fraction.Enter += new System.EventHandler(this.TB_MGRS_Fraction_Enter);
            this.tb_MGRS_Fraction.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_MGRS_Fraction_KeyPress);
            this.tb_MGRS_Fraction.Leave += new System.EventHandler(this.TB_MGRS_Fraction_Leave);
            // 
            // tb_MGRS_LatZone
            // 
            this.tb_MGRS_LatZone.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_MGRS_LatZone.Location = new System.Drawing.Point(40, 8);
            this.tb_MGRS_LatZone.MaxLength = 1;
            this.tb_MGRS_LatZone.Name = "tb_MGRS_LatZone";
            this.tb_MGRS_LatZone.Size = new System.Drawing.Size(34, 20);
            this.tb_MGRS_LatZone.TabIndex = 2;
            this.tb_MGRS_LatZone.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.tb_MGRS_LatZone.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_MGRS_LatZone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_LatZone_KeyPress);
            // 
            // tb_MGRS_LongZone
            // 
            this.tb_MGRS_LongZone.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_MGRS_LongZone.Location = new System.Drawing.Point(3, 8);
            this.tb_MGRS_LongZone.MaxLength = 2;
            this.tb_MGRS_LongZone.Name = "tb_MGRS_LongZone";
            this.tb_MGRS_LongZone.Size = new System.Drawing.Size(34, 20);
            this.tb_MGRS_LongZone.TabIndex = 1;
            this.tb_MGRS_LongZone.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.tb_MGRS_LongZone.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_MGRS_LongZone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_LongZone_KeyPress);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox9);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.panel8);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(473, 143);
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
            this.panel8.Controls.Add(this.tb_UTM_LatZone);
            this.panel8.Controls.Add(this.tableLayoutPanel1);
            this.panel8.Controls.Add(this.tb_UTM_LongZone);
            this.panel8.Location = new System.Drawing.Point(3, 6);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(464, 35);
            this.panel8.TabIndex = 0;
            // 
            // tb_UTM_LatZone
            // 
            this.tb_UTM_LatZone.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_UTM_LatZone.Location = new System.Drawing.Point(40, 8);
            this.tb_UTM_LatZone.MaxLength = 1;
            this.tb_UTM_LatZone.Name = "tb_UTM_LatZone";
            this.tb_UTM_LatZone.Size = new System.Drawing.Size(34, 20);
            this.tb_UTM_LatZone.TabIndex = 2;
            this.tb_UTM_LatZone.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.tb_UTM_LatZone.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_UTM_LatZone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_LatZone_KeyPress);
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
            this.tableLayoutPanel1.Controls.Add(this.tb_UTM_Easting, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_UTM_Northing, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(80, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(378, 26);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tb_UTM_Easting
            // 
            this.tb_UTM_Easting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_UTM_Easting.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_UTM_Easting.Location = new System.Drawing.Point(3, 3);
            this.tb_UTM_Easting.MaxLength = 16;
            this.tb_UTM_Easting.Name = "tb_UTM_Easting";
            this.tb_UTM_Easting.Size = new System.Drawing.Size(183, 20);
            this.tb_UTM_Easting.TabIndex = 3;
            this.tb_UTM_Easting.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.tb_UTM_Easting.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_UTM_Easting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RB_UTM_Northing_Easting_KeyPress);
            this.tb_UTM_Easting.Leave += new System.EventHandler(this.Tb_Input_Leave);
            // 
            // tb_UTM_Northing
            // 
            this.tb_UTM_Northing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_UTM_Northing.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_UTM_Northing.Location = new System.Drawing.Point(192, 3);
            this.tb_UTM_Northing.MaxLength = 16;
            this.tb_UTM_Northing.Name = "tb_UTM_Northing";
            this.tb_UTM_Northing.Size = new System.Drawing.Size(183, 20);
            this.tb_UTM_Northing.TabIndex = 4;
            this.tb_UTM_Northing.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.tb_UTM_Northing.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_UTM_Northing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RB_UTM_Northing_Easting_KeyPress);
            this.tb_UTM_Northing.Leave += new System.EventHandler(this.Tb_Input_Leave);
            // 
            // tb_UTM_LongZone
            // 
            this.tb_UTM_LongZone.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_UTM_LongZone.Location = new System.Drawing.Point(3, 8);
            this.tb_UTM_LongZone.MaxLength = 2;
            this.tb_UTM_LongZone.Name = "tb_UTM_LongZone";
            this.tb_UTM_LongZone.Size = new System.Drawing.Size(34, 20);
            this.tb_UTM_LongZone.TabIndex = 1;
            this.tb_UTM_LongZone.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.tb_UTM_LongZone.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_UTM_LongZone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_LongZone_KeyPress);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lbl_BEPosition);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.panel9);
            this.tabPage4.Controls.Add(this.panel10);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(473, 143);
            this.tabPage4.TabIndex = 5;
            this.tabPage4.Text = "Bullseye";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lbl_BEPosition
            // 
            this.lbl_BEPosition.AutoSize = true;
            this.lbl_BEPosition.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BEPosition.Location = new System.Drawing.Point(7, 99);
            this.lbl_BEPosition.Name = "lbl_BEPosition";
            this.lbl_BEPosition.Size = new System.Drawing.Size(63, 16);
            this.lbl_BEPosition.TabIndex = 4;
            this.lbl_BEPosition.Text = "Not Set";
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
            this.panel9.Controls.Add(this.tb_Bullseye_Range);
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
            // tb_Bullseye_Range
            // 
            this.tb_Bullseye_Range.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Bullseye_Range.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Bullseye_Range.Location = new System.Drawing.Point(3, 3);
            this.tb_Bullseye_Range.MaxLength = 6;
            this.tb_Bullseye_Range.Name = "tb_Bullseye_Range";
            this.tb_Bullseye_Range.Size = new System.Drawing.Size(118, 20);
            this.tb_Bullseye_Range.TabIndex = 2;
            this.tb_Bullseye_Range.TextChanged += new System.EventHandler(this.Tb_Bullseye_Range_TextChanged);
            this.tb_Bullseye_Range.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_Bullseye_Range.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_Bullseye_Range_KeyPress);
            this.tb_Bullseye_Range.Leave += new System.EventHandler(this.Tb_Input_Leave);
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.Controls.Add(this.label1);
            this.panel10.Controls.Add(this.tb_Bullseye_Bearing);
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
            // tb_Bullseye_Bearing
            // 
            this.tb_Bullseye_Bearing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Bullseye_Bearing.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Bullseye_Bearing.Location = new System.Drawing.Point(3, 3);
            this.tb_Bullseye_Bearing.MaxLength = 5;
            this.tb_Bullseye_Bearing.Name = "tb_Bullseye_Bearing";
            this.tb_Bullseye_Bearing.Size = new System.Drawing.Size(118, 20);
            this.tb_Bullseye_Bearing.TabIndex = 1;
            this.tb_Bullseye_Bearing.TextChanged += new System.EventHandler(this.Tb_Bullseye_Bearing_TextChanged);
            this.tb_Bullseye_Bearing.Enter += new System.EventHandler(this.Tb_Input_Enter);
            this.tb_Bullseye_Bearing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_Bullseye_Bearing_KeyPress);
            this.tb_Bullseye_Bearing.Leave += new System.EventHandler(this.Tb_Input_Leave);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pb_Transfer,
            this.lbl_DCS_Status,
            this.lbl_Error});
            this.statusStrip1.Location = new System.Drawing.Point(0, 474);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1189, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pb_Transfer
            // 
            this.pb_Transfer.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pb_Transfer.Name = "pb_Transfer";
            this.pb_Transfer.Size = new System.Drawing.Size(300, 16);
            this.pb_Transfer.Step = 1;
            this.pb_Transfer.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb_Transfer.Visible = false;
            // 
            // lbl_DCS_Status
            // 
            this.lbl_DCS_Status.BackColor = System.Drawing.Color.Yellow;
            this.lbl_DCS_Status.Name = "lbl_DCS_Status";
            this.lbl_DCS_Status.Size = new System.Drawing.Size(127, 17);
            this.lbl_DCS_Status.Text = "DCS not connected";
            this.lbl_DCS_Status.BackColorChanged += new System.EventHandler(this.Lbl_DCS_Status_BackColorChanged);
            this.lbl_DCS_Status.Click += new System.EventHandler(this.FetchCoordinatesControl_Click);
            // 
            // lbl_Error
            // 
            this.lbl_Error.BackColor = System.Drawing.Color.Tomato;
            this.lbl_Error.Name = "lbl_Error";
            this.lbl_Error.Size = new System.Drawing.Size(67, 17);
            this.lbl_Error.Text = "<ERROR>";
            this.lbl_Error.Visible = false;
            // 
            // dgv_CoordinateList
            // 
            this.dgv_CoordinateList.AllowUserToAddRows = false;
            this.dgv_CoordinateList.AllowUserToDeleteRows = false;
            this.dgv_CoordinateList.AllowUserToResizeRows = false;
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
            this.dgv_CoordinateList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_CoordinateList.Location = new System.Drawing.Point(505, 66);
            this.dgv_CoordinateList.Name = "dgv_CoordinateList";
            this.dgv_CoordinateList.ReadOnly = true;
            this.dgv_CoordinateList.Size = new System.Drawing.Size(672, 376);
            this.dgv_CoordinateList.TabIndex = 30;
            this.dgv_CoordinateList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_CoordinateList_CellContentClick);
            this.dgv_CoordinateList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_CoordinateList_CellDoubleClick);
            this.dgv_CoordinateList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dgv_CoordinateList_KeyDown);
            // 
            // colId
            // 
            this.colId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colId.DataPropertyName = "ID";
            this.colId.Frozen = true;
            this.colId.HeaderText = "#";
            this.colId.MinimumWidth = 35;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colId.ToolTipText = "ID of the point.";
            this.colId.Width = 35;
            // 
            // colLabel
            // 
            this.colLabel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLabel.DataPropertyName = "Name";
            this.colLabel.FillWeight = 40F;
            this.colLabel.HeaderText = "Label";
            this.colLabel.MaxInputLength = 12;
            this.colLabel.MinimumWidth = 30;
            this.colLabel.Name = "colLabel";
            this.colLabel.ReadOnly = true;
            this.colLabel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colLabel.ToolTipText = "User defined label for the point";
            // 
            // colCoordinates
            // 
            this.colCoordinates.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCoordinates.DataPropertyName = "CoordinateStr";
            this.colCoordinates.FillWeight = 40F;
            this.colCoordinates.HeaderText = "Coordinates";
            this.colCoordinates.MinimumWidth = 180;
            this.colCoordinates.Name = "colCoordinates";
            this.colCoordinates.ReadOnly = true;
            this.colCoordinates.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCoordinates.ToolTipText = "Coordinate of the point.";
            // 
            // colAltitude
            // 
            this.colAltitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAltitude.DataPropertyName = "Altitude";
            this.colAltitude.FillWeight = 20F;
            this.colAltitude.HeaderText = "Altitude";
            this.colAltitude.MaxInputLength = 5;
            this.colAltitude.MinimumWidth = 100;
            this.colAltitude.Name = "colAltitude";
            this.colAltitude.ReadOnly = true;
            this.colAltitude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAltitude.ToolTipText = "Altitude of the point.";
            // 
            // colXFer
            // 
            this.colXFer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colXFer.DataPropertyName = "XFER";
            this.colXFer.FalseValue = "";
            this.colXFer.HeaderText = "XFER";
            this.colXFer.IndeterminateValue = "";
            this.colXFer.MinimumWidth = 40;
            this.colXFer.Name = "colXFer";
            this.colXFer.ReadOnly = true;
            this.colXFer.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colXFer.ToolTipText = "Check to enable transfer to DCS.";
            this.colXFer.TrueValue = "";
            this.colXFer.Width = 41;
            // 
            // colDelete
            // 
            this.colDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colDelete.HeaderText = "Del";
            this.colDelete.MinimumWidth = 30;
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDelete.Text = "-";
            this.colDelete.ToolTipText = "Remove a point from the list.";
            this.colDelete.UseColumnTextForButtonValue = true;
            this.colDelete.Width = 30;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_FileMenu,
            this.tsmi_DCSMenu,
            this.settingsToolStripMenuItem,
            this.miscToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1189, 24);
            this.menuStrip1.TabIndex = 31;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmi_FileMenu
            // 
            this.tsmi_FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_recentFiles,
            this.tsmi_Load,
            this.toolStripSeparator7,
            this.tsmi_SaveAs,
            this.toolStripSeparator6,
            this.tsmi_exit});
            this.tsmi_FileMenu.Name = "tsmi_FileMenu";
            this.tsmi_FileMenu.Size = new System.Drawing.Size(40, 20);
            this.tsmi_FileMenu.Text = "File";
            // 
            // tsmi_recentFiles
            // 
            this.tsmi_recentFiles.Name = "tsmi_recentFiles";
            this.tsmi_recentFiles.Size = new System.Drawing.Size(149, 22);
            this.tsmi_recentFiles.Text = "Recent Files";
            // 
            // tsmi_Load
            // 
            this.tsmi_Load.Name = "tsmi_Load";
            this.tsmi_Load.Size = new System.Drawing.Size(149, 22);
            this.tsmi_Load.Text = "Load file...";
            this.tsmi_Load.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(146, 6);
            // 
            // tsmi_SaveAs
            // 
            this.tsmi_SaveAs.Name = "tsmi_SaveAs";
            this.tsmi_SaveAs.Size = new System.Drawing.Size(149, 22);
            this.tsmi_SaveAs.Text = "Save As...";
            this.tsmi_SaveAs.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(146, 6);
            // 
            // tsmi_exit
            // 
            this.tsmi_exit.Name = "tsmi_exit";
            this.tsmi_exit.Size = new System.Drawing.Size(149, 22);
            this.tsmi_exit.Text = "Exit";
            this.tsmi_exit.Click += new System.EventHandler(this.tsmi_exit_Click);
            // 
            // tsmi_DCSMenu
            // 
            this.tsmi_DCSMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Transfer,
            this.tsmi_StopTransfer,
            this.toolStripSeparator8,
            this.tsmi_FetchCoordinates,
            this.tsmi_ImportUnits,
            this.toolStripSeparator9,
            this.tsmi_AircraftMenu});
            this.tsmi_DCSMenu.Name = "tsmi_DCSMenu";
            this.tsmi_DCSMenu.Size = new System.Drawing.Size(45, 20);
            this.tsmi_DCSMenu.Text = "DCS";
            // 
            // tsmi_Transfer
            // 
            this.tsmi_Transfer.Name = "tsmi_Transfer";
            this.tsmi_Transfer.Size = new System.Drawing.Size(189, 22);
            this.tsmi_Transfer.Text = "Transfer";
            this.tsmi_Transfer.Click += new System.EventHandler(this.TransferControl_Click);
            // 
            // tsmi_StopTransfer
            // 
            this.tsmi_StopTransfer.Name = "tsmi_StopTransfer";
            this.tsmi_StopTransfer.Size = new System.Drawing.Size(189, 22);
            this.tsmi_StopTransfer.Text = "Stop Transfer";
            this.tsmi_StopTransfer.Click += new System.EventHandler(this.StopTransferControl_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(186, 6);
            // 
            // tsmi_FetchCoordinates
            // 
            this.tsmi_FetchCoordinates.Name = "tsmi_FetchCoordinates";
            this.tsmi_FetchCoordinates.Size = new System.Drawing.Size(189, 22);
            this.tsmi_FetchCoordinates.Text = "Fetch Coordinates";
            this.tsmi_FetchCoordinates.Click += new System.EventHandler(this.FetchCoordinatesControl_Click);
            // 
            // tsmi_ImportUnits
            // 
            this.tsmi_ImportUnits.Name = "tsmi_ImportUnits";
            this.tsmi_ImportUnits.Size = new System.Drawing.Size(189, 22);
            this.tsmi_ImportUnits.Text = "Import Units...";
            this.tsmi_ImportUnits.Click += new System.EventHandler(this.ImportUnitsControl_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(186, 6);
            // 
            // tsmi_AircraftMenu
            // 
            this.tsmi_AircraftMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Auto,
            this.toolStripSeparator1,
            this.tsmi_A10C,
            this.tsmi_AH64Menu,
            this.tsmi_AV8B,
            this.tsmi_F15EMenu,
            this.tsmi_F16Menu,
            this.tsmi_F18,
            this.tsmi_JF17Menu,
            this.tsmi_KA50,
            this.tsmi_M2000,
            this.tsmi_OH58DMenu});
            this.tsmi_AircraftMenu.Name = "tsmi_AircraftMenu";
            this.tsmi_AircraftMenu.Size = new System.Drawing.Size(189, 22);
            this.tsmi_AircraftMenu.Text = "Aircraft";
            // 
            // tsmi_Auto
            // 
            this.tsmi_Auto.Checked = true;
            this.tsmi_Auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmi_Auto.Name = "tsmi_Auto";
            this.tsmi_Auto.Size = new System.Drawing.Size(180, 22);
            this.tsmi_Auto.Text = "Auto";
            this.tsmi_Auto.Click += new System.EventHandler(this.Tsmi_Aircraft_Auto_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmi_AH64Menu
            // 
            this.tsmi_AH64Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_AH64,
            this.tsmi_AH64_ClearPoints,
            this.tsmi_AH64_DTC});
            this.tsmi_AH64Menu.Name = "tsmi_AH64Menu";
            this.tsmi_AH64Menu.Size = new System.Drawing.Size(180, 22);
            this.tsmi_AH64Menu.Text = "AH64";
            // 
            // tsmi_AH64
            // 
            this.tsmi_AH64.Name = "tsmi_AH64";
            this.tsmi_AH64.Size = new System.Drawing.Size(162, 22);
            this.tsmi_AH64.Text = "AH64";
            this.tsmi_AH64.Click += new System.EventHandler(this.Tsmi_AircraftSelection_Click);
            // 
            // tsmi_AH64_ClearPoints
            // 
            this.tsmi_AH64_ClearPoints.Name = "tsmi_AH64_ClearPoints";
            this.tsmi_AH64_ClearPoints.Size = new System.Drawing.Size(162, 22);
            this.tsmi_AH64_ClearPoints.Text = "Clear Points...";
            this.tsmi_AH64_ClearPoints.Click += new System.EventHandler(this.Tsmi_AH64_ClearPoints_Click);
            // 
            // tsmi_AH64_DTC
            // 
            this.tsmi_AH64_DTC.Name = "tsmi_AH64_DTC";
            this.tsmi_AH64_DTC.Size = new System.Drawing.Size(162, 22);
            this.tsmi_AH64_DTC.Text = "DTC...";
            this.tsmi_AH64_DTC.Click += new System.EventHandler(this.Tsmi_AH64_DTC_Click);
            // 
            // tsmi_AV8B
            // 
            this.tsmi_AV8B.Name = "tsmi_AV8B";
            this.tsmi_AV8B.Size = new System.Drawing.Size(180, 22);
            this.tsmi_AV8B.Text = "AV8B";
            this.tsmi_AV8B.Click += new System.EventHandler(this.Tsmi_AircraftSelection_Click);
            // 
            // tsmi_F15EMenu
            // 
            this.tsmi_F15EMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_F15E_Pilot,
            this.tsmi_F15E_WSO});
            this.tsmi_F15EMenu.Name = "tsmi_F15EMenu";
            this.tsmi_F15EMenu.Size = new System.Drawing.Size(180, 22);
            this.tsmi_F15EMenu.Text = "F15E";
            // 
            // tsmi_F15E_Pilot
            // 
            this.tsmi_F15E_Pilot.Name = "tsmi_F15E_Pilot";
            this.tsmi_F15E_Pilot.Size = new System.Drawing.Size(140, 22);
            this.tsmi_F15E_Pilot.Text = "F15E Pilot";
            this.tsmi_F15E_Pilot.Click += new System.EventHandler(this.Tsmi_AircraftSelection_Click);
            // 
            // tsmi_F15E_WSO
            // 
            this.tsmi_F15E_WSO.Name = "tsmi_F15E_WSO";
            this.tsmi_F15E_WSO.Size = new System.Drawing.Size(140, 22);
            this.tsmi_F15E_WSO.Text = "F15E WSO";
            this.tsmi_F15E_WSO.Click += new System.EventHandler(this.Tsmi_AircraftSelection_Click);
            // 
            // tsmi_F16Menu
            // 
            this.tsmi_F16Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_F16,
            this.tsmi_F16_SetFirstPoint});
            this.tsmi_F16Menu.Name = "tsmi_F16Menu";
            this.tsmi_F16Menu.Size = new System.Drawing.Size(180, 22);
            this.tsmi_F16Menu.Text = "F16";
            // 
            // tsmi_F16
            // 
            this.tsmi_F16.Name = "tsmi_F16";
            this.tsmi_F16.Size = new System.Drawing.Size(171, 22);
            this.tsmi_F16.Text = "F16";
            this.tsmi_F16.Click += new System.EventHandler(this.Tsmi_AircraftSelection_Click);
            // 
            // tsmi_F16_SetFirstPoint
            // 
            this.tsmi_F16_SetFirstPoint.Name = "tsmi_F16_SetFirstPoint";
            this.tsmi_F16_SetFirstPoint.Size = new System.Drawing.Size(171, 22);
            this.tsmi_F16_SetFirstPoint.Text = "Set first point...";
            this.tsmi_F16_SetFirstPoint.Click += new System.EventHandler(this.Tsmi_F16_SetFirstPoint_Click);
            // 
            // tsmi_F18
            // 
            this.tsmi_F18.Name = "tsmi_F18";
            this.tsmi_F18.Size = new System.Drawing.Size(180, 22);
            this.tsmi_F18.Text = "F18";
            this.tsmi_F18.Click += new System.EventHandler(this.Tsmi_AircraftSelection_Click);
            // 
            // tsmi_JF17Menu
            // 
            this.tsmi_JF17Menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_JF17,
            this.tsmi_JF17_SetFirstPoint});
            this.tsmi_JF17Menu.Name = "tsmi_JF17Menu";
            this.tsmi_JF17Menu.Size = new System.Drawing.Size(180, 22);
            this.tsmi_JF17Menu.Text = "JF17";
            // 
            // tsmi_JF17
            // 
            this.tsmi_JF17.Name = "tsmi_JF17";
            this.tsmi_JF17.Size = new System.Drawing.Size(171, 22);
            this.tsmi_JF17.Text = "JF17";
            this.tsmi_JF17.Click += new System.EventHandler(this.Tsmi_AircraftSelection_Click);
            // 
            // tsmi_JF17_SetFirstPoint
            // 
            this.tsmi_JF17_SetFirstPoint.Name = "tsmi_JF17_SetFirstPoint";
            this.tsmi_JF17_SetFirstPoint.Size = new System.Drawing.Size(171, 22);
            this.tsmi_JF17_SetFirstPoint.Text = "Set first point...";
            this.tsmi_JF17_SetFirstPoint.Click += new System.EventHandler(this.Tsmi_JF17_SetFirstPoint_Click);
            // 
            // tsmi_KA50
            // 
            this.tsmi_KA50.Name = "tsmi_KA50";
            this.tsmi_KA50.Size = new System.Drawing.Size(180, 22);
            this.tsmi_KA50.Text = "KA50";
            this.tsmi_KA50.Click += new System.EventHandler(this.Tsmi_AircraftSelection_Click);
            // 
            // tsmi_M2000
            // 
            this.tsmi_M2000.Name = "tsmi_M2000";
            this.tsmi_M2000.Size = new System.Drawing.Size(180, 22);
            this.tsmi_M2000.Text = "M2000";
            this.tsmi_M2000.Click += new System.EventHandler(this.Tsmi_AircraftSelection_Click);
            // 
            // tsmi_OH58DMenu
            // 
            this.tsmi_OH58DMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_OH58D});
            this.tsmi_OH58DMenu.Name = "tsmi_OH58DMenu";
            this.tsmi_OH58DMenu.Size = new System.Drawing.Size(180, 22);
            this.tsmi_OH58DMenu.Text = "OH58D";
            // 
            // tsmi_OH58D
            // 
            this.tsmi_OH58D.Name = "tsmi_OH58D";
            this.tsmi_OH58D.Size = new System.Drawing.Size(118, 22);
            this.tsmi_OH58D.Text = "OH58D";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_DCSMainScreenMenu,
            this.tsmi_ReticleSetting,
            this.tsmi_CameraPositionModeMenu,
            this.toolStripSeparator10,
            this.tsmi_AlwaysOnTop,
            this.tsmi_TransparencyMenu,
            this.toolStripSeparator11,
            this.tsmi_CheckForUpdates,
            this.tsmi_AutoCheckForUpdates,
            this.toolStripSeparator12,
            this.tsmi_changeBaseDirectory});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // tsmi_DCSMainScreenMenu
            // 
            this.tsmi_DCSMainScreenMenu.Name = "tsmi_DCSMainScreenMenu";
            this.tsmi_DCSMainScreenMenu.Size = new System.Drawing.Size(229, 22);
            this.tsmi_DCSMainScreenMenu.Text = "DCS Main Screen";
            // 
            // tsmi_ReticleSetting
            // 
            this.tsmi_ReticleSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Reticle_WhenInF10Map,
            this.tsmi_Reticle_Always,
            this.tsmi_Reticle_Never});
            this.tsmi_ReticleSetting.Name = "tsmi_ReticleSetting";
            this.tsmi_ReticleSetting.Size = new System.Drawing.Size(229, 22);
            this.tsmi_ReticleSetting.Text = "Reticle Setting";
            // 
            // tsmi_Reticle_WhenInF10Map
            // 
            this.tsmi_Reticle_WhenInF10Map.Name = "tsmi_Reticle_WhenInF10Map";
            this.tsmi_Reticle_WhenInF10Map.Size = new System.Drawing.Size(183, 22);
            this.tsmi_Reticle_WhenInF10Map.Text = "When in F10 Map";
            this.tsmi_Reticle_WhenInF10Map.Click += new System.EventHandler(this.Tsmi_Reticle_WhenInF10Map_Click);
            // 
            // tsmi_Reticle_Always
            // 
            this.tsmi_Reticle_Always.Name = "tsmi_Reticle_Always";
            this.tsmi_Reticle_Always.Size = new System.Drawing.Size(183, 22);
            this.tsmi_Reticle_Always.Text = "Always";
            this.tsmi_Reticle_Always.Click += new System.EventHandler(this.Tsmi_Reticle_Always_Click);
            // 
            // tsmi_Reticle_Never
            // 
            this.tsmi_Reticle_Never.Name = "tsmi_Reticle_Never";
            this.tsmi_Reticle_Never.Size = new System.Drawing.Size(183, 22);
            this.tsmi_Reticle_Never.Text = "Never";
            this.tsmi_Reticle_Never.Click += new System.EventHandler(this.Tsmi_Reticle_Never_Click);
            // 
            // tsmi_CameraPositionModeMenu
            // 
            this.tsmi_CameraPositionModeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_TerrainElevationUnderCamera,
            this.tsmi_CameraAltitude});
            this.tsmi_CameraPositionModeMenu.Name = "tsmi_CameraPositionModeMenu";
            this.tsmi_CameraPositionModeMenu.Size = new System.Drawing.Size(229, 22);
            this.tsmi_CameraPositionModeMenu.Text = "Camera Position Mode";
            // 
            // tsmi_TerrainElevationUnderCamera
            // 
            this.tsmi_TerrainElevationUnderCamera.Checked = true;
            this.tsmi_TerrainElevationUnderCamera.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmi_TerrainElevationUnderCamera.Name = "tsmi_TerrainElevationUnderCamera";
            this.tsmi_TerrainElevationUnderCamera.Size = new System.Drawing.Size(273, 22);
            this.tsmi_TerrainElevationUnderCamera.Text = "Terrain elevation under Camera";
            this.tsmi_TerrainElevationUnderCamera.Click += new System.EventHandler(this.Tsmi_TerrainElevationUnderCamera_Click);
            // 
            // tsmi_CameraAltitude
            // 
            this.tsmi_CameraAltitude.Name = "tsmi_CameraAltitude";
            this.tsmi_CameraAltitude.Size = new System.Drawing.Size(273, 22);
            this.tsmi_CameraAltitude.Text = "Camera altitude";
            this.tsmi_CameraAltitude.Click += new System.EventHandler(this.Tsmi_CameraAltitude_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(226, 6);
            // 
            // tsmi_AlwaysOnTop
            // 
            this.tsmi_AlwaysOnTop.Name = "tsmi_AlwaysOnTop";
            this.tsmi_AlwaysOnTop.Size = new System.Drawing.Size(229, 22);
            this.tsmi_AlwaysOnTop.Text = "Always on top";
            this.tsmi_AlwaysOnTop.Click += new System.EventHandler(this.Control_AlwaysOnTop_Click);
            // 
            // tsmi_TransparencyMenu
            // 
            this.tsmi_TransparencyMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_opaque,
            this.tsmi_Opacity75,
            this.tsmi_Opacity50,
            this.tsmi_Opacity25});
            this.tsmi_TransparencyMenu.Name = "tsmi_TransparencyMenu";
            this.tsmi_TransparencyMenu.Size = new System.Drawing.Size(229, 22);
            this.tsmi_TransparencyMenu.Text = "Transparency";
            // 
            // tsmi_opaque
            // 
            this.tsmi_opaque.Checked = true;
            this.tsmi_opaque.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmi_opaque.Name = "tsmi_opaque";
            this.tsmi_opaque.Size = new System.Drawing.Size(124, 22);
            this.tsmi_opaque.Text = "Opaque";
            this.tsmi_opaque.Click += new System.EventHandler(this.Tsmi_Opaque_Click);
            // 
            // tsmi_Opacity75
            // 
            this.tsmi_Opacity75.Name = "tsmi_Opacity75";
            this.tsmi_Opacity75.Size = new System.Drawing.Size(124, 22);
            this.tsmi_Opacity75.Text = "25%";
            this.tsmi_Opacity75.Click += new System.EventHandler(this.Tsmi_Opacity75_Click);
            // 
            // tsmi_Opacity50
            // 
            this.tsmi_Opacity50.Name = "tsmi_Opacity50";
            this.tsmi_Opacity50.Size = new System.Drawing.Size(124, 22);
            this.tsmi_Opacity50.Text = "50%";
            this.tsmi_Opacity50.Click += new System.EventHandler(this.Tsmi_Opacity50_Click);
            // 
            // tsmi_Opacity25
            // 
            this.tsmi_Opacity25.Name = "tsmi_Opacity25";
            this.tsmi_Opacity25.Size = new System.Drawing.Size(124, 22);
            this.tsmi_Opacity25.Text = "75%";
            this.tsmi_Opacity25.Click += new System.EventHandler(this.Tsmi_Opacity25_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(226, 6);
            // 
            // tsmi_CheckForUpdates
            // 
            this.tsmi_CheckForUpdates.Name = "tsmi_CheckForUpdates";
            this.tsmi_CheckForUpdates.Size = new System.Drawing.Size(229, 22);
            this.tsmi_CheckForUpdates.Text = "Check for Updates";
            this.tsmi_CheckForUpdates.Click += new System.EventHandler(this.Tsmi_CheckForUpdates_Click);
            // 
            // tsmi_AutoCheckForUpdates
            // 
            this.tsmi_AutoCheckForUpdates.Name = "tsmi_AutoCheckForUpdates";
            this.tsmi_AutoCheckForUpdates.Size = new System.Drawing.Size(229, 22);
            this.tsmi_AutoCheckForUpdates.Text = "Autocheck for Updates";
            this.tsmi_AutoCheckForUpdates.Click += new System.EventHandler(this.tsmi_AutoCheckForUpdates_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(226, 6);
            // 
            // tsmi_changeBaseDirectory
            // 
            this.tsmi_changeBaseDirectory.Name = "tsmi_changeBaseDirectory";
            this.tsmi_changeBaseDirectory.Size = new System.Drawing.Size(229, 22);
            this.tsmi_changeBaseDirectory.Text = "Change Base Directory...";
            this.tsmi_changeBaseDirectory.Click += new System.EventHandler(this.tsmi_changeBaseDirectory_Click);
            // 
            // miscToolStripMenuItem
            // 
            this.miscToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_execute});
            this.miscToolStripMenuItem.Name = "miscToolStripMenuItem";
            this.miscToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.miscToolStripMenuItem.Text = "Misc";
            // 
            // tsmi_execute
            // 
            this.tsmi_execute.Name = "tsmi_execute";
            this.tsmi_execute.Size = new System.Drawing.Size(136, 22);
            this.tsmi_execute.Text = "Execute...";
            this.tsmi_execute.Click += new System.EventHandler(this.Tsmi_execute_Click);
            // 
            // tmr250ms
            // 
            this.tmr250ms.Interval = 250;
            this.tmr250ms.Tick += new System.EventHandler(this.Tmr250ms_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Transfer,
            this.btn_Stop,
            this.toolStripSeparator2,
            this.btn_FetchCoordinates,
            this.btn_ImportUnits,
            this.btn_SetBE,
            this.toolStripSeparator3,
            this.btn_AlwaysOnTop,
            this.toolStripSeparator4,
            this.btn_Add,
            this.btn_Edit,
            this.toolStripSeparator5,
            this.btn_MoveDown,
            this.btn_MoveUp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1189, 39);
            this.toolStrip1.TabIndex = 32;
            this.toolStrip1.Text = "Tools";
            // 
            // btn_Transfer
            // 
            this.btn_Transfer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Transfer.Image = ((System.Drawing.Image)(resources.GetObject("btn_Transfer.Image")));
            this.btn_Transfer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Transfer.Name = "btn_Transfer";
            this.btn_Transfer.Size = new System.Drawing.Size(36, 36);
            this.btn_Transfer.Text = "Transfer";
            this.btn_Transfer.Click += new System.EventHandler(this.TransferControl_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Stop.Image = ((System.Drawing.Image)(resources.GetObject("btn_Stop.Image")));
            this.btn_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(36, 36);
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.Click += new System.EventHandler(this.StopTransferControl_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // btn_FetchCoordinates
            // 
            this.btn_FetchCoordinates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_FetchCoordinates.Image = ((System.Drawing.Image)(resources.GetObject("btn_FetchCoordinates.Image")));
            this.btn_FetchCoordinates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_FetchCoordinates.Name = "btn_FetchCoordinates";
            this.btn_FetchCoordinates.Size = new System.Drawing.Size(36, 36);
            this.btn_FetchCoordinates.Text = "Fetch coordinates";
            this.btn_FetchCoordinates.Click += new System.EventHandler(this.FetchCoordinatesControl_Click);
            // 
            // btn_ImportUnits
            // 
            this.btn_ImportUnits.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_ImportUnits.Image = ((System.Drawing.Image)(resources.GetObject("btn_ImportUnits.Image")));
            this.btn_ImportUnits.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_ImportUnits.Name = "btn_ImportUnits";
            this.btn_ImportUnits.Size = new System.Drawing.Size(36, 36);
            this.btn_ImportUnits.Text = "Import";
            this.btn_ImportUnits.Click += new System.EventHandler(this.ImportUnitsControl_Click);
            // 
            // btn_SetBE
            // 
            this.btn_SetBE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_SetBE.Image = ((System.Drawing.Image)(resources.GetObject("btn_SetBE.Image")));
            this.btn_SetBE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_SetBE.Name = "btn_SetBE";
            this.btn_SetBE.Size = new System.Drawing.Size(36, 36);
            this.btn_SetBE.Text = "Set Bullseye";
            this.btn_SetBE.Click += new System.EventHandler(this.Btn_SetBE_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // btn_AlwaysOnTop
            // 
            this.btn_AlwaysOnTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_AlwaysOnTop.Image = ((System.Drawing.Image)(resources.GetObject("btn_AlwaysOnTop.Image")));
            this.btn_AlwaysOnTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_AlwaysOnTop.Name = "btn_AlwaysOnTop";
            this.btn_AlwaysOnTop.Size = new System.Drawing.Size(36, 36);
            this.btn_AlwaysOnTop.Text = "Always on top";
            this.btn_AlwaysOnTop.Click += new System.EventHandler(this.Control_AlwaysOnTop_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // btn_Add
            // 
            this.btn_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Add.Image = ((System.Drawing.Image)(resources.GetObject("btn_Add.Image")));
            this.btn_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(36, 36);
            this.btn_Add.Text = "Add";
            this.btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // btn_Edit
            // 
            this.btn_Edit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Edit.Image = ((System.Drawing.Image)(resources.GetObject("btn_Edit.Image")));
            this.btn_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(36, 36);
            this.btn_Edit.Text = "Edit";
            this.btn_Edit.Click += new System.EventHandler(this.Btn_Edit_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // btn_MoveDown
            // 
            this.btn_MoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_MoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btn_MoveDown.Image")));
            this.btn_MoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_MoveDown.Name = "btn_MoveDown";
            this.btn_MoveDown.Size = new System.Drawing.Size(36, 36);
            this.btn_MoveDown.Text = "Move Down";
            this.btn_MoveDown.Click += new System.EventHandler(this.Btn_MoveDown_Click);
            // 
            // btn_MoveUp
            // 
            this.btn_MoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_MoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btn_MoveUp.Image")));
            this.btn_MoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_MoveUp.Name = "btn_MoveUp";
            this.btn_MoveUp.Size = new System.Drawing.Size(36, 36);
            this.btn_MoveUp.Text = "Move Up";
            this.btn_MoveUp.Click += new System.EventHandler(this.Btn_MoveUp_Click);
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(505, 445);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(616, 26);
            this.label19.TabIndex = 33;
            this.label19.Text = resources.GetString("label19.Text");
            // 
            // tsmi_A10C
            // 
            this.tsmi_A10C.Name = "tsmi_A10C";
            this.tsmi_A10C.Size = new System.Drawing.Size(180, 22);
            this.tsmi_A10C.Text = "A10C";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 496);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgv_CoordinateList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.GrpBox_Input);
            this.Controls.Add(this.GrpBox_Output);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Coordinate Converter";
            this.GrpBox_Output.ResumeLayout(false);
            this.GrpBox_Output.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_LL_DecimalMinutes_Precision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_LL_DecimalSeconds_Precision)).EndInit();
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
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ToolStripMenuItem tsmi_OH58DMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_OH58D;
        #endregion
        private System.Windows.Forms.GroupBox GrpBox_Output;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_Out_Bullseye;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_Out_UTM;
        private System.Windows.Forms.TextBox tb_Out_MGRS;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_Out_LL_DecimalSeconds;
        private System.Windows.Forms.TextBox tb_Out_LL_DecimalMinutes;
        private System.Windows.Forms.GroupBox GrpBox_Input;
        private System.Windows.Forms.TabControl TC_Input;
        private System.Windows.Forms.TabPage TabPage_LatLon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_LL_DecimalSeconds_Longitude;
        private System.Windows.Forms.RadioButton rb_LL_DecimalSeconds_W;
        private System.Windows.Forms.RadioButton rb_LL_DecimalSeconds_E;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tb_LL_DecimalSeconds_Latitude;
        private System.Windows.Forms.RadioButton rb_LL_DecimalSeconds_N;
        private System.Windows.Forms.RadioButton rb_LL_DecimalSeconds_S;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox tb_LL_DecimalMinutes_Longitude;
        private System.Windows.Forms.RadioButton rb_LL_DecimalMinutes_W;
        private System.Windows.Forms.RadioButton rb_LL_DecimalMinutes_E;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tb_LL_DecimalMinutes_Latitude;
        private System.Windows.Forms.RadioButton rb_LL_DecimalMinutes_N;
        private System.Windows.Forms.RadioButton rb_LL_DecimalMinutes_S;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox tb_UTM_Northing;
        private System.Windows.Forms.TextBox tb_UTM_Easting;
        private System.Windows.Forms.TextBox tb_UTM_LongZone;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Bullseye_Range;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Bullseye_Bearing;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tb_UTM_LatZone;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox tb_MGRS_Fraction;
        private System.Windows.Forms.TextBox tb_MGRS_LatZone;
        private System.Windows.Forms.TextBox tb_MGRS_LongZone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_MGRS_Digraph;
        private Label lbl_BEPosition;
        private Label label4;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lbl_Error;
        private DataGridView dgv_CoordinateList;
        private RadioButton rb_Format_LL_DecimalSeconds;
        private RadioButton rb_Format_Bullseye;
        private RadioButton rb_Format_UTM;
        private RadioButton rb_Format_MGRS;
        private RadioButton rb_Format_LL_DecimalMinutes;
        private NumericUpDown nud_MGRS_Precision;
        private Label label15;
        private ComboBox cb_AltitudeUnit;
        private TextBox tb_Altitude;
        private Label label16;
        private TextBox tb_Label;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem tsmi_FileMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_DCSMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AircraftMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Auto;
        private ToolStripMenuItem tsmi_Transfer;
        private ToolStripMenuItem tsmi_AV8B;
        private ToolStripMenuItem tsmi_F16Menu;
        private ToolStripMenuItem tsmi_F18;
        private ToolStripMenuItem tsmi_KA50;
        private ToolStripMenuItem tsmi_M2000;
        private ToolStripMenuItem tsmi_Load;
        private ToolStripMenuItem tsmi_SaveAs;
        private ToolStripMenuItem tsmi_FetchCoordinates;
        private ToolStripStatusLabel lbl_DCS_Status;
        private Label label18;
        private ComboBox cb_PointType;
        private Label label17;
        private ComboBox cb_PointOption;
        private CheckBox cb_AltitudeIsAGL;
        private ToolStripSeparator toolStripSeparator1;
        private Timer tmr250ms;
        private ToolStripProgressBar pb_Transfer;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem tsmi_DCSMainScreenMenu;
        private ToolStripMenuItem tsmi_ReticleSetting;
        private ToolStripMenuItem tsmi_Reticle_WhenInF10Map;
        private ToolStripMenuItem tsmi_Reticle_Always;
        private ToolStripMenuItem tsmi_Reticle_Never;
        private ToolStripMenuItem tsmi_CameraPositionModeMenu;
        private ToolStripMenuItem tsmi_TerrainElevationUnderCamera;
        private ToolStripMenuItem tsmi_CameraAltitude;
        private ToolStripMenuItem tsmi_AlwaysOnTop;
        private TextBox textBox1;
        private TextBox textBox2;
        private ToolStripMenuItem tsmi_TransparencyMenu;
        private ToolStripMenuItem tsmi_opaque;
        private ToolStripMenuItem tsmi_Opacity50;
        private ToolStripMenuItem tsmi_Opacity25;
        private ToolStripMenuItem tsmi_Opacity75;
        private ToolStripMenuItem tsmi_ImportUnits;
        private ToolStripMenuItem tsmi_AH64Menu;
        private ToolStripMenuItem tsmi_AH64;
        private ToolStripMenuItem tsmi_AH64_ClearPoints;
        private ToolStripMenuItem tsmi_F15EMenu;
        private ToolStripMenuItem tsmi_F15E_Pilot;
        private ToolStripMenuItem tsmi_F15E_WSO;
        private ToolStripMenuItem tsmi_StopTransfer;
        private ToolStripMenuItem tsmi_F16;
        private ToolStripMenuItem tsmi_F16_SetFirstPoint;
        private ToolStripMenuItem tsmi_JF17Menu;
        private ToolStripMenuItem tsmi_JF17;
        private ToolStripMenuItem tsmi_JF17_SetFirstPoint;
        private ToolStrip toolStrip1;
        private Label label19;
        private ToolStripButton btn_Transfer;
        private ToolStripButton btn_Stop;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btn_FetchCoordinates;
        private ToolStripButton btn_ImportUnits;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btn_AlwaysOnTop;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton btn_MoveDown;
        private ToolStripButton btn_MoveUp;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton btn_Add;
        private ToolStripButton btn_Edit;
        private ToolStripButton btn_SetBE;
        private ToolStripMenuItem tsmi_CheckForUpdates;
        private NumericUpDown nud_LL_DecimalMinutes_Precision;
        private NumericUpDown nud_LL_DecimalSeconds_Precision;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colLabel;
        private DataGridViewTextBoxColumn colCoordinates;
        private DataGridViewTextBoxColumn colAltitude;
        private DataGridViewCheckBoxColumn colXFer;
        private DataGridViewButtonColumn colDelete;
        private ToolStripMenuItem tsmi_AH64_DTC;
        private ToolStripMenuItem miscToolStripMenuItem;
        private ToolStripMenuItem tsmi_execute;
        private ToolStripMenuItem tsmi_AutoCheckForUpdates;
        private ToolStripMenuItem tsmi_recentFiles;
        private ToolStripMenuItem tsmi_exit;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripMenuItem tsmi_changeBaseDirectory;
        private ToolStripMenuItem tsmi_A10C;
    }
}

