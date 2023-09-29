
using System.Text.Json.Serialization;

namespace Oss.Model.Request
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum IsolationLevel
    {
        NO_ISOLATION,
        ISOLATION,
    }
}
