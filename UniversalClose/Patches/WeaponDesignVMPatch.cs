using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft.WeaponDesign;
using TaleWorlds.Core;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(WeaponDesignVM))]
    [HarmonyPatch(new[] {typeof(Crafting), typeof(ICraftingCampaignBehavior), typeof(Action), typeof(Func<CraftingAvailableHeroItemVM>)})]
    [HarmonyPatch(MethodType.Constructor)]
    internal class WeaponDesignVMPostfix
    {
        public static void Postfix(WeaponDesignVM __instance)
        {
            DialogHolders.WeaponDesignVM = __instance;
            DebugLogger.Print("WeaponDesignVM opened");
        }
    }
}