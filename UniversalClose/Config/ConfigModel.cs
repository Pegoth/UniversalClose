using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TaleWorlds.InputSystem;

namespace UniversalClose.Config
{
    public class ConfigModel
    {
        [JsonProperty("okaykey")]
        [DefaultValue(InputKey.Tab)]
        [JsonConverter(typeof(StringEnumConverter))]
        public InputKey OkayKey { get; set; }
    }
}