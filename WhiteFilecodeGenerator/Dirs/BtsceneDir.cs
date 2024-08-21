using System;
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
                case GameID.xiii3:
                    BtscenePathXIII2LR(virtualPathData, virtualPath);
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
                    var categoryBits = "10";

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


        #region XIII-2 and XIII-LR
        private static void BtscenePathXIII2LR(string[] virtualPathData, string virtualPath)
        {

        }
        #endregion
    }
}