﻿using System.Windows.Forms;
using WhiteFilecodeGenerator.Support;

namespace WhiteFilecodeGenerator
{
    public partial class InputForm : Form
    {
        public InputForm(string formTitle, string rangeTxt, int min, int max)
        {
            InitializeComponent();

            Text = formTitle;
            RangeTxtLabel.Text = rangeTxt;
            RangeTxtLabel.Left = (Width - RangeTxtLabel.Width) / 2;
            RangeNumericUpDown.Minimum = min;
            RangeNumericUpDown.Maximum = max;
        }


        private void InputOkBtn_Click(object sender, System.EventArgs e)
        {
            SharedMethods.NumInput = (int)RangeNumericUpDown.Value;

            Close();
        }
    }
}