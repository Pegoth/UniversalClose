using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(KingdomManagementVM))]
    [HarmonyPatch(new[] {typeof(Action), typeof(Action), typeof(Action<Army>), typeof(KingdomState.KingdomCategories)})]
    [HarmonyPatch(MethodType.Constructor)]
    internal class KingdomManagementVMPostfix
    {
        public static void Postfix(KingdomManagementVM __instance)
        {
            DialogHolders.KingdomManagementVM = __instance;
        }
    }
}