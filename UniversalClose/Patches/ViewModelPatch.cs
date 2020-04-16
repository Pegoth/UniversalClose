using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.Library;

namespace UniversalClose.Patches
{
    internal static class ViewModelPatches
    {
        private static readonly Dictionary<Type, PropertyInfo> PropertyInfos;

        static ViewModelPatches()
        {
            // Build property list from DialogHolders
            PropertyInfos = typeof(DialogHolders).GetProperties(BindingFlags.Public | BindingFlags.Static)
                                                 .ToDictionary(info => info.PropertyType, info => info);
        }

        [HarmonyPatch(typeof(ViewModel))]
        [HarmonyPatch(new Type[0])]
        [HarmonyPatch(MethodType.Constructor)]
        internal static class ConstructorPatch
        {
            public static void Postfix(ViewModel __instance)
            {
                DebugLogger.Print("Created: {0}", __instance.GetType().Name);

                // Check if property exists in DialogHolders
                if (!PropertyInfos.ContainsKey(__instance.GetType()))
                    return;

                PropertyInfos[__instance.GetType()].SetValue(null, __instance);
            }
        }

#if DEBUG
        [HarmonyPatch(typeof(ViewModel), "ExecuteCommand")]
        internal static class ExecuteCommandPatch
        {
            public static void Postfix(ViewModel __instance, string commandName)
            {
                DebugLogger.Print("Called: ExecuteCommand, {0}, {1}", __instance.GetType().Name, commandName);
            }
        }
#endif
    }
}