# Alien Abduction

Repo for location based VR game Alien Abduction (Unreal Engine 5.3)

## Use Instructions
1. Browse to set Unreal Editor to the path of UnrealEditor.exe
2. Browse to set .uproject to AlienAbduction.uproject
3. Click launch to launch the project

## Options
- Log Mode - Change the log setting of the launched project<br>
		-- None - No log will be shown<br>
		-- Log - Default logging will be shown<br>
		-- Verbose - Verbose logging will be shown<br>
  
- Display Mode - Change the display mode of the launched project<br>
		-- Fullscreen - Launch the project in fullscreen<br>
		-- Windowed - Launch the project in a separate window<br>
		-- Borderless Window - Launch the project in a borderless window<br>

- Player Mode - Change the play type of the launched project<br>
    -- VRClient -- Launch the game in VR as a player (this simulates the customers at VR centers<br>
    -- VRClientFPS -- Launch the game as a first person verison (non VR) version of the player.<br>
                      This player is able to do everything the VRClient is just not in VR for easier testing.
    -- VRCenterHost -- Launch the game as a host (server) this is what the operators of VR centers will use<br>
    -- Application Set -- Launch the game and use what ever setting is set in the GameInstance for AlienAbduction
