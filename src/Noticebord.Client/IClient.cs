using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Noticebord.Client.Models;

namespace Noticebord.Client
{
    public interface IClient
    {
        bool IsLoggedIn { get; }
        string BaseUrl { get; }
        string? Token { get; }

        Task<string> AuthenticateAsync(AuthenticateRequest request, CancellationToken cancellationToken = default);
        Task<Notice> CreateNoticeAsync(SaveNoticeRequest request, CancellationToken cancellationToken = default);
        Task<Notice> CreateTeamNoticeAsync(long team, SaveTeamNoticeRequest request, CancellationToken cancellationToken = default);
        Task DeleteNoticeAsync(long id, CancellationToken cancellationToken = default);
        Task DeleteTeamNoticeAsync(long team, long id, CancellationToken cancellationToken = default);
        Task<Notice> GetNoticeAsync(long id, CancellationToken cancellationToken = default);
        Task<List<Notice>> GetNoticesAsync(CancellationToken cancellationToken = default);
        Task<Notice> GetTeamNoticeAsync(long team, long id, CancellationToken cancellationToken = default);
        Task<List<Notice>> GetTeamNoticesAsync(long team, CancellationToken cancellationToken = default);
        Task<Notice> UpdateNoticeAsync(long id, SaveNoticeRequest request, CancellationToken cancellationToken = default);
        Task<Notice> UpdateTeamNoticeAsync(long team, long id, SaveTeamNoticeRequest request, CancellationToken cancellationToken = default);
    }
}