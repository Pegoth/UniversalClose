using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(ClanVM))]
    [HarmonyPatch(new[] {typeof(Action), typeof(Action<MobileParty>), typeof(Action)})]
    [HarmonyPatch(MethodType.Constructor)]
    internal class ClanVMPostfix
    {
        public static void Postfix(ClanVM __instance)
        {
            DialogHolders.ClanVM = __instance;
        }
    }

    [HarmonyPatch(typeof(ClanVM), "ExecuteClose")]
    internal class ClanVMExecuteClosePostfix
    {
        public static void Postfix()
        {
            // OnFinalize is not called in ClanVM, manual cleanup required
            DialogHolders.ClanVM = null;
        }
    }
}