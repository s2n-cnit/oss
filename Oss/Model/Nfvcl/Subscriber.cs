
using System.Text.Json.Serialization;

namespace Oss.Model.Nfvcl
{
    public class Subscriber
    {
       	[JsonPropertyName("imsi")]
		public string Imsi { get; set; }

       	[JsonPropertyName("k")]
		public string K { get; set; }

       	[JsonPropertyName("opc")]
		public string Opc { get; set; }

       	[JsonPropertyName("snssai")]
		public Snssai[] Snssai { get; set; }
    }
}
