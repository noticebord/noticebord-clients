using Newtonsoft.Json;

namespace Noticebord.Client.Models
{
    public record SaveTeamNoticeRequest(
        [property: JsonProperty("title")] string Title,
        [property: JsonProperty("body")] string Body);
}