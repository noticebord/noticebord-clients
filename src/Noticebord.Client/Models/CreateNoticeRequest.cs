namespace Noticebord.Client.Models
{
    public record CreateNoticeRequest(string Title, string Body, bool Anonymous);
}