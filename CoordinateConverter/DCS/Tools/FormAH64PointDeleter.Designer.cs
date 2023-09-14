namespace CoordinateConverter.DCS.Tools
{
    partial class FormAH64PointDeleter
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
            this.nud_FirstPointIdx = new System.Windows.Forms.NumericUpDown();
            this.nud_LastPointIdx = new System.Windows.Forms.NumericUpDown();
            this.cb_PointType = new System.Windows.Forms.ComboBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud_FirstPointIdx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_LastPointIdx)).BeginInit();
            this.SuspendLayout();
            // 
            // nud_FirstPointIdx
            // 
            this.nud_FirstPointIdx.Location = new System.Drawing.Point(92, 39);
            this.nud_FirstPointIdx.Name = "nud_FirstPointIdx";
            this.nud_FirstPointIdx.Size = new System.Drawing.Size(120, 20);
            this.nud_FirstPointIdx.TabIndex = 0;
            // 
            // nud_LastPointIdx
            // 
            this.nud_LastPointIdx.Location = new System.Drawing.Point(92, 65);
            this.nud_LastPointIdx.Name = "nud_LastPointIdx";
            this.nud_LastPointIdx.Size = new System.Drawing.Size(120, 20);
            this.nud_LastPointIdx.TabIndex = 1;
            // 
            // cb_PointType
            // 
            this.cb_PointType.FormattingEnabled = true;
            this.cb_PointType.Location = new System.Drawing.Point(91, 12);
            this.cb_PointType.Name = "cb_PointType";
            this.cb_PointType.Size = new System.Drawing.Size(121, 21);
            this.cb_PointType.TabIndex = 2;
            this.cb_PointType.SelectedIndexChanged += new System.EventHandler(this.Cb_PointType_SelectedIndexChanged);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(120, 93);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(92, 23);
            this.btn_Ok.TabIndex = 3;
            this.btn_Ok.Text = "Delete";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(12, 93);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(92, 23);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Point Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "First point";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Last point";
            // 
            // AH64PointDeleter
            // 
            this.AcceptButton = this.btn_Cancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(224, 121);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.cb_PointType);
            this.Controls.Add(this.nud_LastPointIdx);
            this.Controls.Add(this.nud_FirstPointIdx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(240, 160);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(240, 160);
            this.Name = "AH64PointDeleter";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Remove points from AH64";
            ((System.ComponentModel.ISupportInitialize)(this.nud_FirstPointIdx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_LastPointIdx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nud_FirstPointIdx;
        private System.Windows.Forms.NumericUpDown nud_LastPointIdx;
        private System.Windows.Forms.ComboBox cb_PointType;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}