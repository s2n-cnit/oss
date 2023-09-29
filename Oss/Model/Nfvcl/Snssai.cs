
using System.Text.Json.Serialization;

namespace Oss.Model.Nfvcl
{
    public class Snssai
    {
       	[JsonPropertyName("sliceId")]
		public string SliceId { get; set; }

       	[JsonPropertyName("sliceType")]
		public string SliceType { get; set; }

       	[JsonPropertyName("pduSessionIds")]
		public string[] PduSessionIds { get; set; }

       	[JsonPropertyName("default_slice")]
		public bool DefaultSlice { get; set; }
    }
}
