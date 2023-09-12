namespace CoordinateConverter
{
    partial class FormStartingWaypoint
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
            this.lbl_FirstSPTPToUse = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.nud_PointNumber = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nud_PointNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_FirstSPTPToUse
            // 
            this.lbl_FirstSPTPToUse.AutoSize = true;
            this.lbl_FirstSPTPToUse.Location = new System.Drawing.Point(12, 14);
            this.lbl_FirstSPTPToUse.Name = "lbl_FirstSPTPToUse";
            this.lbl_FirstSPTPToUse.Size = new System.Drawing.Size(137, 13);
            this.lbl_FirstSPTPToUse.TabIndex = 0;
            this.lbl_FirstSPTPToUse.Text = "First STPT to use [1 .. 699]:";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(12, 38);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(187, 23);
            this.btn_OK.TabIndex = 1;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // nud_PointNumber
            // 
            this.nud_PointNumber.Location = new System.Drawing.Point(155, 12);
            this.nud_PointNumber.Maximum = new decimal(new int[] {
            699,
            0,
            0,
            0});
            this.nud_PointNumber.Name = "nud_PointNumber";
            this.nud_PointNumber.Size = new System.Drawing.Size(44, 20);
            this.nud_PointNumber.TabIndex = 1;
            this.nud_PointNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_PointNumber.ValueChanged += new System.EventHandler(this.nud_PointNumber_ValueChanged);
            // 
            // FormStartingWaypoint
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 69);
            this.ControlBox = false;
            this.Controls.Add(this.nud_PointNumber);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.lbl_FirstSPTPToUse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormStartingWaypoint";
            this.ShowIcon = false;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.nud_PointNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_FirstSPTPToUse;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.NumericUpDown nud_PointNumber;
    }
}