using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.Quests;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(QuestsVM))]
    [HarmonyPatch(new[] {typeof(Action)})]
    [HarmonyPatch(MethodType.Constructor)]
    internal class QuestsVMPostfix
    {
        public static void Postfix(QuestsVM __instance)
        {
            DialogHolders.QuestsVM = __instance;
        }
    }

    [HarmonyPatch(typeof(QuestsVM), "ExecuteClose")]
    internal class QuestsVMExecuteClosePostfix
    {
        public static void Postfix()
        {
            // OnFinalize is not called in QuestsVM, manual cleanup required
            DialogHolders.QuestsVM = null;
        }
    }
}