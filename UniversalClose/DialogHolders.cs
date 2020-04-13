using SandBox.View.Map;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft.WeaponDesign;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement;
using TaleWorlds.CampaignSystem.ViewModelCollection.Quests;

namespace UniversalClose
{
    public static class DialogHolders
    {
        public static SPInventoryVM             SPInventoryVM             { get; set; }
        public static PartyVM                   PartyVM                   { get; set; }
        public static RecruitmentVM             RecruitmentVM             { get; set; }
        public static WeaponDesignVM            WeaponDesignVM            { get; set; }
        public static CharacterDeveloperVM      CharacterDeveloperVM      { get; set; }
        public static ClanVM                    ClanVM                    { get; set; }
        public static QuestsVM                  QuestsVM                  { get; set; }
        public static TownManagementVM          TownManagementVM          { get; set; }
        public static EncyclopediaScreenManager EncyclopediaScreenManager { get; set; }
        public static KingdomManagementVM       KingdomManagementVM       { get; set; }
    }
}