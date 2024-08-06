namespace WhiteFilecodeGenerator
{
    partial class InputForm
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
            this.RangeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.RangeTxtLabel = new System.Windows.Forms.Label();
            this.InputOkBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RangeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // RangeNumericUpDown
            // 
            this.RangeNumericUpDown.Location = new System.Drawing.Point(77, 58);
            this.RangeNumericUpDown.Name = "RangeNumericUpDown";
            this.RangeNumericUpDown.Size = new System.Drawing.Size(68, 20);
            this.RangeNumericUpDown.TabIndex = 0;
            // 
            // RangeTxtLabel
            // 
            this.RangeTxtLabel.AutoSize = true;
            this.RangeTxtLabel.Location = new System.Drawing.Point(81, 24);
            this.RangeTxtLabel.Name = "RangeTxtLabel";
            this.RangeTxtLabel.Size = new System.Drawing.Size(60, 13);
            this.RangeTxtLabel.TabIndex = 1;
            this.RangeTxtLabel.Text = "RangeText";
            this.RangeTxtLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InputOkBtn
            // 
            this.InputOkBtn.Location = new System.Drawing.Point(74, 101);
            this.InputOkBtn.Name = "InputOkBtn";
            this.InputOkBtn.Size = new System.Drawing.Size(75, 23);
            this.InputOkBtn.TabIndex = 2;
            this.InputOkBtn.Text = "OK";
            this.InputOkBtn.UseVisualStyleBackColor = true;
            this.InputOkBtn.Click += new System.EventHandler(this.InputOkBtn_Click);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 144);
            this.Controls.Add(this.InputOkBtn);
            this.Controls.Add(this.RangeTxtLabel);
            this.Controls.Add(this.RangeNumericUpDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputForm";
            this.Text = "Input";
            ((System.ComponentModel.ISupportInitialize)(this.RangeNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown RangeNumericUpDown;
        private System.Windows.Forms.Label RangeTxtLabel;
        private System.Windows.Forms.Button InputOkBtn;
    }
}