namespace WhiteFilecodeGenerator
{
    partial class CoreForm
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
            this.SelectGameGrpBox = new System.Windows.Forms.GroupBox();
            this.FFXiiiLrRadioBtn = new System.Windows.Forms.RadioButton();
            this.FFXiii2RadioBtn = new System.Windows.Forms.RadioButton();
            this.FFXiiiRadioBtn = new System.Windows.Forms.RadioButton();
            this.VirtualPathTxtBox = new System.Windows.Forms.TextBox();
            this.VirtualPathLabel = new System.Windows.Forms.Label();
            this.GenerateBtn = new System.Windows.Forms.Button();
            this.PathCharaNoticeLabel = new System.Windows.Forms.Label();
            this.SelectGameGrpBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectGameGrpBox
            // 
            this.SelectGameGrpBox.Controls.Add(this.FFXiiiLrRadioBtn);
            this.SelectGameGrpBox.Controls.Add(this.FFXiii2RadioBtn);
            this.SelectGameGrpBox.Controls.Add(this.FFXiiiRadioBtn);
            this.SelectGameGrpBox.Location = new System.Drawing.Point(13, 13);
            this.SelectGameGrpBox.Name = "SelectGameGrpBox";
            this.SelectGameGrpBox.Size = new System.Drawing.Size(208, 52);
            this.SelectGameGrpBox.TabIndex = 0;
            this.SelectGameGrpBox.TabStop = false;
            this.SelectGameGrpBox.Text = "Select Game:";
            // 
            // FFXiiiLrRadioBtn
            // 
            this.FFXiiiLrRadioBtn.AutoSize = true;
            this.FFXiiiLrRadioBtn.Location = new System.Drawing.Point(133, 19);
            this.FFXiiiLrRadioBtn.Name = "FFXiiiLrRadioBtn";
            this.FFXiiiLrRadioBtn.Size = new System.Drawing.Size(70, 17);
            this.FFXiiiLrRadioBtn.TabIndex = 2;
            this.FFXiiiLrRadioBtn.TabStop = true;
            this.FFXiiiLrRadioBtn.Text = "FFXIII-LR";
            this.FFXiiiLrRadioBtn.UseVisualStyleBackColor = true;
            // 
            // FFXiii2RadioBtn
            // 
            this.FFXiii2RadioBtn.AutoSize = true;
            this.FFXiii2RadioBtn.Location = new System.Drawing.Point(65, 19);
            this.FFXiii2RadioBtn.Name = "FFXiii2RadioBtn";
            this.FFXiii2RadioBtn.Size = new System.Drawing.Size(62, 17);
            this.FFXiii2RadioBtn.TabIndex = 1;
            this.FFXiii2RadioBtn.TabStop = true;
            this.FFXiii2RadioBtn.Text = "FFXIII-2";
            this.FFXiii2RadioBtn.UseVisualStyleBackColor = true;
            // 
            // FFXiiiRadioBtn
            // 
            this.FFXiiiRadioBtn.AutoSize = true;
            this.FFXiiiRadioBtn.Location = new System.Drawing.Point(6, 19);
            this.FFXiiiRadioBtn.Name = "FFXiiiRadioBtn";
            this.FFXiiiRadioBtn.Size = new System.Drawing.Size(53, 17);
            this.FFXiiiRadioBtn.TabIndex = 0;
            this.FFXiiiRadioBtn.TabStop = true;
            this.FFXiiiRadioBtn.Text = "FFXIII";
            this.FFXiiiRadioBtn.UseVisualStyleBackColor = true;
            // 
            // VirtualPathTxtBox
            // 
            this.VirtualPathTxtBox.Location = new System.Drawing.Point(13, 134);
            this.VirtualPathTxtBox.Name = "VirtualPathTxtBox";
            this.VirtualPathTxtBox.Size = new System.Drawing.Size(382, 20);
            this.VirtualPathTxtBox.TabIndex = 1;
            // 
            // VirtualPathLabel
            // 
            this.VirtualPathLabel.AutoSize = true;
            this.VirtualPathLabel.Location = new System.Drawing.Point(16, 118);
            this.VirtualPathLabel.Name = "VirtualPathLabel";
            this.VirtualPathLabel.Size = new System.Drawing.Size(64, 13);
            this.VirtualPathLabel.TabIndex = 2;
            this.VirtualPathLabel.Text = "Virtual Path:";
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(166, 170);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(75, 23);
            this.GenerateBtn.TabIndex = 3;
            this.GenerateBtn.Text = "Generate";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // PathCharaNoticeLabel
            // 
            this.PathCharaNoticeLabel.AutoSize = true;
            this.PathCharaNoticeLabel.Location = new System.Drawing.Point(99, 83);
            this.PathCharaNoticeLabel.Name = "PathCharaNoticeLabel";
            this.PathCharaNoticeLabel.Size = new System.Drawing.Size(206, 13);
            this.PathCharaNoticeLabel.TabIndex = 4;
            this.PathCharaNoticeLabel.Text = "Path should be separated with / character";
            // 
            // CoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 208);
            this.Controls.Add(this.PathCharaNoticeLabel);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.VirtualPathLabel);
            this.Controls.Add(this.VirtualPathTxtBox);
            this.Controls.Add(this.SelectGameGrpBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CoreForm";
            this.Text = "White Filecode Generator";
            this.SelectGameGrpBox.ResumeLayout(false);
            this.SelectGameGrpBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox SelectGameGrpBox;
        private System.Windows.Forms.RadioButton FFXiiiLrRadioBtn;
        private System.Windows.Forms.RadioButton FFXiii2RadioBtn;
        private System.Windows.Forms.RadioButton FFXiiiRadioBtn;
        private System.Windows.Forms.TextBox VirtualPathTxtBox;
        private System.Windows.Forms.Label VirtualPathLabel;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.Label PathCharaNoticeLabel;
    }
}

