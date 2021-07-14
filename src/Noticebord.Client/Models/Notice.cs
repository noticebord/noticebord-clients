namespace Noticebord.Client.Models
{
    public class Notice : Model
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public User Author { get; set; }
    }
}