using System;
using Newtonsoft.Json;

namespace Noticebord.Client.Models
{
    public record User(
        [property: JsonProperty("id")] long Id,
        [property: JsonProperty("name")] string Name,
        [property: JsonProperty("email")] string Email,
        [property: JsonProperty("profile_photo_url")] string ProfilePhotoUrl);
}