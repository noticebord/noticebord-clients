using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Noticebord.Client.Models;

namespace Noticebord.Client
{
    public interface IClient
    {
        public bool IsLoggedIn { get; }
        public string? Token { get; }

        public Task<string> AuthenticateAsync(AuthenticateRequest request, CancellationToken cancellationToken = default);
        public Task<Notice> CreateNoticeAsync(SaveNoticeRequest request, CancellationToken cancellationToken = default);
        public Task<Notice> GetNoticeAsync(long id, CancellationToken cancellationToken = default);
        public Task<List<Notice>> GetNoticesAsync(CancellationToken cancellationToken = default);
        public Task<Notice> UpdateNoticeAsync(long id, SaveNoticeRequest request, CancellationToken cancellationToken = default);
        public Task<Notice> DeleteNoticeAsync(long id, CancellationToken cancellationToken = default);
    }
}