
using System.Text.Json.Serialization;

namespace Oss.Model.Nfvcl
{
    public class NetworkEndpoints
    {
       	[JsonPropertyName("mgt")]
		public string Mgt { get; set; }

       	[JsonPropertyName("wan")]
		public string Wan { get; set; }

       	[JsonPropertyName("data_nets")]
		public DataNet[] DataNets { get; set; }
    }
}
