
using System.Text.Json.Serialization;

namespace Oss.Model.Nfvcl
{
    public class Pool
    {
       	[JsonPropertyName("cidr")]
		public string Cidr { get; set; }
    }
}
