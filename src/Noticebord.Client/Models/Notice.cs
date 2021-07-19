using System;

namespace Noticebord.Client.Models
{
    public record Notice(
        long Id,
        string Title,
        string Text,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        User Author
    );
}