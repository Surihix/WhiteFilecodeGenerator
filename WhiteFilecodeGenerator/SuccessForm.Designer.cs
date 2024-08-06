namespace WhiteFilecodeGenerator
{
    partial class SuccessForm
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
            this.CodeLabel = new System.Windows.Forms.Label();
            this.CodeTxtBox = new System.Windows.Forms.TextBox();
            this.OkBtn = new System.Windows.Forms.Button();
            this.ExtraInfoLabel = new System.Windows.Forms.Label();
            this.ExtraInfoTxtBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CodeLabel
            // 
            this.CodeLabel.AutoSize = true;
            this.CodeLabel.Location = new System.Drawing.Point(88, 20);
            this.CodeLabel.Name = "CodeLabel";
            this.CodeLabel.Size = new System.Drawing.Size(50, 13);
            this.CodeLabel.TabIndex = 0;
            this.CodeLabel.Text = "Filecode:";
            // 
            // CodeTxtBox
            // 
            this.CodeTxtBox.Location = new System.Drawing.Point(73, 38);
            this.CodeTxtBox.Name = "CodeTxtBox";
            this.CodeTxtBox.ReadOnly = true;
            this.CodeTxtBox.Size = new System.Drawing.Size(80, 20);
            this.CodeTxtBox.TabIndex = 1;
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(83, 269);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(60, 23);
            this.OkBtn.TabIndex = 2;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // ExtraInfoLabel
            // 
            this.ExtraInfoLabel.AutoSize = true;
            this.ExtraInfoLabel.Location = new System.Drawing.Point(86, 93);
            this.ExtraInfoLabel.Name = "ExtraInfoLabel";
            this.ExtraInfoLabel.Size = new System.Drawing.Size(55, 13);
            this.ExtraInfoLabel.TabIndex = 4;
            this.ExtraInfoLabel.Text = "Extra Info:";
            // 
            // ExtraInfoTxtBox
            // 
            this.ExtraInfoTxtBox.Location = new System.Drawing.Point(18, 115);
            this.ExtraInfoTxtBox.Multiline = true;
            this.ExtraInfoTxtBox.Name = "ExtraInfoTxtBox";
            this.ExtraInfoTxtBox.ReadOnly = true;
            this.ExtraInfoTxtBox.Size = new System.Drawing.Size(190, 134);
            this.ExtraInfoTxtBox.TabIndex = 5;
            // 
            // SuccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 306);
            this.Controls.Add(this.ExtraInfoTxtBox);
            this.Controls.Add(this.ExtraInfoLabel);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.CodeTxtBox);
            this.Controls.Add(this.CodeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SuccessForm";
            this.Text = "Success";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CodeLabel;
        private System.Windows.Forms.TextBox CodeTxtBox;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Label ExtraInfoLabel;
        private System.Windows.Forms.TextBox ExtraInfoTxtBox;
    }
}