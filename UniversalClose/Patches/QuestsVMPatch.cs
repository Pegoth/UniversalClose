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
        public static void Prefix(ref Action closeQuestsScreen)
        {
            var origCloseQuestScreen = closeQuestsScreen;

            closeQuestsScreen = () =>
            {
                origCloseQuestScreen?.Invoke();
                DialogHolders.QuestsVM = null;
                DebugLogger.Print("QuestsVM closed (closeQuestsScreen)");
            };
        }

        public static void Postfix(QuestsVM __instance)
        {
            DialogHolders.QuestsVM = __instance;
            DebugLogger.Print("QuestsVM opened");
        }
    }

    [HarmonyPatch(typeof(QuestsVM), "ExecuteClose")]
    internal class QuestsVMExecuteClosePostfix
    {
        public static void Postfix()
        {
            // OnFinalize is not called in QuestsVM, manual cleanup required
            DialogHolders.QuestsVM = null;
            DebugLogger.Print("QuestsVM closed (ExecuteClose)");
        }
    }
}