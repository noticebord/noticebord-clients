using System;
using Newtonsoft.Json;

namespace Noticebord.Client.Models
{
    public record Notice(
        [property: JsonProperty("id")] long Id,
        [property: JsonProperty("title")] string Title,
        [property: JsonProperty("body")] string Body,
        [property: JsonProperty("created_at")] DateTime CreatedAt,
        [property: JsonProperty("updated_at")] DateTime UpdatedAt,
        [property: JsonProperty("author")] User Author);
}