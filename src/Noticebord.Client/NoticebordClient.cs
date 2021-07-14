using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Noticebord.Client.Models;

namespace Noticebord.Client
{
    public class NoticebordClient : IClient
    {
        private const string _baseurl = "https://noticebord.herokuapp.com/api";

        public async Task<List<Notice>> GetNoticesAsync(CancellationToken cancellationToken = default) =>
            await _baseurl.AppendPathSegment("notices").GetJsonAsync<List<Notice>>();

        public async Task<Notice> GetNoticeAsync(long id, CancellationToken cancellationToken = default) =>
            await _baseurl.AppendPathSegment("notices").AppendPathSegment(id).GetJsonAsync<Notice>();
    }
}