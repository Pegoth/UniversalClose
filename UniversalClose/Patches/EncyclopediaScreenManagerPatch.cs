using System;
using HarmonyLib;
using SandBox.View.Map;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(EncyclopediaScreenManager))]
    [HarmonyPatch(new Type[] { })]
    [HarmonyPatch(MethodType.Constructor)]
    internal class EncyclopediaScreenManagerPostfix
    {
        public static void Postfix(EncyclopediaScreenManager __instance)
        {
            // No cleanup required as it is a manager
            DialogHolders.EncyclopediaScreenManager = __instance;
            DebugLogger.Print("EncyclopediaScreenManager opened");
        }
    }
}