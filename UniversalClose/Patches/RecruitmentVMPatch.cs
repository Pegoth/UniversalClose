using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(RecruitmentVM))]
    [HarmonyPatch(new Type[] { })]
    [HarmonyPatch(MethodType.Constructor)]
    internal class RecruitmentVMPostfix
    {
        public static void Postfix(RecruitmentVM __instance)
        {
            DialogHolders.RecruitmentVM = __instance;
            DebugLogger.Print("RecruitmentVM opened");
        }
    }
}