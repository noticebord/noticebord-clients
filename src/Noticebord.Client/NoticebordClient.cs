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
        private readonly string _baseurl = "https://noticebord.herokuapp.com/api";

        public bool IsAuthenticated => Token is not null;
        public string? Token { get; }

        public NoticebordClient(string? token, string? baseurl = default)
        {
            _baseurl = baseurl ?? _baseurl;
            Token = token;
        }

        public async Task<string> AuthenticateAsync(
            AuthenticateRequest request,
            CancellationToken cancellationToken = default) =>
                await _baseurl.AppendPathSegment("tokens")
                    .PostJsonAsync(new
                    {
                        email = request.Email,
                        password = request.Password,
                        device_name = request.DeviceName
                    }, cancellationToken)
                    .ReceiveString();

        public async Task<Notice> CreateNoticeAsync(
            CreateNoticeRequest request,
            CancellationToken cancellationToken = default) =>
                await _baseurl.AppendPathSegment("notices")
                    .WithHeader("Authorization", Token is null ? string.Empty : $"Bearer {Token}")
                    .PostJsonAsync(new { title = request.Title, text = request.Text }, cancellationToken)
                    .ReceiveJson<Notice>();

        public async Task<Notice> GetNoticeAsync(long id, CancellationToken cancellationToken = default) =>
            await _baseurl.AppendPathSegment("notices")
                .AppendPathSegment(id)
                .GetJsonAsync<Notice>(cancellationToken);

        public async Task<List<Notice>> GetNoticesAsync(CancellationToken cancellationToken = default) =>
            await _baseurl.AppendPathSegment("notices")
                .GetJsonAsync<List<Notice>>(cancellationToken);
    }
}