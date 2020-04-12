using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(TownManagementVM))]
    [HarmonyPatch(new Type[] { })]
    [HarmonyPatch(MethodType.Constructor)]
    internal class TownManagementVMPostfix
    {
        public static void Postfix(TownManagementVM __instance)
        {
            DialogHolders.TownManagementVM = __instance;
        }
    }
}