
using System.Text.Json.Serialization;

namespace Oss.Model.Nfvcl
{
    public class Slice
    {
       	[JsonPropertyName("sliceType")]
		public string SliceType { get; set; }

       	[JsonPropertyName("sliceId")]
		public string SliceId { get; set; }
    }
}
