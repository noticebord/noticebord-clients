using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Noticebord.Client.Models;

namespace Noticebord.Client
{
    public interface IClient
    {
        public Task<List<Notice>> GetNoticesAsync(CancellationToken cancellationToken = default);

        public Task<Notice> GetNoticeAsync(long id, CancellationToken cancellationToken = default);
    }
}