using Noticebord.Client.Models;

namespace Noticebord.Cli.Utils
{
    public class NoticeUtils
    {
        public static Notice AssignDefaultAuthor(Notice notice)
        {
            if (notice.Author is not null) return notice;

            return notice with { Author = new(0, "Anonymous", string.Empty, string.Empty) };
        }
    }
}