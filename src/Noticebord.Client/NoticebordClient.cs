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
        private const string defaultUrl = "https://noticebord.herokuapp.com/api";

        public bool IsLoggedIn => Token is not null;
        public string BaseUrl { get; }
        public string? Token { get; }

        public NoticebordClient(string? token, string? baseUrl = default)
        {
            Token = token;
            BaseUrl = baseUrl ?? defaultUrl;
        }

        public async Task<string> AuthenticateAsync(
            AuthenticateRequest request,
            CancellationToken cancellationToken = default) =>
            await BaseUrl.AppendPathSegment("tokens")
                .PostJsonAsync(request, cancellationToken)
                .ReceiveString();

        public async Task<Notice> CreateNoticeAsync(
            CreateNoticeRequest request,
            CancellationToken cancellationToken = default) =>
            await BaseUrl.AppendPathSegment("notices")
                .WithHeader("Authorization", IsLoggedIn ? string.Empty : $"Bearer {Token}")
                .PostJsonAsync(request, cancellationToken)
                .ReceiveJson<Notice>();

        public async Task<Notice> GetNoticeAsync(long id, CancellationToken cancellationToken = default) =>
            await BaseUrl.AppendPathSegment("notices")
                .AppendPathSegment(id)
                .GetJsonAsync<Notice>(cancellationToken);

        public async Task<List<Notice>> GetNoticesAsync(CancellationToken cancellationToken = default) =>
            await BaseUrl.AppendPathSegment("notices")
                .GetJsonAsync<List<Notice>>(cancellationToken);
    }
}