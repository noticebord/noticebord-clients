using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Noticebord.Client.Models;

namespace Noticebord.Client
{
    public interface IClient
    {
        public Task<string> AuthorizeAsync(string email, string password, string deviceName, CancellationToken cancellationToken = default);
        public Task<Notice> CreateNoticeAsync(string title, string text, string token, CancellationToken cancellationToken = default);
        public Task<Notice> GetNoticeAsync(long id, CancellationToken cancellationToken = default);
        public Task<List<Notice>> GetNoticesAsync(CancellationToken cancellationToken = default);
    }
}