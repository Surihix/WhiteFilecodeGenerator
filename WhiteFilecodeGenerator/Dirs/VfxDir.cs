using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static WhiteFilecodeGenerator.SharedEnums;

namespace WhiteFilecodeGenerator.Dirs
{
    internal class VfxDir
    {
        private static readonly List<string> _validExtensions = new List<string>()
        {
            ".imgb", ".xfv"
        };

        public static void ProcessVfxPath(string[] virtualPathData, string virtualPath, GameID gameID)
        {
            switch (gameID)
            {
                case GameID.xiii:
                    VfxPathXIII(virtualPathData, virtualPath);
                    break;

                case GameID.xiii2:
                case GameID.xiii3:
                    VfxPathXIII2LR(virtualPathData, virtualPath);
                    break;
            }
        }


        #region XIII
        private static void VfxPathXIII(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];
            var fileExtn = Path.GetExtension(virtualPath);

            if (!_validExtensions.Contains(fileExtn))
            {
                SharedMethods.ErrorHalt("Path does not contain a valid file extension for this root directory");
            }

            var finalComputedBits = string.Empty;
            int fileExtnID = 0;
            string fileExtnBits;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            // 4 bits
            var mainTypeBits = string.Empty;

            switch (startingPortion)
            {
                case "vfx/chr":
                    mainTypeBits = Convert.ToString(1, 2).PadLeft(4, '0');

                    // 5 bits
                    var categoryID = DetermineVfxChrCategory(virtualPathData[2][0]);
                    if (categoryID == -1)
                    {
                        SharedMethods.ErrorHalt("Unable to determine category");
                    }

                    var chrCategoryBits = Convert.ToString(categoryID, 2).PadLeft(5, '0');

                    // 10 bits
                    var modelNum = SharedMethods.DeriveNumFromString(virtualPathData[2]);
                    if (modelNum == -1)
                    {
                        SharedMethods.ErrorHalt("Model number in the path is invalid");
                    }

                    if (modelNum > 999)
                    {
                        SharedMethods.ErrorHalt("Model number in the path is too large. must be from 000 to 999.");
                    }

                    var modelNumBits = Convert.ToString(modelNum, 2).PadLeft(10, '0');

                    // 5 bits
                    fileExtnID = fileExtn == ".imgb" ? 2 : 3;
                    fileExtnBits = Convert.ToString(fileExtnID, 2).PadLeft(5, '0');

                    // 8 bits (remaining)
                    var unkIDbits = "00000001";

                    // Assemble bits
                    finalComputedBits += mainTypeBits;
                    finalComputedBits += chrCategoryBits;
                    finalComputedBits += modelNumBits;
                    finalComputedBits += fileExtnBits;
                    finalComputedBits += unkIDbits;

                    extraInfo += $"MainType (4 bits): {mainTypeBits}\r\n\r\n";
                    extraInfo += $"Category (5 bits): {chrCategoryBits}\r\n\r\n";
                    extraInfo += $"ModelNumber (10 bits): {modelNumBits}\r\n\r\n";
                    extraInfo += $"ModelExtension Type (5 bits): {fileExtnBits}\r\n\r\n";
                    extraInfo += $"Unk ID (8 bits): {unkIDbits}";
                    finalComputedBits.Reverse();

                    fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                    SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                    break;


                default:
                    SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                    break;
            }
        }
        #endregion


        #region XIII-2 and XIII-LR
        private static void VfxPathXIII2LR(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];
            var fileExtn = Path.GetExtension(virtualPath);

            if (!_validExtensions.Contains(fileExtn))
            {
                SharedMethods.ErrorHalt("Path does not contain a valid file extension for this root directory");
            }

            var finalComputedBits = string.Empty;
            int fileExtnID = 0;
            string fileExtnBits;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            // 4 bits
            var reservedBits = "0000";

            switch (startingPortion)
            {
                case "vfx/chr":
                    // 5 bits
                    var categoryID = DetermineVfxChrCategory(virtualPathData[2][0]);
                    if (categoryID == -1)
                    {
                        SharedMethods.ErrorHalt("Unable to determine category");
                    }

                    var chrCategoryBits = Convert.ToString(categoryID, 2).PadLeft(5, '0');

                    // 10 bits
                    var modelNum = SharedMethods.DeriveNumFromString(virtualPathData[2]);
                    if (modelNum == -1)
                    {
                        SharedMethods.ErrorHalt("Model number in the path is invalid");
                    }

                    if (modelNum > 999)
                    {
                        SharedMethods.ErrorHalt("Model number in the path is too large. must be from 000 to 999.");
                    }

                    var modelNumBits = Convert.ToString(modelNum, 2).PadLeft(10, '0');

                    // 5 bits
                    fileExtnID = fileExtn == ".imgb" ? 2 : 3;
                    fileExtnBits = Convert.ToString(fileExtnID, 2).PadLeft(5, '0');

                    // 8 bits
                    var unkIDbits = "00000001";

                    // Assemble bits
                    finalComputedBits += reservedBits;
                    finalComputedBits += chrCategoryBits;
                    finalComputedBits += modelNumBits;
                    finalComputedBits += fileExtnBits;
                    finalComputedBits += unkIDbits;

                    extraInfo += $"Reserved (4 bits): {reservedBits}\r\n\r\n";
                    extraInfo += $"Category (5 bits): {chrCategoryBits}\r\n\r\n";
                    extraInfo += $"ModelNumber (10 bits): {modelNumBits}\r\n\r\n";
                    extraInfo += $"ModelExtension Type (5 bits): {fileExtnBits}\r\n\r\n";
                    extraInfo += $"Unk ID (8 bits): {unkIDbits}";
                    finalComputedBits.Reverse();

                    fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                    SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                    break;


                default:
                    SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                    break;
            }
        }
        #endregion


        private static int DetermineVfxChrCategory(char startChara)
        {
            var categoryID = -1;

            switch (startChara)
            {
                case 'c':
                    categoryID = 2;
                    break;

                case 'f':
                    categoryID = 5;
                    break;

                case 'm':
                    categoryID = 12;
                    break;

                case 'n':
                    categoryID = 13;
                    break;

                case 's':
                    categoryID = 18;
                    break;

                case 'w':
                    categoryID = 22;
                    break;
            }

            return categoryID;
        }
    }
}