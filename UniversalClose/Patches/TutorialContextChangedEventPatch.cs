using HarmonyLib;
using TaleWorlds.Core;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(TutorialContextChangedEvent))]
    [HarmonyPatch(new[] {typeof(TutorialContexts)})]
    [HarmonyPatch(MethodType.Constructor)]
    internal class TutorialContextChangedEventPostfix
    {
        private static TutorialContexts _last = TutorialContexts.None;

        public static void Postfix(TutorialContexts newContext)
        {
            if (newContext == TutorialContexts.None ||
                newContext == TutorialContexts.EncyclopediaWindow)
                return;

            // Hack to check which dialog is active
            switch (_last)
            {
                case TutorialContexts.PartyScreen:
                    DialogHolders.PartyVM = null;
                    break;
                case TutorialContexts.InventoryScreen:
                    DialogHolders.SPInventoryVM = null;
                    break;
                case TutorialContexts.CharacterScreen:
                    DialogHolders.CharacterDeveloperVM = null;
                    break;
                case TutorialContexts.RecruitmentWindow:
                    DialogHolders.RecruitmentVM = null;
                    break;
                case TutorialContexts.ClanScreen:
                    DialogHolders.ClanVM = null;
                    break;
                case TutorialContexts.KingdomScreen:
                    DialogHolders.KingdomManagementVM = null;
                    break;
                case TutorialContexts.QuestsScreen:
                    DialogHolders.QuestsVM = null;
                    break;
            }

            if (_last != TutorialContexts.None)
                DebugLogger.Print("{0} closed (TutorialContexts)", _last);

            _last = newContext;
        }
    }
}