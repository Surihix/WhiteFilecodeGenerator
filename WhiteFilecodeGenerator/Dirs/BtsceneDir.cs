﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WhiteFilecodeGenerator.Support;
using static WhiteFilecodeGenerator.SharedEnums;

namespace WhiteFilecodeGenerator.Dirs
{
    internal class BtsceneDir
    {
        private static readonly List<string> _validExtensions = new List<string>()
        {
            ".bin", ".wdb"
        };

        public static void ProcessBtscenePath(string[] virtualPathData, string virtualPath, GameID gameID)
        {
            switch (gameID)
            {
                case GameID.xiii:
                    BtscenePathXIII(virtualPathData, virtualPath);
                    break;

                case GameID.xiii2:
                    BtscenePathXIII2(virtualPathData, virtualPath);
                    break;

                case GameID.xiii3:
                    BtscenePathXIIILR(virtualPathData, virtualPath);
                    break;
            }
        }


        #region XIII
        private static void BtscenePathXIII(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];
            var fileExtn = Path.GetExtension(virtualPath);

            if (!_validExtensions.Contains(fileExtn))
            {
                SharedMethods.ErrorHalt("Path does not contain a valid file extension for this root directory");
            }

            var finalComputedBits = string.Empty;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            // 4 bits
            var mainTypeBits = string.Empty;

            if (virtualPathData.Length > 2)
            {
                if (startingPortion == "btscene/wdb" && virtualPathData[2] == "entry")
                {
                    mainTypeBits = Convert.ToString(11, 2).PadLeft(4, '0');

                    // 2 bits
                    var categoryBits = Convert.ToString(2, 2).PadLeft(2, '0');

                    // 10 bits
                    var reservedBits = "0000000000";

                    // 16 bits
                    var fileId = SharedMethods.DeriveNumFromString(virtualPathData[3]);
                    if (fileId == -1)
                    {
                        SharedMethods.ErrorHalt("btscene file number in the path is invalid");
                    }

                    fileId++;

                    if (fileId > 65535)
                    {
                        SharedMethods.ErrorHalt("btscene file number in the path is too large. must be from 0 to 65534.");
                    }

                    var fileIdBits = Convert.ToString(fileId, 2).PadLeft(16, '0');

                    // Assemble bits
                    finalComputedBits += mainTypeBits;
                    finalComputedBits += categoryBits;
                    finalComputedBits += reservedBits;
                    finalComputedBits += fileIdBits;

                    extraInfo += $"MainType (4 bits): {mainTypeBits}\r\n\r\n";
                    extraInfo += $"Category (2 bits): {categoryBits}\r\n\r\n";
                    extraInfo += $"Reserved (10 bits): {reservedBits}\r\n\r\n";
                    extraInfo += $"FileId (16 bits): {fileIdBits}";
                    finalComputedBits.Reverse();

                    fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                    SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                }
                else
                {
                    SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                }
            }
            else
            {
                SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
            }
        }
        #endregion


        #region XIII-2
        private static void BtscenePathXIII2(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];
            var fileExtn = Path.GetExtension(virtualPath);

            if (!_validExtensions.Contains(fileExtn))
            {
                SharedMethods.ErrorHalt("Path does not contain a valid file extension for this root directory");
            }

            var finalComputedBits = string.Empty;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            if (virtualPathData.Length > 2)
            {
                switch (startingPortion)
                {
                    case "btscene/pack":
                        if (virtualPathData.Length > 3)
                        {
                            var letterIdBits = string.Empty;

                            switch (virtualPathData[2])
                            {
                                case "clb":
                                case "wdb":
                                    if (virtualPathData[2] == "clb" && virtualPathData[3].StartsWith("z"))
                                    {
                                        // 8 bits
                                        var categoryAbits = Convert.ToString(1, 2).PadLeft(8, '0');

                                        // 9 bits
                                        var categoryBbits = Convert.ToString(2, 2).PadLeft(9, '0');

                                        // 15 bits
                                        var zoneID = SharedMethods.DeriveNumFromString(virtualPathData[3]);
                                        if (zoneID == -1)
                                        {
                                            SharedMethods.ErrorHalt("Zone number in the path is invalid");
                                        }

                                        if (zoneID > 1000)
                                        {
                                            SharedMethods.ErrorHalt("Zone number in the path is too large. must be from 0 to 1000.");
                                        }

                                        var zoneIDbits = Convert.ToString(zoneID, 2).PadLeft(15, '0');

                                        // Assemble bits
                                        finalComputedBits += categoryAbits;
                                        finalComputedBits += categoryBbits;
                                        finalComputedBits += zoneIDbits;

                                        extraInfo += $"CategoryA (8 bits): {categoryAbits}\r\n\r\n";
                                        extraInfo += $"CategoryB (9 bits): {categoryBbits}\r\n\r\n";
                                        extraInfo += $"ZoneID (15 bits): {zoneIDbits}";
                                        finalComputedBits.Reverse();

                                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                                    }
                                    else if (!virtualPath.EndsWith("vwx.bin"))
                                    {
                                        // 5 bits
                                        letterIdBits = Convert.ToString(GetLetterID(virtualPathData[3]), 2).PadLeft(5, '0');

                                        // 12 bits
                                        var fileId = SharedMethods.DeriveNumFromString(virtualPathData[3]);
                                        if (fileId == -1)
                                        {
                                            SharedMethods.ErrorHalt("File number in the path is invalid");
                                        }

                                        if (fileId > 999)
                                        {
                                            SharedMethods.ErrorHalt("File number in the path is too large. must be from 0 to 999.");
                                        }

                                        var fileIdBits = Convert.ToString(fileId, 2).PadLeft(12, '0');

                                        // 15 bits
                                        var paddedBits = Convert.ToString(short.MaxValue, 2);

                                        // Assemble bits
                                        finalComputedBits += letterIdBits;
                                        finalComputedBits += fileIdBits;
                                        finalComputedBits += paddedBits;

                                        extraInfo += $"Category (5 bits): {letterIdBits}\r\n\r\n";
                                        extraInfo += $"Reserved (12 bits): {fileIdBits}\r\n\r\n";
                                        extraInfo += $"FileId (15 bits): {paddedBits}";
                                        finalComputedBits.Reverse();

                                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                                    }
                                    else
                                    {
                                        SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                                    }
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
                        break;


                    case "btscene/wdb":
                        if (virtualPathData[2] == "entry" && virtualPathData.Length > 3)
                        {
                            // 6 bits
                            var categoryBits = Convert.ToString(2, 2).PadLeft(6, '0');

                            // 10 bits
                            var reservedBits = "0000000000";

                            // 16 bits
                            var fileId = SharedMethods.DeriveNumFromString(virtualPathData[3]);

                            if (fileId == -1)
                            {
                                SharedMethods.ErrorHalt("btsc file number in the path is invalid");
                            }

                            fileId++;

                            if (fileId > 65535)
                            {
                                SharedMethods.ErrorHalt("btsc file number in the path is too large. must be from 0 to 65534.");
                            }

                            var fileIdBits = Convert.ToString(fileId, 2).PadLeft(16, '0');

                            // Assemble bits
                            finalComputedBits += categoryBits;
                            finalComputedBits += reservedBits;
                            finalComputedBits += fileIdBits;

                            extraInfo += $"Category (6 bits): {categoryBits}\r\n\r\n";
                            extraInfo += $"Reserved (10 bits): {reservedBits}\r\n\r\n";
                            extraInfo += $"FileId (16 bits): {fileIdBits}";
                            finalComputedBits.Reverse();

                            fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                            SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        }
                        else
                        {
                            SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                        }
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
        private static void BtscenePathXIIILR(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];
            var fileExtn = Path.GetExtension(virtualPath);

            if (!_validExtensions.Contains(fileExtn))
            {
                SharedMethods.ErrorHalt("Path does not contain a valid file extension for this root directory");
            }

            var finalComputedBits = string.Empty;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            if (virtualPathData.Length > 2)
            {
                switch (startingPortion)
                {
                    case "btscene/pack":
                        if (virtualPathData.Length > 3)
                        {
                            // 5 bits
                            var letterIdBits = Convert.ToString(GetLetterID(virtualPathData[3]), 2).PadLeft(5, '0');

                            // 12 bits
                            var fileId = SharedMethods.DeriveNumFromString(virtualPathData[3]);
                            if (fileId == -1)
                            {
                                SharedMethods.ErrorHalt("File number in the path is invalid");
                            }

                            if (fileId > 999)
                            {
                                SharedMethods.ErrorHalt("File number in the path is too large. must be from 0 to 999.");
                            }

                            var fileIdBits = Convert.ToString(fileId, 2).PadLeft(12, '0');

                            // 15 bits
                            var paddedBits = Convert.ToString(short.MaxValue, 2);

                            // Assemble bits
                            finalComputedBits += letterIdBits;
                            finalComputedBits += fileIdBits;
                            finalComputedBits += paddedBits;

                            extraInfo += $"Category (5 bits): {letterIdBits}\r\n\r\n";
                            extraInfo += $"Reserved (12 bits): {fileIdBits}\r\n\r\n";
                            extraInfo += $"FileId (15 bits): {paddedBits}";
                            finalComputedBits.Reverse();

                            fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                            SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        }
                        else
                        {
                            SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                        }
                        break;


                    case "btscene/wdb":
                        if (virtualPathData[2] == "entry" && virtualPathData.Length > 3)
                        {
                            // 6 bits
                            var categoryBits = Convert.ToString(2, 2).PadLeft(6, '0');

                            // 10 bits
                            var reservedBits = "0000000000";

                            // 16 bits
                            var fileId = SharedMethods.DeriveNumFromString(virtualPathData[3]);

                            if (fileId == -1)
                            {
                                SharedMethods.ErrorHalt("btsc file number in the path is invalid");
                            }

                            fileId++;

                            if (fileId > 65535)
                            {
                                SharedMethods.ErrorHalt("btsc file number in the path is too large. must be from 0 to 65534.");
                            }

                            var fileIdBits = Convert.ToString(fileId, 2).PadLeft(16, '0');

                            // Assemble bits
                            finalComputedBits += categoryBits;
                            finalComputedBits += reservedBits;
                            finalComputedBits += fileIdBits;

                            extraInfo += $"Category (6 bits): {categoryBits}\r\n\r\n";
                            extraInfo += $"Reserved (10 bits): {reservedBits}\r\n\r\n";
                            extraInfo += $"FileId (16 bits): {fileIdBits}";
                            finalComputedBits.Reverse();

                            fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                            SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        }
                        else
                        {
                            SharedMethods.ErrorHalt("Unable to generate filecode. check if the path starts with a valid directory.");
                        }
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


        private static int GetLetterID(string fileName)
        {
            if (!SharedMethods.LettersList.Contains(fileName[0]))
            {
                SharedMethods.ErrorHalt("Unable to get letter from filename");
            }

            return SharedMethods.LettersList.IndexOf(fileName[0]) + 1;
        }
    }
}