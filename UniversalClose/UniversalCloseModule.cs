using System;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace UniversalClose
{
    public class UniversalCloseModule : MBSubModuleBase
    {
        public static bool IsInquaryVisible { get; set; }

        protected override void OnSubModuleLoad()
        {
            Config.Initialize();
            new Harmony("hu.pegoth.UniversalClose").PatchAll();
        }

        [SuppressMessage("ReSharper", "InvertIf")]
        protected override void OnApplicationTick(float dt)
        {
            try
            {
                // Check if it is required to run the logic
                if (Campaign.Current == null || !Config.Instance.OkayKeyEnum.IsReleased())
                    return;

                // Check for which dialog to close (if any)
                if (DialogHolders.EncyclopediaScreenManager != null && DialogHolders.EncyclopediaScreenManager.IsEncyclopediaOpen)
                {
                    DialogHolders.EncyclopediaScreenManager.CloseEncyclopedia();
                    DebugLogger.Print("Closing encyclopedia");
                }
                else if (DialogHolders.PartyVM != null)
                {
                    if (IsInquaryVisible)
                    {
                        InformationManager.HideInquiry();
                        PartyScreenManager.ClosePartyPresentation(false);
                        DebugLogger.Print("Closing PartyVM with inquiry open");
                    }
                    else
                    {
                        Traverse.Create(DialogHolders.PartyVM).Method("ExecuteDone").GetValue();
                        DebugLogger.Print("Closing PartyVM (or opened inquiry)");
                    }
                }
                else if (DialogHolders.SPInventoryVM != null)
                {
                    Traverse.Create(DialogHolders.SPInventoryVM).Method("ExecuteRemoveZeroCounts").GetValue();
                    Traverse.Create(DialogHolders.SPInventoryVM).Method("ExecuteCompleteTranstactions").GetValue();
                    DebugLogger.Print("Closing SPInventoryVM");
                }
                else if (DialogHolders.WeaponDesignVM != null && DialogHolders.WeaponDesignVM.IsInFinalCraftingStage)
                {
                    Traverse.Create(DialogHolders.WeaponDesignVM).Method("ExecuteFinalizeCrafting").GetValue();
                    DebugLogger.Print("Closing WeaponDesignVM");
                }
                else if (DialogHolders.RecruitmentVM != null)
                {
                    if (IsInquaryVisible)
                    {
                        InformationManager.HideInquiry();
                        Traverse.Create(DialogHolders.RecruitmentVM).Method("OnDone").GetValue();
                        DebugLogger.Print("Closing RecruitmentVM with inquiry open");
                    }
                    else
                    {
                        Traverse.Create(DialogHolders.RecruitmentVM).Method("ExecuteDone").GetValue();
                        DebugLogger.Print("Closing RecruitmentVM (or opened inquiry)");
                    }
                }
                else if (DialogHolders.CharacterDeveloperVM != null)
                {
                    Traverse.Create(DialogHolders.CharacterDeveloperVM).Method("ExecuteDone").GetValue();
                    DebugLogger.Print("Closing CharacterDeveloperVM");
                }
                else if (DialogHolders.ClanVM != null)
                {
                    Traverse.Create(DialogHolders.ClanVM).Method("ExecuteClose").GetValue();
                    DebugLogger.Print("Closing ClanVM");
                }
                else if (DialogHolders.TownManagementVM != null)
                {
                    Traverse.Create(DialogHolders.TownManagementVM).Method("ExecuteDone").GetValue();
                    DebugLogger.Print("Closing TownManagementVM");
                }
                else if (DialogHolders.KingdomManagementVM != null)
                {
                    Traverse.Create(DialogHolders.KingdomManagementVM).Method("ExecuteClose").GetValue();
                    DebugLogger.Print("Closing KingdomManagementVM");
                }
                else if (DialogHolders.QuestsVM != null)
                {
                    //Traverse.Create(DialogHolders.QuestsVM).Method("ExecuteClose").GetValue();
                    DebugLogger.Print("Closing QuestVM");
                }
            }
            catch (Exception ex)
            {
                DebugLogger.Print("Error in tick: {0}", ex);
            }
        }
    }
}