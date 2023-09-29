
using System.Text.Json.Serialization;

namespace Oss.Model.Nfvcl
{
    public class Area
    {
       	[JsonPropertyName("id")]
		public int Id { get; set; }

       	[JsonPropertyName("nci")]
		public string Nci { get; set; }

       	[JsonPropertyName("idLength")]
		public int IdLength { get; set; }

       	[JsonPropertyName("core")]
		public bool Core { get; set; }

       	[JsonPropertyName("slices")]
		public Slice[] Slices { get; set; }
    }
}
