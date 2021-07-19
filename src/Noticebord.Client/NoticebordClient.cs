using System;
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

        public async Task<string> AuthorizeAsync(
            string email,
            string password,
            string deviceName,
            CancellationToken cancellationToken = default) =>
            await _baseurl.AppendPathSegment("tokens")
                .PostJsonAsync(new
                {
                    email,
                    password,
                    device_name = deviceName
                }, cancellationToken)
                .ReceiveString();

        public async Task<Notice> CreateNoticeAsync(
            string title,
            string text,
            string token,
            CancellationToken cancellationToken = default) =>
            await _baseurl.AppendPathSegment("notices")
                .WithHeader("Authorization", $"Bearer {token}")
                .PostJsonAsync(new { title, text }, cancellationToken)
                .ReceiveJson<Notice>();

        public async Task<Notice> GetNoticeAsync(long id, CancellationToken cancellationToken = default) =>
            await _baseurl.AppendPathSegment("notices").AppendPathSegment(id).GetJsonAsync<Notice>(cancellationToken);

        public async Task<List<Notice>> GetNoticesAsync(CancellationToken cancellationToken = default) =>
            await _baseurl.AppendPathSegment("notices").GetJsonAsync<List<Notice>>(cancellationToken);
    }
}