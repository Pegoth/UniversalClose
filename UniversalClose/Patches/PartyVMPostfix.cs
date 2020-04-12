using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(PartyVM))]
    [HarmonyPatch(new[] {typeof(Game), typeof(PartyScreenLogic), typeof(string)})]
    [HarmonyPatch(MethodType.Constructor)]
    internal class PartyVMPostfix
    {
        public static void Postfix(PartyVM __instance)
        {
            UniversalCloseModule.PartyVM = __instance;
        }
    }
}