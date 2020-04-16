using System;
using System.Xml.Serialization;
using ModLib;
using ModLib.Attributes;
using TaleWorlds.InputSystem;

namespace UniversalClose
{
    public class Config : SettingsBase
    {
        #region Config
        [XmlElement]
        [SettingProperty("OkayKey", "The key to press to close the windows with Done button.")]
        public string OkayKey { get; set; }

        [XmlIgnore]
        public InputKey OkayKeyEnum => Enum.TryParse<InputKey>(OkayKey, true, out var buf) ? buf : InputKey.Tab;
        #endregion

        #region SettingsBase implementation
        private const string InstanceID = "UniversalCloseSettings";
        private const string FolderName = "zzzUniversalClose";

        [XmlElement]
        public override string ID { get; set; } = InstanceID;

        public override string ModuleFolderName => FolderName;
        public override string ModName          => "Universal Close";

        /// <summary>
        ///     The currently registered values in the <see cref="SettingsDatabase" />.
        /// </summary>
        public static Config Instance => SettingsDatabase.GetSettings(InstanceID) as Config;

        /// <summary>
        ///     Initializes the <see cref="SettingsDatabase" />.
        /// </summary>
        public static void Initialize()
        {
            FileDatabase.Initialise(FolderName);
            SettingsDatabase.RegisterSettings(FileDatabase.Get<Config>(InstanceID) ?? new Config
            {
                OkayKey = "Tab"
            });
        }
        #endregion
    }
}