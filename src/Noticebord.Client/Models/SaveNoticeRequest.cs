using Newtonsoft.Json;

namespace Noticebord.Client.Models
{
    public record SaveNoticeRequest(
        [property: JsonProperty("title")] string Title,
        [property: JsonProperty("body")] string Body,
        [property: JsonProperty("anonymous")] bool Anonymous,
        [property: JsonProperty("public")] bool Public);
}