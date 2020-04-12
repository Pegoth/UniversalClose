using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using TaleWorlds.CampaignSystem;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;
using UniversalClose.Config;

namespace UniversalClose
{
    public class UniversalCloseModule : MBSubModuleBase
    {
        public static ConfigModel Config { get; set; }

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

                if (DialogHolders.PartyVM != null)
                {
                    Traverse.Create(DialogHolders.PartyVM).Method("ExecuteDone").GetValue();
                }
                else if (DialogHolders.SPInventoryVM != null)
                {
                    Traverse.Create(DialogHolders.SPInventoryVM).Method("ExecuteRemoveZeroCounts").GetValue();
                    Traverse.Create(DialogHolders.SPInventoryVM).Method("ExecuteCompleteTranstactions").GetValue();
                }
                else if (DialogHolders.WeaponDesignVM != null && DialogHolders.WeaponDesignVM.IsInFinalCraftingStage)
                {
                    Traverse.Create(DialogHolders.WeaponDesignVM).Method("ExecuteFinalizeCrafting").GetValue();
                }
                else if (DialogHolders.RecruitmentVM != null)
                {
                    Traverse.Create(DialogHolders.RecruitmentVM).Method("ExecuteDone").GetValue();
                }
                else if (DialogHolders.CharacterDeveloperVM != null)
                {
                    Traverse.Create(DialogHolders.CharacterDeveloperVM).Method("ExecuteDone").GetValue();
                }
                else if (DialogHolders.ClanVM != null)
                {
                    Traverse.Create(DialogHolders.ClanVM).Method("ExecuteClose").GetValue();
                }
                else if (DialogHolders.QuestsVM != null)
                {
                    Traverse.Create(DialogHolders.QuestsVM).Method("ExecuteClose").GetValue();
                }
                else if (DialogHolders.TownManagementVM != null)
                {
                    Traverse.Create(DialogHolders.TownManagementVM).Method("ExecuteDone").GetValue();
                }
                else if (DialogHolders.EncyclopediaScreenManager.IsEncyclopediaOpen)
                {
                    DialogHolders.EncyclopediaScreenManager.CloseEncyclopedia();
                }
                else
                {
                    // Do not clear keys if nothing relevant was open.
                    return;
                }

                Input.ClearKeys();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while running {0}: {1}", nameof(OnApplicationTick), ex);
            }
        }
    }
}