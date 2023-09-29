
using Oss.Model.Request;
using System.Text.Json.Serialization;

namespace Oss.Model.Nfvcl
{
    public class Request
    {
       	[JsonPropertyName("type")]
		public string Type { get; set; }

       	[JsonPropertyName("callbackURL")]
		public string CallbackUrl { get; set; }

       	[JsonPropertyName("config")]
		public Config Config { get; set; }

       	[JsonPropertyName("areas")]
		public Area[] Areas { get; set; }
    }
}
