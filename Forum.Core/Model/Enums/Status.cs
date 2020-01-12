using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Forum.Core.Model.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        APPROVED,
        WAITING_FOR_APPROVAL,
        REJECTED,
        REPORTED_AS_SPAM,
        BLOCKED,
        DELETED
    }
}
