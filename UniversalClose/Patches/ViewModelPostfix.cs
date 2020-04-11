using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft.WeaponDesign;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu;
using TaleWorlds.Library;

namespace UniversalClose.Patches
{
    [HarmonyPatch(typeof(ViewModel), "OnFinalize")]
    public class ViewModelPostfix
    {
        public static void Postfix(ViewModel __instance)
        {
            switch (__instance)
            {
                case SPInventoryVM _:
                    UniversalCloseModule.InventoryVM = null;
                    break;
                case PartyVM _:
                    UniversalCloseModule.PartyVM = null;
                    break;
                case WeaponDesignVM _:
                    UniversalCloseModule.WeaponDesignVM = null;
                    break;
                case RecruitmentVM _:
                    UniversalCloseModule.RecruitmentVM = null;
                    break;
            }
        }
    }
}