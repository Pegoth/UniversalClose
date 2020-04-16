using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Newtonsoft.Json.Linq;

namespace UniversalClose.BuildTasks
{
    public class ProjectLauncherUpdaterTask : Task
    {
        [Required]
        public string LaunchSettingsPath { get; set; }

        public override bool Execute()
        {
            if (!File.Exists(LaunchSettingsPath))
                throw new ArgumentException($"File does not exists: {LaunchSettingsPath}");

            var ser = new XmlSerializer(typeof(LauncherDataModel));
            using (var fxml = File.Open(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Mount and Blade II Bannerlord", "Configs", "LauncherData.xml"), FileMode.Open, FileAccess.Read))
            {
                if (!(ser.Deserialize(fxml) is LauncherDataModel data))
                    return false;

                var sb = new StringBuilder("/singleplayer _MODULES_*");
                foreach (var mod in data.SingleplayerData.ModDatas.UserModData)
                    if (mod.IsSelected)
                        sb.Append(mod.Id)
                          .Append("*");
                sb.Append("_MODULES_");

                var obj = JObject.Parse(File.ReadAllText(LaunchSettingsPath));
                obj["profiles"]["UniversalClose"]["commandLineArgs"] = sb.ToString();
                File.WriteAllText(LaunchSettingsPath, obj.ToString());
            }

            return true;
        }

        #region XML Models
        [XmlRoot("UserData")]
        public class LauncherDataModel
        {
            [XmlElement("GameType")]
            public string GameType { get; set; }

            [XmlElement("SingleplayerData")]
            public DataModel SingleplayerData { get; set; }

            [XmlElement("MultiplayerData")]
            public DataModel MultiplayerData { get; set; }
        }

        public class DataModel
        {
            [XmlElement("ModDatas")]
            public ModDatasModel ModDatas { get; set; }
        }

        public class ModDatasModel
        {
            [XmlElement("UserModData")]
            public UserModDataModel[] UserModData { get; set; }
        }

        public class UserModDataModel
        {
            [XmlElement("Id")]
            public string Id { get; set; }

            [XmlElement("IsSelected")]
            public bool IsSelected { get; set; }
        }
        #endregion
    }
}