﻿using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(SPInventoryVM))]
    [HarmonyPatch(new[] {typeof(InventoryLogic), typeof(bool), typeof(Func<WeaponComponentData, ItemObject.ItemUsageSetFlags>), typeof(string)})]
    [HarmonyPatch(MethodType.Constructor)]
    internal class GetInventoryVMPostfix
    {
        public static void Postfix(SPInventoryVM __instance)
        {
            UniversalCloseModule.InventoryVM = __instance;
        }
    }
}