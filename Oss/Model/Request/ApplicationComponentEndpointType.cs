
using System.Text.Json.Serialization;

namespace Oss.Model.Request
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ApplicationComponentEndpointType
    {
        CORE,
        ACCESS
    }
}
