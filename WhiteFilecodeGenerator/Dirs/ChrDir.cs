﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WhiteFilecodeGenerator.Support;
using static WhiteFilecodeGenerator.SharedEnums;

namespace WhiteFilecodeGenerator.Dirs
{
    internal class ChrDir
    {
        public static void ProcessChrPath(string[] virtualPathData, string virtualPath, GameID gameID)
        {
            switch (gameID)
            {
                case GameID.xiii:
                    ChrPathXIII(virtualPathData, virtualPath);
                    break;

                case GameID.xiii2:
                    ChrPathXIII2(virtualPathData, virtualPath);
                    break;

                case GameID.xiii3:
                    ChrPathXIIILR(virtualPathData, virtualPath);
                    break;
            }
        }


        #region XIII
        private static void ChrPathXIII(string[] virtualPathData, string virtualPath)
        {
            var validExtensions = new List<string>
            {
                ".trb", ".imgb"
            };

            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];
            var fileExtn = Path.GetExtension(virtualPath);

            if (!validExtensions.Contains(fileExtn))
            {
                SharedMethods.ErrorHalt("Path does not contain a valid file extension");
            }

            var finalComputedBits = string.Empty;
            int fileExtnID = 0;
            string fileExtnBits;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            // 4 bits
            var mainTypeBits = string.Empty;

            if (virtualPathData.Length > 2)
            {
                switch (startingPortion)
                {
                    case "chr/fa":
                    case "chr/mon":
                    case "chr/npc":
                    case "chr/pc":
                    case "chr/summon":
                    case "chr/weapon":
                        mainTypeBits = Convert.ToString(1, 2).PadLeft(4, '0');

                        // 5 bits
                        var chrCategoryBits = Convert.ToString(DetermineChrCategory(virtualPathData[1]), 2).PadLeft(5, '0');

                        // 10 bits
                        var modelID = SharedMethods.DeriveNumFromString(virtualPathData[2]);
                        if (modelID == -1)
                        {
                            SharedMethods.ErrorHalt("Model number in the path is invalid");
                        }

                        if (modelID > 999)
                        {
                            SharedMethods.ErrorHalt("Model number in the path is too large. must be from 0 to 999.");
                        }

                        var modelIDbits = Convert.ToString(modelID, 2).PadLeft(10, '0');

                        // 5 bits
                        fileExtnID = fileExtn == ".imgb" ? 0 : 1;
                        fileExtnBits = Convert.ToString(fileExtnID, 2).PadLeft(5, '0');

                        // 8 bits (remaining)
                        var reservedBits = "00000000";

                        // Assemble bits
                        finalComputedBits += mainTypeBits;
                        finalComputedBits += chrCategoryBits;
                        finalComputedBits += modelIDbits;
                        finalComputedBits += fileExtnBits;
                        finalComputedBits += reservedBits;

                        extraInfo += $"MainType (4 bits): {mainTypeBits}\r\n\r\n";
                        extraInfo += $"Category (5 bits): {chrCategoryBits}\r\n\r\n";
                        extraInfo += $"ModelID (10 bits): {modelIDbits}\r\n\r\n";
                        extraInfo += $"ModelExtension Type (5 bits): {fileExtnBits}\r\n\r\n";
                        extraInfo += $"Reserved (8 bits): {reservedBits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;


                    default:
                        SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                        break;
                }
            }
            else
            {
                SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
            }
        }
        #endregion


        #region XIII-2
        private static void ChrPathXIII2(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];
            var fileExtn = Path.GetExtension(virtualPath);

            var validExtensions = new List<string>
            {
                ".trb", ".imgb", ".mpk"
            };

            if (!validExtensions.Contains(fileExtn))
            {
                SharedMethods.ErrorHalt("Path does not contain a valid file extension");
            }

            var finalComputedBits = string.Empty;
            int fileExtnID = 0;
            string fileExtnBits;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            // 4 bits
            var reservedBits = "0000";

            if (virtualPathData.Length > 2)
            {
                switch (startingPortion)
                {
                    case "chr/exte":
                    case "chr/fa":
                    case "chr/mon":
                    case "chr/npc":
                    case "chr/pc":
                    case "chr/summon":
                    case "chr/weapon":
                        // 5 bits
                        var chrCategoryBits = Convert.ToString(DetermineChrCategory(virtualPathData[1]), 2).PadLeft(5, '0');

                        // 10 bits
                        var modelID = SharedMethods.DeriveNumFromString(virtualPathData[2]);
                        if (modelID == -1)
                        {
                            SharedMethods.ErrorHalt("Model number specified is invalid");
                        }

                        if (modelID > 999)
                        {
                            SharedMethods.ErrorHalt("Model number in the path is too large. must be from 0 to 999.");
                        }

                        var modelIDbits = Convert.ToString(modelID, 2).PadLeft(10, '0');

                        // 5 bits
                        switch (fileExtn)
                        {
                            case ".imgb":
                                fileExtnID = 0;
                                break;

                            case ".trb":
                                fileExtnID = 1;
                                break;

                            case ".mpk":
                                fileExtnID = 4;
                                break;
                        }

                        fileExtnBits = Convert.ToString(fileExtnID, 2).PadLeft(5, '0');

                        // 8 bits
                        var mpkID = 0;
                        if (fileExtn == ".mpk")
                        {
                            if (virtualPath.Contains("_rain"))
                            {
                                mpkID = 1;
                            }
                            else if (virtualPath.Contains("_snow"))
                            {
                                mpkID = 2;
                            }
                        }
                        var mpkIDbits = Convert.ToString(mpkID, 2).PadLeft(8, '0');

                        // Assemble bits
                        finalComputedBits += reservedBits;
                        finalComputedBits += chrCategoryBits;
                        finalComputedBits += modelIDbits;
                        finalComputedBits += fileExtnBits;
                        finalComputedBits += mpkIDbits;

                        extraInfo += $"Reserved (4 bits): {reservedBits}\r\n\r\n";
                        extraInfo += $"Category (5 bits): {chrCategoryBits}\r\n\r\n";
                        extraInfo += $"ModelID (10 bits): {modelIDbits}\r\n\r\n";
                        extraInfo += $"ModelExtension Type (5 bits): {fileExtnBits}\r\n\r\n";
                        extraInfo += $"MpkID (8 bits): {mpkIDbits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;


                    default:
                        SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                        break;
                }
            }
            else
            {
                SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
            }
        }
        #endregion


        #region XIII-LR
        private static void ChrPathXIIILR(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];
            var fileExtn = Path.GetExtension(virtualPath);

            var validExtensions = new List<string>
            {
                ".trb", ".imgb"
            };

            if (!validExtensions.Contains(fileExtn))
            {
                SharedMethods.ErrorHalt("Path does not contain a valid file extension");
            }

            var finalComputedBits = string.Empty;
            int fileExtnID = 0;
            string fileExtnBits;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            // 4 bits
            var reservedBits = "0000";

            if (virtualPathData.Length > 2)
            {
                switch (startingPortion)
                {
                    case "chr/exte":
                    case "chr/fa":
                    case "chr/mon":
                    case "chr/npc":
                    case "chr/pc":
                    case "chr/summon":
                    case "chr/weapon":
                        // 5 bits
                        var chrCategoryBits = Convert.ToString(DetermineChrCategory(virtualPathData[1]), 2).PadLeft(5, '0');

                        // 10 bits
                        var modelID = SharedMethods.DeriveNumFromString(virtualPathData[2]);
                        if (modelID == -1)
                        {
                            SharedMethods.ErrorHalt("Model number specified is invalid");
                        }

                        if (modelID > 999)
                        {
                            SharedMethods.ErrorHalt("Model number in the path is too large. must be from 0 to 999.");
                        }

                        var modelIDbits = Convert.ToString(modelID, 2).PadLeft(10, '0');

                        // 5 bits
                        switch (fileExtn)
                        {
                            case ".imgb":
                                fileExtnID = 0;
                                break;

                            case ".trb":
                                fileExtnID = 1;
                                break;
                        }

                        fileExtnBits = Convert.ToString(fileExtnID, 2).PadLeft(5, '0');

                        // 8 bits
                        var reserved2Bits = "00000000";

                        // Assemble bits
                        finalComputedBits += reservedBits;
                        finalComputedBits += chrCategoryBits;
                        finalComputedBits += modelIDbits;
                        finalComputedBits += fileExtnBits;
                        finalComputedBits += reserved2Bits;

                        extraInfo += $"Reserved (4 bits): {reservedBits}\r\n\r\n";
                        extraInfo += $"Category (5 bits): {chrCategoryBits}\r\n\r\n";
                        extraInfo += $"ModelID (10 bits): {modelIDbits}\r\n\r\n";
                        extraInfo += $"ModelExtension Type (5 bits): {fileExtnBits}\r\n\r\n";
                        extraInfo += $"Reserved2 (8 bits): {reserved2Bits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;


                    default:
                        SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                        break;
                }
            }
            else
            {
                SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
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
                    SharedMethods.ErrorHalt("Unable to determine category from the filename. check if the chr filename, starts with a valid category string.");
                    break;
            }

            return categoryID;
        }
    }
}