using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft.WeaponDesign;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;
using UniversalClose.Config;

namespace UniversalClose
{
    public class UniversalCloseModule : MBSubModuleBase
    {
        public static SPInventoryVM  InventoryVM    { get; set; }
        public static PartyVM        PartyVM        { get; set; }
        public static RecruitmentVM  RecruitmentVM  { get; set; }
        public static WeaponDesignVM WeaponDesignVM { get; set; }
        public static ConfigModel    Config         { get; set; }

        protected override void OnSubModuleLoad()
        {
            // Get the location to the config files
            var configFolder = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException(), "..", "..", "Config"));
            var configFile   = Path.Combine(configFolder, "universalclose.json");
            var schemaFile   = Path.Combine(configFolder, "universalclose-schema.json");

            // Check if config files exists
            if (!File.Exists(configFile))
                throw new InvalidOperationException("Config file does not exists.");
            if (!File.Exists(schemaFile))
                throw new InvalidOperationException("Config schema file does not exists, reinstall the mod to get it.");

            // Parse the schema and the config
            var schema = JSchema.Parse(File.ReadAllText(schemaFile));
            var config = JObject.Parse(File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "Config", "universalclose.json")));

            // Check if config is valid
            if (!config.IsValid(schema, out IList<string> errorMessages))
                throw new Exception($"Errors while trying to validate config: {string.Join(", ", errorMessages)}");

            // Save the config to the property
            Config = config.ToObject<ConfigModel>(JsonSerializer.Create(new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Populate
            }));

            new Harmony("hu.pegoth.UniversalClose").PatchAll();
            base.OnSubModuleLoad();
        }

        [SuppressMessage("ReSharper", "InvertIf")]
        protected override void OnApplicationTick(float dt)
        {
            try
            {
                if (Campaign.Current == null || !Input.IsKeyPressed(Config.OkayKey))
                    return;

                if (PartyVM != null)
                {
                    Traverse.Create(PartyVM).Method("ExecuteDone").GetValue();
                    Input.ClearKeys();
                    return;
                }

                if (InventoryVM != null)
                {
                    Traverse.Create(InventoryVM).Method("ExecuteRemoveZeroCounts").GetValue();
                    Traverse.Create(InventoryVM).Method("ExecuteCompleteTranstactions").GetValue();
                    Input.ClearKeys();
                    return;
                }

                if (WeaponDesignVM != null && WeaponDesignVM.IsInFinalCraftingStage)
                {
                    Traverse.Create(WeaponDesignVM).Method("ExecuteFinalizeCrafting").GetValue();
                    Input.ClearKeys();
                    return;
                }

                if (RecruitmentVM != null)
                {
                    Traverse.Create(RecruitmentVM).Method("ExecuteDone").GetValue();
                    Input.ClearKeys();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while running {0}: {1}", nameof(OnApplicationTick), ex);
            }
        }
    }
}