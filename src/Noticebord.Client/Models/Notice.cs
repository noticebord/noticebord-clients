using System;

namespace Noticebord.Client.Models
{
    public record Notice(
        long Id,
        string Title,
        string Body,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        User Author
    );
}