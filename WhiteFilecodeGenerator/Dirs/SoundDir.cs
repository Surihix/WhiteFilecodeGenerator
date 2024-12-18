﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WhiteFilecodeGenerator.Support;
using static WhiteFilecodeGenerator.SharedEnums;

namespace WhiteFilecodeGenerator.Dirs
{
    internal class SoundDir
    {
        private static readonly List<string> _validExtensions = new List<string>()
        {
            ".scd", ".wpd"
        };

        public static void ProcessSoundPath(string[] virtualPathData, string virtualPath, GameID gameID)
        {
            switch (gameID)
            {
                case GameID.xiii:
                    SoundPathXIII(virtualPathData, virtualPath);
                    break;

                case GameID.xiii2:
                case GameID.xiii3:
                    SoundPathXIII2LR(virtualPathData, virtualPath);
                    break;
            }
        }


        #region XIII
        private static void SoundPathXIII(string[] virtualPathData, string virtualPath)
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

            if (virtualPathData.Length > 2)
            {
                switch (startingPortion)
                {
                    case "sound/pack":
                        mainTypeBits = Convert.ToString(10, 2).PadLeft(4, '0');

                        // 2 bits
                        if (fileExtn == ".scd")
                        {
                            fileExtnID = 2;
                        }
                        else
                        {
                            fileExtnID = 3;
                        }

                        fileExtnBits = Convert.ToString(fileExtnID, 2).PadLeft(2, '0');

                        // 26 bits
                        var soundDirID = SharedMethods.DeriveNumFromString(virtualPathData[2]);
                        string soundIDbits;

                        if (soundDirID == -1)
                        {
                            SharedMethods.ErrorHalt("Sound directory number in path was invalid");
                        }

                        if (soundDirID > 9999)
                        {
                            SharedMethods.ErrorHalt("Sound directory number in the path is too large. must be from 0 to 9999.");
                        }

                        // If .wpd, then do not
                        // prompt for file number
                        if (fileExtn == ".wpd")
                        {
                            soundIDbits = Convert.ToString(soundDirID, 2).PadLeft(26, '0');
                        }
                        else
                        {
                            var scdFileID = SharedMethods.UserInput("Enter SCD number", "Must be from 0 to 999", 0, 999);

                            var scdFileIDmerged = int.Parse(soundDirID.ToString() + "" + scdFileID.ToString().PadLeft(3, '0'));
                            soundIDbits = Convert.ToString(scdFileIDmerged, 2).PadLeft(26, '0');
                        }

                        // Assemble bits
                        finalComputedBits += mainTypeBits;
                        finalComputedBits += fileExtnBits;
                        finalComputedBits += soundIDbits;

                        extraInfo += $"MainType (4 bits): {mainTypeBits}\r\n\r\n";
                        extraInfo += $"Category (2 bits): {fileExtnBits}\r\n\r\n";
                        extraInfo += $"Dir & FileID (26 bits): {soundIDbits}";
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


        #region XIII-2 and XIII-LR
        private static void SoundPathXIII2LR(string[] virtualPathData, string virtualPath)
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

            if (virtualPathData.Length > 2)
            {
                switch (startingPortion)
                {
                    case "sound/pack":
                        // 2 bits
                        if (fileExtn == ".scd")
                        {
                            fileExtnID = 2;
                        }
                        else
                        {
                            fileExtnID = 3;
                        }

                        fileExtnBits = Convert.ToString(fileExtnID, 2).PadLeft(2, '0');

                        // 26 bits
                        var soundDirID = SharedMethods.DeriveNumFromString(virtualPathData[2]);
                        string soundIDbits;

                        if (soundDirID == -1)
                        {
                            SharedMethods.ErrorHalt("Sound directory number specified was invalid");
                        }

                        if (soundDirID > 9999)
                        {
                            SharedMethods.ErrorHalt("Sound directory number in the path is too large. must be from 0 to 9999.");
                        }

                        // If .wpd, then do not
                        // prompt for file number
                        if (fileExtn == ".wpd")
                        {
                            soundIDbits = Convert.ToString(soundDirID, 2).PadLeft(26, '0');
                        }
                        else
                        {
                            var scdFileID = SharedMethods.UserInput("Enter SCD number", "Must be from 0 to 999", 0, 999);

                            var scdFileIDmerged = int.Parse(soundDirID.ToString() + "" + scdFileID.ToString().PadLeft(3, '0'));
                            soundIDbits = Convert.ToString(scdFileIDmerged, 2).PadLeft(26, '0');
                        }

                        // Assemble bits
                        finalComputedBits += reservedBits;
                        finalComputedBits += fileExtnBits;
                        finalComputedBits += soundIDbits;

                        extraInfo += $"Reserved (4 bits): {reservedBits}\r\n\r\n";
                        extraInfo += $"Category (2 bits): {fileExtnBits}\r\n\r\n";
                        extraInfo += $"Dir & FileID (26 bits): {soundIDbits}";
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
    }
}