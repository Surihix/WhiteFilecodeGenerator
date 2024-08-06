using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WhiteFilecodeGenerator
{
    internal class SharedMethods
    {
        public static int NumInput { get; set; }

        public static void DisplayMsgBox(string msg, bool isError)
        {
            if (isError)
            {
                MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void ErrorHalt(string errorMsg)
        {
            DisplayMsgBox(errorMsg, true);
            throw new Exception("Handled");
        }


        public static int DeriveNumFromString(string fieldName)
        {
            var foundNumsList = new List<int>();

            for (int i = 0; i < fieldName.Length; i++)
            {
                if (fieldName[i] == '.')
                {
                    break;
                }

                if (fieldName[i] == ' ')
                {
                    ErrorHalt("Number contains spaces");
                    break;
                }

                if (char.IsDigit(fieldName[i]))
                {
                    foundNumsList.Add(int.Parse(Convert.ToString(fieldName[i])));
                }
            }

            var foundNumStr = "";
            foreach (var n in foundNumsList)
            {
                foundNumStr += n;
            }

            var hasParsed = int.TryParse(foundNumStr, out int foundNum);

            if (hasParsed)
            {
                return foundNum;
            }
            else
            {
                return -1;
            }
        }


        public static int UserInput(string formTitle, string rangeTxt, int min, int max)
        {
            var numInputForm = new InputForm(formTitle, rangeTxt, min, max);
            System.Media.SystemSounds.Asterisk.Play();
            numInputForm.ShowDialog();

            return NumInput;
        }


        public static void ShowSuccessForm(string fileCode, string extraInfo)
        {
            var successForm = new SuccessForm(fileCode, extraInfo);
            System.Media.SystemSounds.Asterisk.Play();
            successForm.ShowDialog();
        }
    }
}