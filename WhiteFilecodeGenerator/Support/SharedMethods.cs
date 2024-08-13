using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WhiteFilecodeGenerator.Support
{
    internal class SharedMethods
    {
        public static int NumInput { get; set; }


        public static readonly List<char> LettersList = new List<char>
        {
            'a','b','c','d','e','f','g','h','i','j','k','l','m',
            'n','o','p','q','r','s','t','u','v','w','x','y','z'
        };


        public static void DisplayMsgBox(string msg, bool isError)
        {
            if (isError)
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public static void ErrorHalt(string errorMsg)
        {
            DisplayMsgBox(errorMsg, true);
            throw new Exception("Error handled");
        }


        public static int DeriveNumFromString(string numberedString)
        {
            var foundNumsList = new List<int>();

            for (int i = 0; i < numberedString.Length; i++)
            {
                if (numberedString[i] == '.')
                {
                    break;
                }

                if (numberedString[i] == ' ')
                {
                    ErrorHalt("Number contains spaces");
                    break;
                }

                if (char.IsDigit(numberedString[i]))
                {
                    foundNumsList.Add(int.Parse(Convert.ToString(numberedString[i])));
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