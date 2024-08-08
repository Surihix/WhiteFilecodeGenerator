﻿using WhiteFilecodeGenerator.Dirs;
using WhiteFilecodeGenerator.Support;

namespace WhiteFilecodeGenerator
{
    internal class GenerationHelper
    {
        public static void GenerateFileCode(string virtualPath, SharedEnums.GameID gameID)
        {
            var virtualPathData = virtualPath.Split('/');

            switch (virtualPathData[0])
            {
                case "chr":
                    ChrDir.ProcessChrPath(virtualPathData, virtualPath, gameID);
                    break;

                case "gui":
                    GuiDir.ProcessGuiPath(virtualPathData, virtualPath, gameID);
                    break;

                case "sound":
                    SoundDir.ProcessSoundPath(virtualPathData, virtualPath, gameID);
                    break;

                case "txtres":
                    TxtresDir.ProcessTxtresPath(virtualPathData, virtualPath, gameID);
                    break;

                case "vfx":
                    VfxDir.ProcessVfxPath(virtualPathData, virtualPath, gameID);
                    break;

                case "zone":
                    ZoneDir.ProcessZonePath(virtualPathData, virtualPath, gameID);
                    break;

                default:
                    SharedMethods.ErrorHalt("Valid root directory is not specified");
                    break;
            }
        }
    }
}