
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Oss.Model.Nfvcl
{
    public class Response
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("detail")]
        public string Message { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> Extra { get; set; }
    }
}
