using Newtonsoft.Json;

namespace Noticebord.Client.Models
{
    public record AuthenticateRequest(
        [property: JsonProperty("email")] string Email, 
        [property: JsonProperty("password")] string Password, 
        [property: JsonProperty("device_name")] string DeviceName);
}