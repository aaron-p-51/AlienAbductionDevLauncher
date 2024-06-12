using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


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

            cmd += GetDisplayModeArg(displayMode);
            cmd += GetLogModeArg(logMode);
            cmd += GetPlayerMode(playerMode);
            cmd += GetStartInVR(playerMode);
       
            return true;
        }

        public static string GetDisplayModeArg(DisplayMode displayMode)
        {
            switch (displayMode)
            {
                case DisplayMode.Fullscreen:
                    return ""; // Fullscreen does not require arguments
                case DisplayMode.Windowed:
                    return " -windowed -ResX=1280 -ResY=720";
                case DisplayMode.BorderlessWindow:
                    return " -windowed -fullscreen";
            }

            return "";
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
                    return " -log -LogCmds=\"LogTemp Verbose\"";
            }

            return "";
        }

        public static string GetPlayerMode(PlayerMode playerMode)
        {
            switch (playerMode)
            {
                case PlayerMode.VRClient:
                    return " -PlayerMode=VRClient";
                case PlayerMode.VRClientFPS:
                    return " -PlayerMode=VRClient";
                case PlayerMode.VRCenterHost:
                    return " -PlayerMode=VRCenterHost";
                case PlayerMode.ApplicationSet:
                    return "";  // Application Set playermode does not require additional parameters
            }

            return "";
        }

        public static string GetStartInVR(PlayerMode playerMode)
        {
            return playerMode == PlayerMode.VRClient ? " -StartInVR" : "";
        }
    }
}
