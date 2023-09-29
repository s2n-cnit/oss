
using System.Text.Json.Serialization;

namespace Oss.Model.Nfvcl
{
    public class DataNet
    {
       	[JsonPropertyName("net_name")]
		public string NetName { get; set; }

       	[JsonPropertyName("dnn")]
		public string Dnn { get; set; }

       	[JsonPropertyName("dns")]
		public string Dns { get; set; }

       	[JsonPropertyName("pools")]
		public Pool[] Pools { get; set; }

       	[JsonPropertyName("uplinkAmbr")]
		public string UplinkAmbr { get; set; }

       	[JsonPropertyName("downlinkAmbr")]
		public string DownlinkAmbr { get; set; }

       	[JsonPropertyName("default5qi")]
		public string Default5qi { get; set; }
    }
}
