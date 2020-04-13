using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(CharacterDeveloperVM))]
    [HarmonyPatch(new[] {typeof(Action)})]
    [HarmonyPatch(MethodType.Constructor)]
    internal class CharacterDeveloperVMPostfix
    {
        public static void Postfix(CharacterDeveloperVM __instance)
        {
            DialogHolders.CharacterDeveloperVM = __instance;
            DebugLogger.Print("CharacterDeveloperVM opened");
        }
    }

    [HarmonyPatch(typeof(CharacterDeveloperVM), "ExecuteDone")]
    internal class CharacterDeveloperVMExecuteDonePostfix
    {
        public static void Postfix()
        {
            // OnFinalize is not called in CharacterDeveloperVM, manual cleanup required
            DialogHolders.CharacterDeveloperVM = null;
            DebugLogger.Print("CharacterDeveloperVM closed (ExecuteDone)");
        }
    }

    [HarmonyPatch(typeof(CharacterDeveloperVM), "ExecuteCancel")]
    internal class CharacterDeveloperVMExecuteCancelPostfix
    {
        public static void Postfix()
        {
            // OnFinalize is not called in CharacterDeveloperVM, manual cleanup required
            DialogHolders.CharacterDeveloperVM = null;
            DebugLogger.Print("CharacterDeveloperVM closed (ExecuteCancel)");
        }
    }
}