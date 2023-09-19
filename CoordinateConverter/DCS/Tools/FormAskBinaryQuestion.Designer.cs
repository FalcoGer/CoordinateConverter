namespace CoordinateConverter.DCS.Tools
{
    partial class FormAskBinaryQuestion
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
            this.lbl_QuestionText = new System.Windows.Forms.Label();
            this.btn_Affirm = new System.Windows.Forms.Button();
            this.btn_Deny = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_QuestionText
            // 
            this.lbl_QuestionText.AutoSize = true;
            this.lbl_QuestionText.Location = new System.Drawing.Point(13, 13);
            this.lbl_QuestionText.Name = "lbl_QuestionText";
            this.lbl_QuestionText.Size = new System.Drawing.Size(55, 13);
            this.lbl_QuestionText.TabIndex = 0;
            this.lbl_QuestionText.Text = "Question?";
            // 
            // btn_Affirm
            // 
            this.btn_Affirm.AutoSize = true;
            this.btn_Affirm.Location = new System.Drawing.Point(93, 33);
            this.btn_Affirm.Name = "btn_Affirm";
            this.btn_Affirm.Size = new System.Drawing.Size(75, 23);
            this.btn_Affirm.TabIndex = 1;
            this.btn_Affirm.Text = "Yes";
            this.btn_Affirm.UseVisualStyleBackColor = true;
            this.btn_Affirm.Click += new System.EventHandler(this.Btn_Affirm_Click);
            // 
            // btn_Deny
            // 
            this.btn_Deny.AutoSize = true;
            this.btn_Deny.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Deny.Location = new System.Drawing.Point(12, 33);
            this.btn_Deny.Name = "btn_Deny";
            this.btn_Deny.Size = new System.Drawing.Size(75, 23);
            this.btn_Deny.TabIndex = 2;
            this.btn_Deny.Text = "No";
            this.btn_Deny.UseVisualStyleBackColor = true;
            this.btn_Deny.Click += new System.EventHandler(this.Btn_Deny_Click);
            // 
            // FormAskBinaryQuestion
            // 
            this.AcceptButton = this.btn_Affirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Deny;
            this.ClientSize = new System.Drawing.Size(181, 66);
            this.Controls.Add(this.btn_Deny);
            this.Controls.Add(this.btn_Affirm);
            this.Controls.Add(this.lbl_QuestionText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormAskBinaryQuestion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AskBinaryQuestion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_QuestionText;
        private System.Windows.Forms.Button btn_Affirm;
        private System.Windows.Forms.Button btn_Deny;
    }
}