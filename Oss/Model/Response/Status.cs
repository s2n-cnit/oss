
using System.Text.Json.Serialization;

namespace Oss.Model.Response
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        INSTANTIATING,
        INSTANTIATED,
        FAILED,
        TERMINATING,
        TERMINATED
    }
}