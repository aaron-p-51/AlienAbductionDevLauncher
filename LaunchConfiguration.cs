using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AlienAbductionDevLauncher
{
    class LaunchConfiguration
    {
        public string? unrealFilePath;
        public string? uProjectFilePath;
        public PlayerMode playerMode;
        public LogMode logMode;
        public DisplayMode displayMode;
        public bool startInVR;


        public bool GetLaunchCommand(ref string cmd)
        {
            if (unrealFilePath == null || uProjectFilePath == null)
            {
                return false;
            }

            cmd = "START \"\" \"" + unrealFilePath + "\" \"" + uProjectFilePath + "\" -game";

            cmd += GetLogModeArg(logMode);
            cmd += GetPlayerMode(playerMode);

            if (playerMode == PlayerMode.VRClient)
            { 
                cmd += GetStartInVR(startInVR);
            }
       
            return true;
        }

        public static string GetLogModeArg(LogMode logMode)
        {
            switch (logMode)
            {
                case LogMode.None:
                    return "";
                case LogMode.log:
                    return " -log";
                case LogMode.Verbose:
                    return " -verbose";
            }

            return "";
        }

        public static string GetPlayerMode(PlayerMode playerMode)
        {
            switch (playerMode)
            {
                case PlayerMode.ApplicationSet:
                    return "";
                case PlayerMode.VRCenterHost:
                    return " -PlayerMode=ApplicationSet";
                case PlayerMode.VRClient:
                    return " -PlayerMode=VRClient";
            }

            return "";
        }

        public static string GetStartInVR(bool startInVR)
        {
            return startInVR ? " -StartInVR" : "";
        }
    }
}
