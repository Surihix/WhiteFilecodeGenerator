using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WhiteFilecodeGenerator.Support;
using static WhiteFilecodeGenerator.SharedEnums;

namespace WhiteFilecodeGenerator.Dirs
{
    internal class MotDir
    {
        private static GameID _gameID = new GameID();

        public static void ProcessMotPath(string[] virtualPathData, string virtualPath, GameID gameID)
        {
            _gameID = gameID;

            switch (gameID)
            {
                case GameID.xiii:
                    MotPathXIII(virtualPathData, virtualPath);
                    break;

                case GameID.xiii2:
                case GameID.xiii3:
                    MotPathXIII2LR(virtualPathData, virtualPath);
                    break;
            }
        }


        #region XIII
        private static void MotPathXIII(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];
            var fileExtn = Path.GetExtension(virtualPath);

            if (fileExtn != ".bin")
            {
                SharedMethods.ErrorHalt("Path does not contain a valid file extension for this root directory");
            }

            var finalComputedBits = string.Empty;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            // 4 bits
            var mainTypeBits = string.Empty;

            if (virtualPathData.Length > 3)
            {
                mainTypeBits = Convert.ToString(2, 2).PadLeft(4, '0');

                switch (startingPortion)
                {
                    case "mot/exte":
                    case "mot/fa":
                    case "mot/mon":
                    case "mot/npc":
                    case "mot/pc":
                    case "mot/summon":
                    case "mot/weapon":
                        // 5 bits
                        var chrCategoryBits = Convert.ToString(DetermineChrCategory(virtualPathData[1]), 2).PadLeft(5, '0');

                        // 10 bits
                        var modelID = SharedMethods.DeriveNumFromString(virtualPathData[2].Split('_')[1]);
                        if (modelID == -1)
                        {
                            SharedMethods.ErrorHalt("Model number specified is invalid");
                        }

                        if (modelID > 999)
                        {
                            SharedMethods.ErrorHalt("Model number in the path is too large. must be from 0 to 999.");
                        }

                        var modelIDbits = Convert.ToString(modelID, 2).PadLeft(10, '0');

                        // 8 bits + 5 bits
                        string fileNameTypeBits;
                        string fileIDbits;

                        if (virtualPathData[3].StartsWith("lsdpack"))
                        {
                            fileNameTypeBits = "00000000";
                            fileIDbits = "01010";
                        }
                        else
                        {
                            var validStartChara = false;
                            if (SharedMethods.LettersList.Contains(virtualPathData[3][0]))
                            {
                                validStartChara = true;
                            }

                            if (validStartChara)
                            {
                                fileNameTypeBits = Convert.ToString(SharedMethods.LettersList.IndexOf(virtualPathData[3][0]), 2).PadLeft(8, '0');
                                fileIDbits = Convert.ToString(SharedMethods.DeriveNumFromString(virtualPathData[3]), 2).PadLeft(5, '0');
                            }
                            else
                            {
                                fileNameTypeBits = "00000000";
                                fileIDbits = "00000";
                            }
                        }

                        // Assemble bits
                        finalComputedBits += mainTypeBits;
                        finalComputedBits += chrCategoryBits;
                        finalComputedBits += modelIDbits;
                        finalComputedBits += fileNameTypeBits;
                        finalComputedBits += fileIDbits;

                        extraInfo += $"MainType (4 bits): {mainTypeBits}\r\n\r\n";
                        extraInfo += $"Category (5 bits): {chrCategoryBits}\r\n\r\n";
                        extraInfo += $"ModelID (10 bits): {modelIDbits}\r\n\r\n";
                        extraInfo += $"FileNameType (8 bits): {fileNameTypeBits}\r\n\r\n";
                        extraInfo += $"FileID (5 bits): {fileIDbits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;


                    default:
                        SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                        break;
                }
            }
        }
        #endregion


        #region XIII-2 and XIII-LR
        private static void MotPathXIII2LR(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];
            var fileExtn = Path.GetExtension(virtualPath);

            if (_gameID == GameID.xiii2)
            {
                if (fileExtn != ".bin")
                {
                    SharedMethods.ErrorHalt("Path does not contain a valid file extension for this root directory");
                }
            }
            else
            {
                var validExtensions = new List<string>
                {
                    ".bin", ".wpd"
                };

                if (!validExtensions.Contains(fileExtn))
                {
                    SharedMethods.ErrorHalt("Path does not contain a valid file extension for this root directory");
                }
            }

            var finalComputedBits = string.Empty;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            if (virtualPathData.Length > 3)
            {
                switch (startingPortion)
                {
                    case "mot/exte":
                    case "mot/fa":
                    case "mot/mon":
                    case "mot/npc":
                    case "mot/pc":
                    case "mot/summon":
                    case "mot/weapon":
                        // 9 bits
                        var chrCategoryBits = Convert.ToString(DetermineChrCategory(virtualPathData[1]), 2).PadLeft(9, '0');

                        // 10 bits
                        var modelID = SharedMethods.DeriveNumFromString(virtualPathData[2].Split('_')[1]);
                        if (modelID == -1)
                        {
                            SharedMethods.ErrorHalt("Model number specified is invalid");
                        }

                        if (modelID > 999)
                        {
                            SharedMethods.ErrorHalt("Model number in the path is too large. must be from 0 to 999.");
                        }

                        var modelIDbits = Convert.ToString(modelID, 2).PadLeft(10, '0');

                        // 8 bits + 5 bits
                        string fileNameTypeBits;
                        string fileIDbits;

                        if (virtualPathData[3].StartsWith("lsdpack"))
                        {
                            fileNameTypeBits = "00000000";
                            fileIDbits = "01010";
                        }
                        else
                        {
                            var validStartChara = false;
                            if (SharedMethods.LettersList.Contains(virtualPathData[3][0]))
                            {
                                validStartChara = true;
                            }

                            if (validStartChara)
                            {
                                fileNameTypeBits = Convert.ToString(SharedMethods.LettersList.IndexOf(virtualPathData[3][0]), 2).PadLeft(8, '0');
                                fileIDbits = Convert.ToString(SharedMethods.DeriveNumFromString(virtualPathData[3]), 2).PadLeft(5, '0');
                            }
                            else
                            {
                                fileNameTypeBits = "00000000";
                                fileIDbits = "00000";
                            }
                        }

                        // Assemble bits
                        finalComputedBits += chrCategoryBits;
                        finalComputedBits += modelIDbits;
                        finalComputedBits += fileNameTypeBits;
                        finalComputedBits += fileIDbits;

                        extraInfo += $"Category (9 bits): {chrCategoryBits}\r\n\r\n";
                        extraInfo += $"ModelID (10 bits): {modelIDbits}\r\n\r\n";
                        extraInfo += $"FileNameType (8 bits): {fileNameTypeBits}\r\n\r\n";
                        extraInfo += $"FileID (5 bits): {fileIDbits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;


                    default:
                        SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                        break;
                }
            }
        }
        #endregion


        private static int DetermineChrCategory(string dirName)
        {
            var categoryID = 0;

            switch (dirName)
            {
                case "pc":
                    categoryID = 2;
                    break;

                case "exte":
                    categoryID = 4;
                    break;

                case "fa":
                    categoryID = 5;
                    break;

                case "mon":
                    categoryID = 12;
                    break;

                case "npc":
                    categoryID = 13;
                    break;

                case "summon":
                    categoryID = 18;
                    break;

                case "weapon":
                    categoryID = 22;
                    break;

                default:
                    SharedMethods.ErrorHalt("Unable to determine category from the filename. check if the mot filename, starts with a valid category string.");
                    break;
            }

            return categoryID;
        }


    }
}