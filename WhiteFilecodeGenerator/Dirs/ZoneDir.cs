using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static WhiteFilecodeGenerator.SharedEnums;

namespace WhiteFilecodeGenerator.Dirs
{
    internal class ZoneDir
    {
        private static readonly List<string> _validExtensions = new List<string>()
        {
            ".wdb", ".clb"
        };

        public static void ProcessZonePath(string[] virtualPathData, string virtualPath, GameID gameID)
        {
            switch (gameID)
            {
                case GameID.xiii:
                    ZonePathXIII(virtualPathData, virtualPath);
                    break;

                case GameID.xiii2:
                case GameID.xiii3:
                    ZonePathXIII2LR(virtualPathData, virtualPath);
                    break;
            }
        }


        #region XIII
        private static void ZonePathXIII(string[] virtualPathData, string virtualPath)
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

            if (startingPortion.StartsWith("zone/z"))
            {
                string categoryBits;
                string reservedBits;

                int zoneID;
                string zoneNumBits;

                switch (fileExtn)
                {
                    case ".wdb":
                        mainTypeBits = Convert.ToString(4, 2).PadLeft(4, '0');

                        // 11 bits
                        categoryBits = Convert.ToString(1, 2).PadLeft(11, '0');

                        // 9 bits
                        reservedBits = "000000000";

                        // 8 bits
                        zoneID = SharedMethods.DeriveNumFromString(virtualPathData[1]);
                        if (zoneID == -1)
                        {
                            SharedMethods.ErrorHalt("Zone number in the wdb filename is invalid");
                        }

                        if (zoneID > 255)
                        {
                            SharedMethods.ErrorHalt("Zone number in the wdb filename is too large. must be from 000 to 255.");
                        }

                        zoneNumBits = Convert.ToString(zoneID, 2).PadLeft(8, '0');

                        // Assemble bits
                        finalComputedBits += mainTypeBits;
                        finalComputedBits += categoryBits;
                        finalComputedBits += reservedBits;
                        finalComputedBits += zoneNumBits;

                        extraInfo += $"MainType (4 bits): {mainTypeBits}\r\n\r\n";
                        extraInfo += $"Category (11 bits): {categoryBits}\r\n\r\n";
                        extraInfo += $"Reserved (9 bits): {reservedBits}\r\n\r\n";
                        extraInfo += $"ZoneNumber (8 bits): {zoneNumBits}";
                        finalComputedBits.Reverse();
                        
                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;

                    case ".clb":
                        mainTypeBits = Convert.ToString(6, 2).PadLeft(4, '0');

                        // 12 bits
                        zoneID = SharedMethods.DeriveNumFromString(virtualPathData[1]);
                        if (zoneID == -1)
                        {
                            SharedMethods.ErrorHalt("Zone number in the wdb filename is invalid or not specified");
                        }

                        if (zoneID > 255)
                        {
                            SharedMethods.ErrorHalt("Zone number in the wdb filename is too large. must be from 000 to 255.");
                        }

                        zoneNumBits = Convert.ToString(zoneID, 2).PadLeft(12, '0');

                        // 8 bits
                        reservedBits = "00000000";

                        // 8 bits
                        var scrID = SharedMethods.DeriveNumFromString(virtualPathData[2]);
                        if (scrID == -1)
                        {
                            SharedMethods.ErrorHalt("scr number in the clb filename is invalid or not specified");
                        }

                        if (scrID > 255)
                        {
                            SharedMethods.ErrorHalt("scr number in the clb filename is too large. must be from 000 to 255.");
                        }

                        var scrNumBits = Convert.ToString(scrID, 2).PadLeft(8, '0');

                        // Assemble bits
                        finalComputedBits += mainTypeBits;
                        finalComputedBits += zoneNumBits;
                        finalComputedBits += reservedBits;
                        finalComputedBits += scrNumBits;

                        extraInfo += $"MainType (4 bits): {mainTypeBits}\r\n\r\n";
                        extraInfo += $"ZoneNumber (12 bits): {zoneNumBits}\r\n\r\n";
                        extraInfo += $"Reserved (8 bits): {reservedBits}\r\n\r\n";
                        extraInfo += $"ScrNumber (8 bits): {scrNumBits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;
                }
            }
        }
        #endregion


        #region XIII-2 and XIII-LR
        private static void ZonePathXIII2LR(string[] virtualPathData, string virtualPath)
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
            var reservedBits = "0000";

            if (startingPortion.StartsWith("zone/z"))
            {
                string categoryBits;
                string reserved2Bits;

                int zoneID;
                string zoneNumBits;

                switch (fileExtn)
                {
                    case ".wdb":
                        // 11 bits
                        categoryBits = Convert.ToString(1, 2).PadLeft(11, '0');

                        // 5 bits
                        reserved2Bits = "00000";

                        // 12 bits
                        zoneID = SharedMethods.DeriveNumFromString(virtualPathData[1]);
                        if (zoneID == -1)
                        {
                            SharedMethods.ErrorHalt("Zone number in the wdb filename is invalid or not specified");
                        }

                        if (zoneID > 1000)
                        {
                            SharedMethods.ErrorHalt("Zone number in the wdb filename is too large. must be from 000 to 1000.");
                        }

                        zoneNumBits = Convert.ToString(zoneID, 2).PadLeft(12, '0');

                        // Assemble bits
                        finalComputedBits += reservedBits;
                        finalComputedBits += categoryBits;
                        finalComputedBits += reserved2Bits;
                        finalComputedBits += zoneNumBits;

                        extraInfo += $"Reserved (4 bits): {reservedBits}\r\n\r\n";
                        extraInfo += $"Category (11 bits): {categoryBits}\r\n\r\n";
                        extraInfo += $"Reserved2 (5 bits): {reserved2Bits}\r\n\r\n";
                        extraInfo += $"ZoneNumber (12 bits): {zoneNumBits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;

                    case ".clb":
                        // 8 bits
                        reserved2Bits = "00000000";

                        // 12 bits
                        zoneID = SharedMethods.DeriveNumFromString(virtualPathData[1]);
                        if (zoneID == -1)
                        {
                            SharedMethods.ErrorHalt("Zone number in the wdb filename is invalid or not specified");
                        }

                        if (zoneID > 1000)
                        {
                            SharedMethods.ErrorHalt("Zone number in the wdb filename is too large. must be from 000 to 1000.");
                        }

                        zoneNumBits = Convert.ToString(zoneID, 2).PadLeft(12, '0');

                        // 8 bits
                        var scrID = SharedMethods.DeriveNumFromString(virtualPathData[2]);
                        if (scrID == -1)
                        {
                            SharedMethods.ErrorHalt("scr number in the clb filename is invalid or not specified");
                        }

                        if (scrID > 255)
                        {
                            SharedMethods.ErrorHalt("scr number in the clb filename is too large. must be from 000 to 255.");
                        }

                        var scrNumBits = Convert.ToString(scrID, 2).PadLeft(8, '0');

                        // Assemble bits
                        finalComputedBits += reservedBits;
                        finalComputedBits += reserved2Bits;
                        finalComputedBits += zoneNumBits;
                        finalComputedBits += scrNumBits;

                        extraInfo += $"Reserved (4 bits): {reservedBits}\r\n\r\n";
                        extraInfo += $"Reserved2 (8 bits): {reserved2Bits}\r\n\r\n";
                        extraInfo += $"ZoneNumber (12 bits): {zoneNumBits}\r\n\r\n";
                        extraInfo += $"ScrNumber (8 bits): {scrNumBits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;
                }
            }
        }
        #endregion
    }
}