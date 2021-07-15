using System;

namespace Noticebord.Client.Models
{
    public class Notice : Model
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public User Author { get; set; }

        public override string ToString() =>  $"{Id}|{Title}|{CreatedAt}|{UpdatedAt}|{Author.Id}";
    }
}