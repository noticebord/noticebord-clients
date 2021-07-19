namespace Noticebord.Client.Models
{
    public record AuthenticateRequest(string Email, string Password, string DeviceName);
}