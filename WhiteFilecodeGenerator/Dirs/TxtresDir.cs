﻿using System;
using System.IO;
using System.Linq;
using WhiteFilecodeGenerator.Support;
using static WhiteFilecodeGenerator.SharedEnums;

namespace WhiteFilecodeGenerator.Dirs
{
    internal class TxtresDir
    {
        public static void ProcessTxtresPath(string[] virtualPathData, string virtualPath, GameID gameID)
        {
            if (Path.GetExtension(virtualPath) != ".ztr")
            {
                SharedMethods.ErrorHalt("Path does not contain a valid file extension");
            }

            switch (gameID)
            {
                case GameID.xiii:
                    TxtresPathXIII(virtualPathData, virtualPath);
                    break;

                case GameID.xiii2:
                case GameID.xiii3:
                    TxtresPathXIII2LR(virtualPathData, virtualPath);
                    break;
            }
        }


        #region XIII
        private static void TxtresPathXIII(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];

            var finalComputedBits = string.Empty;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            string langIDbits;
            int zoneID;
            string zoneIDbits;

            // 8 bits
            var mainTypeBits = string.Empty;

            if (virtualPathData.Length > 2)
            {
                switch (startingPortion)
                {
                    case "txtres/ac":
                        mainTypeBits = Convert.ToString(228, 2).PadLeft(8, '0');

                        // 4 bits
                        langIDbits = Convert.ToString(GetLangID(Path.GetFileName(virtualPath)), 2).PadLeft(4, '0');

                        // 10 bits
                        if (virtualPathData[2].StartsWith("ac_comn"))
                        {
                            zoneID = 0;
                        }
                        else
                        {
                            zoneID = SharedMethods.UserInput("Enter Zone ID", "Must be from 0 to 255", 0, 255);
                        }
                        zoneIDbits = Convert.ToString(zoneID, 2).PadLeft(10, '0');

                        // 10 bits
                        var acID = SharedMethods.DeriveNumFromString(virtualPathData[2]);

                        if (acID == -1)
                        {
                            SharedMethods.ErrorHalt("ac number in the path is invalid");
                        }

                        if (acID > 999)
                        {
                            SharedMethods.ErrorHalt("ac number in the path is too large. must be from 0 to 999.");
                        }

                        var acIDbits = Convert.ToString(acID, 2).PadLeft(10, '0');

                        // Assemble bits
                        finalComputedBits += mainTypeBits;
                        finalComputedBits += langIDbits;
                        finalComputedBits += zoneIDbits;
                        finalComputedBits += acIDbits;

                        extraInfo += $"MainType (8 bits): {mainTypeBits}\r\n\r\n";
                        extraInfo += $"LanguageID (4 bits): {langIDbits}\r\n\r\n";
                        extraInfo += $"ZoneID (10 bits): {zoneIDbits}\r\n\r\n";
                        extraInfo += $"AcID (10 bits): {acIDbits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;


                    case "txtres/event":
                        mainTypeBits = Convert.ToString(227, 2).PadLeft(8, '0');

                        // 4 bits
                        langIDbits = Convert.ToString(GetLangID(Path.GetFileName(virtualPath)), 2).PadLeft(4, '0');

                        // 10 bits
                        if (virtualPathData[2].StartsWith("ev_comn"))
                        {
                            zoneID = 0;
                        }
                        else
                        {
                            zoneID = SharedMethods.UserInput("Enter Zone ID", "Must be from 0 to 255", 0, 255);
                        }
                        zoneIDbits = Convert.ToString(zoneID, 2).PadLeft(10, '0');

                        // 10 bits
                        var evID = SharedMethods.DeriveNumFromString(virtualPathData[2]);

                        if (evID == -1)
                        {
                            SharedMethods.ErrorHalt("Event number in the path is invalid");
                        }

                        if (evID > 999)
                        {
                            SharedMethods.ErrorHalt("Event number in the path is too large. must be from 0 to 999.");
                        }

                        var evIDbits = Convert.ToString(evID, 2).PadLeft(10, '0');

                        // Assemble bits
                        finalComputedBits += mainTypeBits;
                        finalComputedBits += langIDbits;
                        finalComputedBits += zoneIDbits;
                        finalComputedBits += evIDbits;

                        extraInfo += $"MainType (8 bits): {mainTypeBits}\r\n\r\n";
                        extraInfo += $"LanguageID (4 bits): {langIDbits}\r\n\r\n";
                        extraInfo += $"ZoneID (10 bits): {zoneIDbits}\r\n\r\n";
                        extraInfo += $"EvID (10 bits): {evIDbits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;


                    case "txtres/zone":
                        mainTypeBits = Convert.ToString(229, 2).PadLeft(8, '0');

                        // 4 bits
                        langIDbits = Convert.ToString(GetLangID(Path.GetFileName(virtualPath)), 2).PadLeft(4, '0');

                        // 10 bits
                        var reservedBits = "0000000000";

                        // 10 bits
                        zoneID = SharedMethods.UserInput("Enter Zone ID", "Must be from 0 to 255", 0, 255);
                        zoneIDbits = Convert.ToString(zoneID, 2).PadLeft(10, '0');

                        // Assemble bits
                        finalComputedBits += mainTypeBits;
                        finalComputedBits += langIDbits;
                        finalComputedBits += reservedBits;
                        finalComputedBits += zoneIDbits;

                        extraInfo += $"MainType (8 bits): {mainTypeBits}\r\n\r\n";
                        extraInfo += $"LanguageID (4 bits): {langIDbits}\r\n\r\n";
                        extraInfo += $"Reserved (10 bits): {reservedBits}\r\n\r\n";
                        extraInfo += $"ZoneID (10 bits): {zoneIDbits}";
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
        private static void TxtresPathXIII2LR(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];

            var finalComputedBits = string.Empty;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            string langIDbits;
            string categoryBits;

            int zoneID;
            string zoneIDbits;

            string reservedBits;

            if (virtualPathData.Length > 2)
            {
                switch (startingPortion)
                {
                    case "txtres/ac":
                        // 4 bits
                        langIDbits = Convert.ToString(GetLangID(Path.GetFileName(virtualPath)), 2).PadLeft(4, '0');

                        // 4 bits
                        categoryBits = Convert.ToString(4, 2).PadLeft(4, '0');

                        // 4 bits
                        reservedBits = "0000";

                        // 10 bits
                        if (virtualPathData[2].StartsWith("ac_comn"))
                        {
                            zoneID = 0;
                        }
                        else
                        {
                            zoneID = SharedMethods.UserInput("Enter Zone ID", "Must be from 0 to 1000", 0, 1000);
                        }

                        zoneIDbits = Convert.ToString(zoneID, 2).PadLeft(10, '0');

                        // 10 bits
                        var acID = SharedMethods.DeriveNumFromString(virtualPathData[2]);

                        if (acID == -1)
                        {
                            SharedMethods.ErrorHalt("ac number in the path is invalid");
                        }

                        if (acID > 999)
                        {
                            SharedMethods.ErrorHalt("ac number in the path is too large. must be from 0 to 999.");
                        }

                        var acIDbits = Convert.ToString(acID, 2).PadLeft(10, '0');

                        // Assemble bits
                        finalComputedBits += langIDbits;
                        finalComputedBits += categoryBits;
                        finalComputedBits += reservedBits;
                        finalComputedBits += zoneIDbits;
                        finalComputedBits += acIDbits;

                        extraInfo += $"LanguageID (4 bits): {langIDbits}\r\n\r\n";
                        extraInfo += $"Category (4 bits): {categoryBits}\r\n\r\n";
                        extraInfo += $"Reserved (4 bits): {reservedBits}\r\n\r\n";
                        extraInfo += $"ZoneID (10 bits): {zoneIDbits}\r\n\r\n";
                        extraInfo += $"AcID (10 bits): {acIDbits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;


                    case "txtres/event":
                        // 4 bits
                        langIDbits = Convert.ToString(GetLangID(Path.GetFileName(virtualPath)), 2).PadLeft(4, '0');

                        // 4 bits
                        categoryBits = Convert.ToString(3, 2).PadLeft(4, '0');

                        // 4 bits
                        reservedBits = "0000";

                        // 10 bits
                        if (virtualPathData[2].StartsWith("ev_comn"))
                        {
                            zoneID = 0;
                        }
                        else
                        {
                            zoneID = SharedMethods.UserInput("Enter Zone ID", "Must be from 0 to 1000", 0, 1000);
                        }

                        zoneIDbits = Convert.ToString(zoneID, 2).PadLeft(10, '0');

                        // 10 bits
                        var evID = SharedMethods.DeriveNumFromString(virtualPathData[2]);

                        if (evID == -1)
                        {
                            SharedMethods.ErrorHalt("Event number in the path is invalid");
                        }

                        if (evID > 999)
                        {
                            SharedMethods.ErrorHalt("Event number in the path is too large. must be from 0 to 999.");
                        }

                        var evIDbits = Convert.ToString(evID, 2).PadLeft(10, '0');

                        // Assemble bits
                        finalComputedBits += langIDbits;
                        finalComputedBits += categoryBits;
                        finalComputedBits += reservedBits;
                        finalComputedBits += zoneIDbits;
                        finalComputedBits += evIDbits;

                        extraInfo += $"LanguageID (4 bits): {langIDbits}\r\n\r\n";
                        extraInfo += $"Category (4 bits): {categoryBits}\r\n\r\n";
                        extraInfo += $"Reserved (4 bits): {reservedBits}\r\n\r\n";
                        extraInfo += $"ZoneID (10 bits): {zoneIDbits}\r\n\r\n";
                        extraInfo += $"EvID (10 bits): {evIDbits}";
                        finalComputedBits.Reverse();

                        fileCode = finalComputedBits.BinaryToUInt(0, 32).ToString();

                        SharedMethods.ShowSuccessForm(fileCode, extraInfo);
                        break;


                    case "txtres/zone":
                        // 4 bits
                        langIDbits = Convert.ToString(GetLangID(Path.GetFileName(virtualPath)), 2).PadLeft(4, '0');

                        // 4 bits
                        categoryBits = Convert.ToString(5, 2).PadLeft(4, '0');

                        // 14 bits
                        reservedBits = "00000000000000";

                        // 10 bits
                        zoneID = SharedMethods.UserInput("Enter Zone ID", "Must be from 0 to 1000", 0, 1000);
                        zoneIDbits = Convert.ToString(zoneID, 2).PadLeft(10, '0');

                        // Assemble bits
                        finalComputedBits += langIDbits;
                        finalComputedBits += categoryBits;
                        finalComputedBits += reservedBits;
                        finalComputedBits += zoneIDbits;

                        extraInfo += $"LanguageID (4 bits): {langIDbits}\r\n\r\n";
                        extraInfo += $"Category (4 bits): {categoryBits}\r\n\r\n";
                        extraInfo += $"Reserved (14 bits): {reservedBits}\r\n\r\n";
                        extraInfo += $"ZoneID (10 bits): {zoneIDbits}";
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


        private static int GetLangID(string fileName)
        {
            var langID = 0;

            switch (fileName)
            {
                case "txtres_jp.ztr":
                    langID = 0;
                    break;

                case "txtres_us.ztr":
                    langID = 1;
                    break;

                case "txtes_uk.ztr":
                    langID = 2;
                    break;

                case "txtres_it.ztr":
                    langID = 3;
                    break;

                case "txtres_gr.ztr":
                    langID = 4;
                    break;

                case "txtres_fr.ztr":
                    langID = 5;
                    break;

                case "txtres_sp.ztr":
                    langID = 6;
                    break;

                case "txtres_ru.ztr":
                    langID = 7;
                    break;

                case "txtres_kr.ztr":
                    langID = 8;
                    break;

                case "txtres_ck.ztr":
                    langID = 9;
                    break;

                case "txtres_ch.ztr":
                    langID = 10;
                    break;

                default:
                    SharedMethods.ErrorHalt("Unable to determine the language from the filename. check if the ztr filename, starts with a valid language id.");
                    break;
            }

            return langID;
        }
    }
}