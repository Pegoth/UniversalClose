﻿using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(RecruitmentVM))]
    [HarmonyPatch(new Type[] { })]
    [HarmonyPatch(MethodType.Constructor)]
    internal class GetRecruitmentVMPostfix
    {
        public static void Postfix(RecruitmentVM __instance)
        {
            UniversalCloseModule.RecruitmentVM = __instance;
        }
    }
}