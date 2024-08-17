using WhiteFilecodeGenerator.Dirs;
using WhiteFilecodeGenerator.Support;

namespace WhiteFilecodeGenerator
{
    internal class GenerationHelper
    {
        public static void GenerateFileCode(string virtualPath, SharedEnums.GameID gameID)
        {
            var virtualPathData = virtualPath.Split('/');

            if (virtualPathData.Length < 2) 
            {
                SharedMethods.ErrorHalt("A Valid path is not specified");
            }

            switch (virtualPathData[0])
            {
                case "chr":
                    ChrDir.ProcessChrPath(virtualPathData, virtualPath, gameID);
                    break;

                case "event":
                    EventDir.ProcessEventPath(virtualPathData, virtualPath, gameID);
                    break;

                case "gui":
                    GuiDir.ProcessGuiPath(virtualPathData, virtualPath, gameID);
                    break;

                case "mot":
                    MotDir.ProcessMotPath(virtualPathData, virtualPath, gameID);
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