# Universal Close Mount & Blade II: Bannerlord Module

Download from https://www.nexusmods.com/mountandblade2bannerlord/mods/431/

# Building the project

To build first open $(SolutionDir)GameTarget.targets file and edit according to the following:

* **GameDirectory**: The path to the main Bannerlord directory (not the executable).
* **CopyToGameModules**: Will auto copy the mod on build to the Modules directory of the game.
* **GetLaunchModulesFromGame**: Will update the project launch arguments based on the game launcher options. This is to use the same mods and in the same order as the launcher would.
