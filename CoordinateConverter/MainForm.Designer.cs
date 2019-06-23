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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.RB_BullsE = new System.Windows.Forms.RadioButton();
            this.RB_BullsW = new System.Windows.Forms.RadioButton();
            this.TB_BullsLon = new System.Windows.Forms.TextBox();
            this.GrpBox_Bulls = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Pnl_BullsEasting = new System.Windows.Forms.Panel();
            this.Pnl_BullsNorthing = new System.Windows.Forms.Panel();
            this.TB_BullsLat = new System.Windows.Forms.TextBox();
            this.RB_BullsN = new System.Windows.Forms.RadioButton();
            this.RB_BullsS = new System.Windows.Forms.RadioButton();
            this.GrpBox_Output = new System.Windows.Forms.GroupBox();
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
            this.TB_MGRS_SubgridIdent = new System.Windows.Forms.TextBox();
            this.TB_MGRS_Fraction = new System.Windows.Forms.TextBox();
            this.TB_MGRS_NorthGrid = new System.Windows.Forms.TextBox();
            this.TB_MGRS_EastGrid = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.TB_UTM_NorthGrid = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TB_UTM_Easting = new System.Windows.Forms.TextBox();
            this.TB_UTM_Northing = new System.Windows.Forms.TextBox();
            this.TB_UTM_EastGrid = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Bulls_Range = new System.Windows.Forms.TextBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Bulls_Bearing = new System.Windows.Forms.TextBox();
            this.LbL_Error = new System.Windows.Forms.Label();
            this.GrpBox_Bulls.SuspendLayout();
            this.Pnl_BullsEasting.SuspendLayout();
            this.Pnl_BullsNorthing.SuspendLayout();
            this.GrpBox_Output.SuspendLayout();
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
            this.SuspendLayout();
            // 
            // RB_BullsE
            // 
            this.RB_BullsE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_BullsE.AutoSize = true;
            this.RB_BullsE.Checked = true;
            this.RB_BullsE.Location = new System.Drawing.Point(242, 4);
            this.RB_BullsE.Name = "RB_BullsE";
            this.RB_BullsE.Size = new System.Drawing.Size(32, 17);
            this.RB_BullsE.TabIndex = 2;
            this.RB_BullsE.TabStop = true;
            this.RB_BullsE.Text = "E";
            this.RB_BullsE.UseVisualStyleBackColor = true;
            this.RB_BullsE.CheckedChanged += new System.EventHandler(this.RB_Bulls_CheckedChanged);
            // 
            // RB_BullsW
            // 
            this.RB_BullsW.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_BullsW.AutoSize = true;
            this.RB_BullsW.Location = new System.Drawing.Point(203, 4);
            this.RB_BullsW.Name = "RB_BullsW";
            this.RB_BullsW.Size = new System.Drawing.Size(36, 17);
            this.RB_BullsW.TabIndex = 1;
            this.RB_BullsW.Text = "W";
            this.RB_BullsW.UseVisualStyleBackColor = true;
            this.RB_BullsW.CheckedChanged += new System.EventHandler(this.RB_Bulls_CheckedChanged);
            // 
            // TB_BullsLon
            // 
            this.TB_BullsLon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_BullsLon.Location = new System.Drawing.Point(3, 3);
            this.TB_BullsLon.MaxLength = 16;
            this.TB_BullsLon.Name = "TB_BullsLon";
            this.TB_BullsLon.Size = new System.Drawing.Size(191, 20);
            this.TB_BullsLon.TabIndex = 0;
            this.TB_BullsLon.TextChanged += new System.EventHandler(this.TB_BullsLon_TextChanged);
            this.TB_BullsLon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_KeyPress);
            // 
            // GrpBox_Bulls
            // 
            this.GrpBox_Bulls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBox_Bulls.Controls.Add(this.label4);
            this.GrpBox_Bulls.Controls.Add(this.Pnl_BullsEasting);
            this.GrpBox_Bulls.Controls.Add(this.Pnl_BullsNorthing);
            this.GrpBox_Bulls.Location = new System.Drawing.Point(613, 12);
            this.GrpBox_Bulls.Name = "GrpBox_Bulls";
            this.GrpBox_Bulls.Size = new System.Drawing.Size(283, 100);
            this.GrpBox_Bulls.TabIndex = 1;
            this.GrpBox_Bulls.TabStop = false;
            this.GrpBox_Bulls.Text = "Bullseye";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Bullseye coordinates in L/L";
            // 
            // Pnl_BullsEasting
            // 
            this.Pnl_BullsEasting.Controls.Add(this.TB_BullsLon);
            this.Pnl_BullsEasting.Controls.Add(this.RB_BullsW);
            this.Pnl_BullsEasting.Controls.Add(this.RB_BullsE);
            this.Pnl_BullsEasting.Location = new System.Drawing.Point(3, 49);
            this.Pnl_BullsEasting.Name = "Pnl_BullsEasting";
            this.Pnl_BullsEasting.Size = new System.Drawing.Size(277, 27);
            this.Pnl_BullsEasting.TabIndex = 1;
            // 
            // Pnl_BullsNorthing
            // 
            this.Pnl_BullsNorthing.Controls.Add(this.TB_BullsLat);
            this.Pnl_BullsNorthing.Controls.Add(this.RB_BullsN);
            this.Pnl_BullsNorthing.Controls.Add(this.RB_BullsS);
            this.Pnl_BullsNorthing.Location = new System.Drawing.Point(3, 16);
            this.Pnl_BullsNorthing.Name = "Pnl_BullsNorthing";
            this.Pnl_BullsNorthing.Size = new System.Drawing.Size(277, 27);
            this.Pnl_BullsNorthing.TabIndex = 0;
            // 
            // TB_BullsLat
            // 
            this.TB_BullsLat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_BullsLat.Location = new System.Drawing.Point(3, 3);
            this.TB_BullsLat.MaxLength = 16;
            this.TB_BullsLat.Name = "TB_BullsLat";
            this.TB_BullsLat.Size = new System.Drawing.Size(191, 20);
            this.TB_BullsLat.TabIndex = 0;
            this.TB_BullsLat.TextChanged += new System.EventHandler(this.TB_BullsLat_TextChanged);
            this.TB_BullsLat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_KeyPress);
            // 
            // RB_BullsN
            // 
            this.RB_BullsN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_BullsN.AutoSize = true;
            this.RB_BullsN.Checked = true;
            this.RB_BullsN.Location = new System.Drawing.Point(203, 4);
            this.RB_BullsN.Name = "RB_BullsN";
            this.RB_BullsN.Size = new System.Drawing.Size(33, 17);
            this.RB_BullsN.TabIndex = 1;
            this.RB_BullsN.TabStop = true;
            this.RB_BullsN.Text = "N";
            this.RB_BullsN.UseVisualStyleBackColor = true;
            this.RB_BullsN.CheckedChanged += new System.EventHandler(this.RB_Bulls_CheckedChanged);
            // 
            // RB_BullsS
            // 
            this.RB_BullsS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_BullsS.AutoSize = true;
            this.RB_BullsS.Location = new System.Drawing.Point(242, 4);
            this.RB_BullsS.Name = "RB_BullsS";
            this.RB_BullsS.Size = new System.Drawing.Size(32, 17);
            this.RB_BullsS.TabIndex = 2;
            this.RB_BullsS.Text = "S";
            this.RB_BullsS.UseVisualStyleBackColor = true;
            this.RB_BullsS.CheckedChanged += new System.EventHandler(this.RB_Bulls_CheckedChanged);
            // 
            // GrpBox_Output
            // 
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
            this.GrpBox_Output.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GrpBox_Output.Location = new System.Drawing.Point(0, 269);
            this.GrpBox_Output.Name = "GrpBox_Output";
            this.GrpBox_Output.Size = new System.Drawing.Size(908, 151);
            this.GrpBox_Output.TabIndex = 2;
            this.GrpBox_Output.TabStop = false;
            this.GrpBox_Output.Text = "Output";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "BULLS";
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
            this.TB_Out_Bulls.Size = new System.Drawing.Size(855, 20);
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
            this.TB_Out_UTM.Size = new System.Drawing.Size(855, 20);
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
            this.TB_Out_MGRS.Size = new System.Drawing.Size(855, 20);
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
            this.TB_Out_LL.Size = new System.Drawing.Size(855, 20);
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
            this.TB_Out_LLDec.Size = new System.Drawing.Size(855, 20);
            this.TB_Out_LLDec.TabIndex = 1;
            this.TB_Out_LLDec.TabStop = false;
            this.TB_Out_LLDec.Text = "Enter Coordinates";
            // 
            // GrpBox_Input
            // 
            this.GrpBox_Input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBox_Input.Controls.Add(this.TC_Input);
            this.GrpBox_Input.Location = new System.Drawing.Point(12, 12);
            this.GrpBox_Input.Name = "GrpBox_Input";
            this.GrpBox_Input.Size = new System.Drawing.Size(590, 251);
            this.GrpBox_Input.TabIndex = 0;
            this.GrpBox_Input.TabStop = false;
            this.GrpBox_Input.Text = "Input";
            // 
            // TC_Input
            // 
            this.TC_Input.Controls.Add(this.TabPage_LatLon);
            this.TC_Input.Controls.Add(this.tabPage1);
            this.TC_Input.Controls.Add(this.tabPage2);
            this.TC_Input.Controls.Add(this.tabPage3);
            this.TC_Input.Controls.Add(this.tabPage4);
            this.TC_Input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TC_Input.Location = new System.Drawing.Point(3, 16);
            this.TC_Input.Name = "TC_Input";
            this.TC_Input.SelectedIndex = 0;
            this.TC_Input.Size = new System.Drawing.Size(584, 232);
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
            this.TabPage_LatLon.Size = new System.Drawing.Size(576, 206);
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
            this.label7.Text = "Example: 57° 25\' 66.23\" N / 047° 55\' 42.52\" E";
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
            this.panel1.Size = new System.Drawing.Size(537, 27);
            this.panel1.TabIndex = 1;
            // 
            // TB_LL_Lon
            // 
            this.TB_LL_Lon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_LL_Lon.Location = new System.Drawing.Point(3, 3);
            this.TB_LL_Lon.MaxLength = 16;
            this.TB_LL_Lon.Name = "TB_LL_Lon";
            this.TB_LL_Lon.Size = new System.Drawing.Size(451, 20);
            this.TB_LL_Lon.TabIndex = 2;
            this.TB_LL_Lon.TextChanged += new System.EventHandler(this.TB_LL_Lon_TextChanged);
            this.TB_LL_Lon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_KeyPress);
            // 
            // RB_LL_W
            // 
            this.RB_LL_W.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LL_W.AutoSize = true;
            this.RB_LL_W.Location = new System.Drawing.Point(463, 4);
            this.RB_LL_W.Name = "RB_LL_W";
            this.RB_LL_W.Size = new System.Drawing.Size(36, 17);
            this.RB_LL_W.TabIndex = 0;
            this.RB_LL_W.Text = "W";
            this.RB_LL_W.UseVisualStyleBackColor = true;
            this.RB_LL_W.CheckedChanged += new System.EventHandler(this.RB_LL_CheckedChanged);
            // 
            // RB_LL_E
            // 
            this.RB_LL_E.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LL_E.AutoSize = true;
            this.RB_LL_E.Checked = true;
            this.RB_LL_E.Location = new System.Drawing.Point(502, 4);
            this.RB_LL_E.Name = "RB_LL_E";
            this.RB_LL_E.Size = new System.Drawing.Size(32, 17);
            this.RB_LL_E.TabIndex = 1;
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
            this.panel2.Size = new System.Drawing.Size(537, 27);
            this.panel2.TabIndex = 0;
            // 
            // TB_LL_Lat
            // 
            this.TB_LL_Lat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_LL_Lat.Location = new System.Drawing.Point(3, 3);
            this.TB_LL_Lat.MaxLength = 16;
            this.TB_LL_Lat.Name = "TB_LL_Lat";
            this.TB_LL_Lat.Size = new System.Drawing.Size(451, 20);
            this.TB_LL_Lat.TabIndex = 0;
            this.TB_LL_Lat.TextChanged += new System.EventHandler(this.TB_LL_Lat_TextChanged);
            this.TB_LL_Lat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_KeyPress);
            // 
            // RB_LL_N
            // 
            this.RB_LL_N.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LL_N.AutoSize = true;
            this.RB_LL_N.Checked = true;
            this.RB_LL_N.Location = new System.Drawing.Point(463, 4);
            this.RB_LL_N.Name = "RB_LL_N";
            this.RB_LL_N.Size = new System.Drawing.Size(33, 17);
            this.RB_LL_N.TabIndex = 1;
            this.RB_LL_N.TabStop = true;
            this.RB_LL_N.Text = "N";
            this.RB_LL_N.UseVisualStyleBackColor = true;
            this.RB_LL_N.CheckedChanged += new System.EventHandler(this.RB_LL_CheckedChanged);
            // 
            // RB_LL_S
            // 
            this.RB_LL_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LL_S.AutoSize = true;
            this.RB_LL_S.Location = new System.Drawing.Point(502, 4);
            this.RB_LL_S.Name = "RB_LL_S";
            this.RB_LL_S.Size = new System.Drawing.Size(32, 17);
            this.RB_LL_S.TabIndex = 2;
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
            this.tabPage1.Size = new System.Drawing.Size(576, 206);
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
            this.panel3.Size = new System.Drawing.Size(537, 27);
            this.panel3.TabIndex = 1;
            // 
            // TB_LLDec_Lon
            // 
            this.TB_LLDec_Lon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_LLDec_Lon.Location = new System.Drawing.Point(3, 3);
            this.TB_LLDec_Lon.MaxLength = 16;
            this.TB_LLDec_Lon.Name = "TB_LLDec_Lon";
            this.TB_LLDec_Lon.Size = new System.Drawing.Size(451, 20);
            this.TB_LLDec_Lon.TabIndex = 0;
            this.TB_LLDec_Lon.TextChanged += new System.EventHandler(this.TB_LLDecimal_Lon_TextChanged);
            this.TB_LLDec_Lon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_Decimal_KeyPress);
            // 
            // RB_LLDec_W
            // 
            this.RB_LLDec_W.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LLDec_W.AutoSize = true;
            this.RB_LLDec_W.Location = new System.Drawing.Point(463, 4);
            this.RB_LLDec_W.Name = "RB_LLDec_W";
            this.RB_LLDec_W.Size = new System.Drawing.Size(36, 17);
            this.RB_LLDec_W.TabIndex = 1;
            this.RB_LLDec_W.Text = "W";
            this.RB_LLDec_W.UseVisualStyleBackColor = true;
            this.RB_LLDec_W.CheckedChanged += new System.EventHandler(this.RB_LLDecimal_CheckedChanged);
            // 
            // RB_LLDec_E
            // 
            this.RB_LLDec_E.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LLDec_E.AutoSize = true;
            this.RB_LLDec_E.Checked = true;
            this.RB_LLDec_E.Location = new System.Drawing.Point(502, 4);
            this.RB_LLDec_E.Name = "RB_LLDec_E";
            this.RB_LLDec_E.Size = new System.Drawing.Size(32, 17);
            this.RB_LLDec_E.TabIndex = 2;
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
            this.panel4.Size = new System.Drawing.Size(537, 27);
            this.panel4.TabIndex = 0;
            // 
            // TB_LLDec_Lat
            // 
            this.TB_LLDec_Lat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_LLDec_Lat.Location = new System.Drawing.Point(3, 3);
            this.TB_LLDec_Lat.MaxLength = 16;
            this.TB_LLDec_Lat.Name = "TB_LLDec_Lat";
            this.TB_LLDec_Lat.Size = new System.Drawing.Size(451, 20);
            this.TB_LLDec_Lat.TabIndex = 0;
            this.TB_LLDec_Lat.TextChanged += new System.EventHandler(this.TB_LLDecimal_Lat_TextChanged);
            this.TB_LLDec_Lat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_LL_Decimal_KeyPress);
            // 
            // RB_LLDec_N
            // 
            this.RB_LLDec_N.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LLDec_N.AutoSize = true;
            this.RB_LLDec_N.Checked = true;
            this.RB_LLDec_N.Location = new System.Drawing.Point(463, 4);
            this.RB_LLDec_N.Name = "RB_LLDec_N";
            this.RB_LLDec_N.Size = new System.Drawing.Size(33, 17);
            this.RB_LLDec_N.TabIndex = 1;
            this.RB_LLDec_N.TabStop = true;
            this.RB_LLDec_N.Text = "N";
            this.RB_LLDec_N.UseVisualStyleBackColor = true;
            this.RB_LLDec_N.CheckedChanged += new System.EventHandler(this.RB_LLDecimal_CheckedChanged);
            // 
            // RB_LLDec_S
            // 
            this.RB_LLDec_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_LLDec_S.AutoSize = true;
            this.RB_LLDec_S.Location = new System.Drawing.Point(502, 4);
            this.RB_LLDec_S.Name = "RB_LLDec_S";
            this.RB_LLDec_S.Size = new System.Drawing.Size(32, 17);
            this.RB_LLDec_S.TabIndex = 2;
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
            this.tabPage2.Size = new System.Drawing.Size(576, 206);
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
            this.textBox7.Size = new System.Drawing.Size(567, 140);
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
            this.panel5.Controls.Add(this.TB_MGRS_SubgridIdent);
            this.panel5.Controls.Add(this.TB_MGRS_Fraction);
            this.panel5.Controls.Add(this.TB_MGRS_NorthGrid);
            this.panel5.Controls.Add(this.TB_MGRS_EastGrid);
            this.panel5.Location = new System.Drawing.Point(3, 6);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(569, 35);
            this.panel5.TabIndex = 0;
            // 
            // TB_MGRS_SubgridIdent
            // 
            this.TB_MGRS_SubgridIdent.Location = new System.Drawing.Point(80, 8);
            this.TB_MGRS_SubgridIdent.MaxLength = 2;
            this.TB_MGRS_SubgridIdent.Name = "TB_MGRS_SubgridIdent";
            this.TB_MGRS_SubgridIdent.Size = new System.Drawing.Size(34, 20);
            this.TB_MGRS_SubgridIdent.TabIndex = 2;
            this.TB_MGRS_SubgridIdent.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.TB_MGRS_SubgridIdent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_MGRS_SubgridIdent_KeyPress);
            // 
            // TB_MGRS_Fraction
            // 
            this.TB_MGRS_Fraction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_MGRS_Fraction.Location = new System.Drawing.Point(120, 8);
            this.TB_MGRS_Fraction.MaxLength = 11;
            this.TB_MGRS_Fraction.Name = "TB_MGRS_Fraction";
            this.TB_MGRS_Fraction.Size = new System.Drawing.Size(446, 20);
            this.TB_MGRS_Fraction.TabIndex = 3;
            this.TB_MGRS_Fraction.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.TB_MGRS_Fraction.Enter += new System.EventHandler(this.TB_MGRS_Fraction_Enter);
            this.TB_MGRS_Fraction.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_MGRS_Fraction_KeyPress);
            this.TB_MGRS_Fraction.Leave += new System.EventHandler(this.TB_MGRS_Fraction_Leave);
            // 
            // TB_MGRS_NorthGrid
            // 
            this.TB_MGRS_NorthGrid.Location = new System.Drawing.Point(40, 8);
            this.TB_MGRS_NorthGrid.MaxLength = 1;
            this.TB_MGRS_NorthGrid.Name = "TB_MGRS_NorthGrid";
            this.TB_MGRS_NorthGrid.Size = new System.Drawing.Size(34, 20);
            this.TB_MGRS_NorthGrid.TabIndex = 1;
            this.TB_MGRS_NorthGrid.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.TB_MGRS_NorthGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_NorthGrid_KeyPress);
            // 
            // TB_MGRS_EastGrid
            // 
            this.TB_MGRS_EastGrid.Location = new System.Drawing.Point(3, 8);
            this.TB_MGRS_EastGrid.MaxLength = 2;
            this.TB_MGRS_EastGrid.Name = "TB_MGRS_EastGrid";
            this.TB_MGRS_EastGrid.Size = new System.Drawing.Size(34, 20);
            this.TB_MGRS_EastGrid.TabIndex = 0;
            this.TB_MGRS_EastGrid.TextChanged += new System.EventHandler(this.InputMGRSChanged);
            this.TB_MGRS_EastGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_EastGrid_KeyPress);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox9);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.panel8);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(576, 206);
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
            this.textBox9.Size = new System.Drawing.Size(567, 140);
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
            this.panel8.Controls.Add(this.TB_UTM_NorthGrid);
            this.panel8.Controls.Add(this.tableLayoutPanel1);
            this.panel8.Controls.Add(this.TB_UTM_EastGrid);
            this.panel8.Location = new System.Drawing.Point(3, 6);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(569, 35);
            this.panel8.TabIndex = 0;
            // 
            // TB_UTM_NorthGrid
            // 
            this.TB_UTM_NorthGrid.Location = new System.Drawing.Point(40, 8);
            this.TB_UTM_NorthGrid.MaxLength = 1;
            this.TB_UTM_NorthGrid.Name = "TB_UTM_NorthGrid";
            this.TB_UTM_NorthGrid.Size = new System.Drawing.Size(34, 20);
            this.TB_UTM_NorthGrid.TabIndex = 1;
            this.TB_UTM_NorthGrid.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.TB_UTM_NorthGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_NorthGrid_KeyPress);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(483, 26);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // TB_UTM_Easting
            // 
            this.TB_UTM_Easting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_UTM_Easting.Location = new System.Drawing.Point(3, 3);
            this.TB_UTM_Easting.MaxLength = 16;
            this.TB_UTM_Easting.Name = "TB_UTM_Easting";
            this.TB_UTM_Easting.Size = new System.Drawing.Size(235, 20);
            this.TB_UTM_Easting.TabIndex = 0;
            this.TB_UTM_Easting.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.TB_UTM_Easting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RB_UTM_Northing_Easting_KeyPress);
            // 
            // TB_UTM_Northing
            // 
            this.TB_UTM_Northing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_UTM_Northing.Location = new System.Drawing.Point(244, 3);
            this.TB_UTM_Northing.MaxLength = 16;
            this.TB_UTM_Northing.Name = "TB_UTM_Northing";
            this.TB_UTM_Northing.Size = new System.Drawing.Size(236, 20);
            this.TB_UTM_Northing.TabIndex = 1;
            this.TB_UTM_Northing.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.TB_UTM_Northing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RB_UTM_Northing_Easting_KeyPress);
            // 
            // TB_UTM_EastGrid
            // 
            this.TB_UTM_EastGrid.Location = new System.Drawing.Point(3, 8);
            this.TB_UTM_EastGrid.MaxLength = 2;
            this.TB_UTM_EastGrid.Name = "TB_UTM_EastGrid";
            this.TB_UTM_EastGrid.Size = new System.Drawing.Size(34, 20);
            this.TB_UTM_EastGrid.TabIndex = 0;
            this.TB_UTM_EastGrid.TextChanged += new System.EventHandler(this.InputUTMChanged);
            this.TB_UTM_EastGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_UTM_MGRS_EastGrid_KeyPress);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.panel9);
            this.tabPage4.Controls.Add(this.panel10);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(576, 206);
            this.tabPage4.TabIndex = 5;
            this.tabPage4.Text = "BULLS";
            this.tabPage4.UseVisualStyleBackColor = true;
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
            this.panel9.Size = new System.Drawing.Size(537, 27);
            this.panel9.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(511, 6);
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
            this.TB_Bulls_Range.MaxLength = 4;
            this.TB_Bulls_Range.Name = "TB_Bulls_Range";
            this.TB_Bulls_Range.Size = new System.Drawing.Size(502, 20);
            this.TB_Bulls_Range.TabIndex = 0;
            this.TB_Bulls_Range.TextChanged += new System.EventHandler(this.TB_Bulls_Range_TextChanged);
            this.TB_Bulls_Range.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_Bulls_Range_KeyPress);
            // 
            // panel10
            // 
            this.panel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel10.Controls.Add(this.label1);
            this.panel10.Controls.Add(this.TB_Bulls_Bearing);
            this.panel10.Location = new System.Drawing.Point(6, 6);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(537, 27);
            this.panel10.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(507, 6);
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
            this.TB_Bulls_Bearing.MaxLength = 3;
            this.TB_Bulls_Bearing.Name = "TB_Bulls_Bearing";
            this.TB_Bulls_Bearing.Size = new System.Drawing.Size(502, 20);
            this.TB_Bulls_Bearing.TabIndex = 0;
            this.TB_Bulls_Bearing.TextChanged += new System.EventHandler(this.TB_Bulls_Bearing_TextChanged);
            this.TB_Bulls_Bearing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_Bulls_Bearing_KeyPress);
            // 
            // LbL_Error
            // 
            this.LbL_Error.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LbL_Error.AutoSize = true;
            this.LbL_Error.BackColor = System.Drawing.Color.Pink;
            this.LbL_Error.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LbL_Error.Location = new System.Drawing.Point(610, 115);
            this.LbL_Error.Name = "LbL_Error";
            this.LbL_Error.Size = new System.Drawing.Size(58, 13);
            this.LbL_Error.TabIndex = 3;
            this.LbL_Error.Text = "<ERROR>";
            this.LbL_Error.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 420);
            this.Controls.Add(this.LbL_Error);
            this.Controls.Add(this.GrpBox_Input);
            this.Controls.Add(this.GrpBox_Output);
            this.Controls.Add(this.GrpBox_Bulls);
            this.Name = "MainForm";
            this.Text = "Coordinate Converter";
            this.GrpBox_Bulls.ResumeLayout(false);
            this.GrpBox_Bulls.PerformLayout();
            this.Pnl_BullsEasting.ResumeLayout(false);
            this.Pnl_BullsEasting.PerformLayout();
            this.Pnl_BullsNorthing.ResumeLayout(false);
            this.Pnl_BullsNorthing.PerformLayout();
            this.GrpBox_Output.ResumeLayout(false);
            this.GrpBox_Output.PerformLayout();
            this.GrpBox_Input.ResumeLayout(false);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton RB_BullsE;
        private System.Windows.Forms.RadioButton RB_BullsW;
        private System.Windows.Forms.TextBox TB_BullsLon;
        private System.Windows.Forms.GroupBox GrpBox_Bulls;
        private System.Windows.Forms.Panel Pnl_BullsEasting;
        private System.Windows.Forms.Panel Pnl_BullsNorthing;
        private System.Windows.Forms.TextBox TB_BullsLat;
        private System.Windows.Forms.RadioButton RB_BullsN;
        private System.Windows.Forms.RadioButton RB_BullsS;
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
        private System.Windows.Forms.Label label4;
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
        private System.Windows.Forms.TextBox TB_UTM_EastGrid;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Bulls_Range;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_Bulls_Bearing;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox TB_UTM_NorthGrid;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox TB_MGRS_Fraction;
        private System.Windows.Forms.TextBox TB_MGRS_NorthGrid;
        private System.Windows.Forms.TextBox TB_MGRS_EastGrid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox TB_MGRS_SubgridIdent;
        private Label LbL_Error;
    }
}

