using System;
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
            switch (gameID)
            {
                case GameID.xiii:
                    TxtresPathXIII(virtualPathData, virtualPath);
                    break;

                case GameID.xiii2:
                    TxtresPathXIII2(virtualPathData, virtualPath);
                    break;

                case GameID.xiii3:
                    TxtresPathXIIILR(virtualPathData, virtualPath);
                    break;
            }
        }


        #region XIII
        private static void TxtresPathXIII(string[] virtualPathData, string virtualPath)
        {
            var startingPortion = virtualPathData[0] + "/" + virtualPathData[1];
            var fileExtn = Path.GetExtension(virtualPath);

            if (fileExtn != ".ztr")
            {
                SharedMethods.ErrorHalt("Path does not contain a valid file extension");
            }

            var finalComputedBits = string.Empty;

            string fileCode = string.Empty;
            string extraInfo = string.Empty;

            string langIDbits;
            string zoneIDbits;

            // 8 bits
            var mainTypeBits = string.Empty;

            switch (startingPortion)
            {
                case "txtres/event":
                    mainTypeBits = Convert.ToString(227, 2).PadLeft(8, '0');

                    // 4 bits
                    var langID = GetZTRLangID(Path.GetFileNameWithoutExtension(virtualPath));
                    langIDbits = Convert.ToString(langID, 2).PadLeft(4, '0');

                    // 10 bits
                    var zoneID = SharedMethods.UserInput("Enter Zone ID", "Must be from 0 to 255", 0, 255);
                    zoneIDbits = Convert.ToString(zoneID, 2).PadLeft(10, '0');

                    // 10 bits
                    var eventNum = SharedMethods.DeriveNumFromString(virtualPathData[2]);

                    if (eventNum == -1)
                    {
                        SharedMethods.ErrorHalt("Event number in the path is invalid");
                    }

                    if (eventNum > 999)
                    {
                        SharedMethods.ErrorHalt("Event number in the path is too large. must be from 000 to 999.");
                    }

                    var eventNumBits = Convert.ToString(eventNum, 2).PadLeft(10, '0');

                    // Assemble bits
                    finalComputedBits += mainTypeBits;
                    finalComputedBits += langIDbits;
                    finalComputedBits += zoneIDbits;
                    finalComputedBits += eventNumBits;

                    extraInfo += $"MainType (8 bits): {mainTypeBits}\r\n\r\n";
                    extraInfo += $"LanguageID (4 bits): {langIDbits}\r\n\r\n";
                    extraInfo += $"ZoneID (10 bits): {zoneIDbits}\r\n\r\n";
                    extraInfo += $"EventNumber (10 bits): {eventNumBits}";
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


        #region XIII-2
        private static void TxtresPathXIII2(string[] virtualPathData, string virtualPath)
        {

        }
        #endregion


        #region XIII-LR
        private static void TxtresPathXIIILR(string[] virtualPathData, string virtualPath)
        {

        }
        #endregion


        private static int GetZTRLangID(string fileName)
        {
            var langID = 0;

            switch (fileName)
            {
                case "txtres_jp":
                    langID = 0;
                    break;

                case "txtres_us":
                    langID = 1;
                    break;

                case "txtes_uk":
                    langID = 2;
                    break;

                case "txtres_it":
                    langID = 3;
                    break;

                case "txtres_gr":
                    langID = 4;
                    break;

                case "txtres_fr":
                    langID = 5;
                    break;

                case "txtres_sp":
                    langID = 6;
                    break;

                case "txtres_ru":
                    langID = 7;
                    break;

                case "txtres_kr":
                    langID = 8;
                    break;

                case "txtres_ck":
                    langID = 9;
                    break;

                case "txtres_ch":
                    langID = 10;
                    break;
            }

            return langID;
        }
    }
}