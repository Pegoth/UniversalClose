using System;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft.WeaponDesign;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace UniversalClose
{
    public class UniversalCloseModule : MBSubModuleBase
    {
        public static SPInventoryVM  InventoryVM    { get; set; }
        public static PartyVM        PartyVM        { get; set; }
        public static RecruitmentVM  RecruitmentVM  { get; set; }
        public static WeaponDesignVM WeaponDesignVM { get; set; }

        protected override void OnSubModuleLoad()
        {
            new Harmony("hu.pegoth.UniversalClose").PatchAll();

            base.OnSubModuleLoad();
        }

        protected override void OnApplicationTick(float dt)
        {
            try
            {
                Tick();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while running {0}: {1}", nameof(Tick), ex);
            }

            base.OnApplicationTick(dt);
        }

        [SuppressMessage("ReSharper", "InvertIf")]
        private static void Tick()
        {
            if (Campaign.Current == null || !InputKey.Tab.IsPressed())
                return;

            if (PartyVM != null)
            {
                Traverse.Create(PartyVM).Method("ExecuteDone").GetValue();
                Input.ClearKeys();
            }

            if (InventoryVM != null)
            {
                Traverse.Create(InventoryVM).Method("ExecuteCompleteTranstactions").GetValue();
                Input.ClearKeys();
            }

            if (WeaponDesignVM != null && WeaponDesignVM.IsInFinalCraftingStage)
            {
                Traverse.Create(WeaponDesignVM).Method("ExecuteFinalizeCrafting").GetValue();
                Input.ClearKeys();
            }

            if (RecruitmentVM != null)
            {
                Traverse.Create(RecruitmentVM).Method("ExecuteDone").GetValue();
                Input.ClearKeys();
            }
        }
    }
}