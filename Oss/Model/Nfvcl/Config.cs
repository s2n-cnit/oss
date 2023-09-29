
using System.Text.Json.Serialization;

using Oss.Model.Request;

namespace Oss.Model.Nfvcl
{
    public class Config
    {
        [JsonPropertyName("sliceProfiles")]
        public SliceProfile[] SliceProfiles { get; set; }

        [JsonPropertyName("network_endpoints")]
		public NetworkEndpoints NetworkEndpoints { get; set; }

       	[JsonPropertyName("plmn")]
		public string Plmn { get; set; }

       	[JsonPropertyName("subscribers")]
		public Subscriber[] Subscribers { get; set; }
    }
}
