using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.Library;
using Debug = System.Diagnostics.Debug;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(ViewModel), "OnFinalize")]
    internal static class ViewModelPostfix
    {
        private static readonly Dictionary<string, PropertyInfo> PropertyInfos;

        static ViewModelPostfix()
        {
            // Build property list from DialogHolders
            PropertyInfos = typeof(DialogHolders).GetProperties(BindingFlags.Public | BindingFlags.Static)
                                                 .ToDictionary(info => info.PropertyType.Name, info => info);
        }

        public static void Postfix(ViewModel __instance)
        {
            // Check if property exists in DialogHolders
            if (!PropertyInfos.ContainsKey(__instance.GetType().Name))
                return;

            Debug.Print($"Auto cleanup: {__instance.GetType().Name}");

            // If so clear it
            PropertyInfos[__instance.GetType().Name].SetValue(null, null);
        }
    }

#if DEBUG
    [HarmonyPatch(typeof(ViewModel), "ExecuteCommand")]
    internal static class ViewModelExecuteCommandPostfix
    {
        public static void Postfix(ViewModel __instance, string commandName)
        {
            Debug.Print($"ExecuteCommand: {__instance.GetType().Name}, {commandName}");
        }
    }
#endif
}