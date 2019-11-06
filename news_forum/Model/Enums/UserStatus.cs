using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace news_forum.Model.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserStatus
    {
        EMAIL_NOT_VERIFIED,
        EMAIL_VERIFICATION_TO_BE_SENT,
        EMAIL_VERIFICATION_RESENT,
        VERIFIED,
        BLOCKED
    }
}
