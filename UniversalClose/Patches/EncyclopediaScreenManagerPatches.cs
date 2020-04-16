using System;
using HarmonyLib;
using SandBox.View.Map;

namespace UniversalClose.Patches
{
    internal class EncyclopediaScreenManagerPatches
    {
        [HarmonyPatch(typeof(EncyclopediaScreenManager))]
        [HarmonyPatch(new Type[] { })]
        [HarmonyPatch(MethodType.Constructor)]
        internal class ConstructorPatch
        {
            public static void Postfix(EncyclopediaScreenManager __instance)
            {
                // No cleanup required as it is a manager
                DialogHolders.EncyclopediaScreenManager = __instance;
                DebugLogger.Print("Created: EncyclopediaScreenManager");
            }
        }
    }
}