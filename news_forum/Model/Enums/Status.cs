﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace news_forum.Model.Enums
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