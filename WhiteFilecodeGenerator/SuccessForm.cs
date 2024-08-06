using System.Windows.Forms;

namespace WhiteFilecodeGenerator
{
    public partial class SuccessForm : Form
    {
        public SuccessForm(string fileCode, string extraInfo)
        {
            InitializeComponent();

            CodeTxtBox.Text = fileCode;
            CodeTxtBox.BackColor = System.Drawing.SystemColors.Window;

            ExtraInfoTxtBox.Text = extraInfo;
            ExtraInfoTxtBox.BackColor = System.Drawing.SystemColors.Window;
        }


        private void OkBtn_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}