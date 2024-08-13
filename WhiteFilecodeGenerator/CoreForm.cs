using System;
using System.Windows.Forms;
using WhiteFilecodeGenerator.Support;
using static WhiteFilecodeGenerator.SharedEnums;

namespace WhiteFilecodeGenerator
{
    public partial class CoreForm : Form
    {
        public CoreForm()
        {
            InitializeComponent();
        }


        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            EnableDisableGUI(false);

            var gameId = new GameID();

            if (FFXiiiRadioBtn.Checked)
            {
                gameId = GameID.xiii;
            }
            else if (FFXiii2RadioBtn.Checked)
            {
                gameId = GameID.xiii2;
            }
            else if (FFXiiiLrRadioBtn.Checked)
            {
                gameId = GameID.xiii3;
            }

            var virtualPath = VirtualPathTxtBox.Text;

            if (string.IsNullOrEmpty(virtualPath) || string.IsNullOrWhiteSpace(virtualPath))
            {
                SharedMethods.DisplayMsgBox("A valid path was not specified", true);
                EnableDisableGUI(true);
            }
            else
            {
                try
                {
                    try
                    {
                        GenerationHelper.GenerateFileCode(virtualPath, gameId);
                    }
                    finally
                    {
                        EnableDisableGUI(true);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToString() != "Error handled")
                    {
                        SharedMethods.DisplayMsgBox(ex.ToString(), true);
                        EnableDisableGUI(true);
                    }
                }
            }
        }


        private void EnableDisableGUI(bool isEnabled)
        {
            if (isEnabled)
            {
                GenerateBtn.Text = "Generate";
            }
            else
            {
                GenerateBtn.Text = "Generating...";
            }

            FFXiiiRadioBtn.Enabled = isEnabled;
            FFXiii2RadioBtn.Enabled = isEnabled;
            FFXiiiLrRadioBtn.Enabled = isEnabled;

            VirtualPathTxtBox.Enabled = isEnabled;

            GenerateBtn.Enabled = isEnabled;
        }
    }
}