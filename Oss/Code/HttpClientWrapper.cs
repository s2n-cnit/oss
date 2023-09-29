
namespace Oss.Code
{
    public class HttpClientWrapper
    {
        private readonly HttpClient client_;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="client">The possibly shared HttpClient.</param>
        public HttpClientWrapper(HttpClient client)
        {
            ArgumentNullException.ThrowIfNull(client);

            client_ = client;
        }

        /// <summary>
        /// HTTP GET method.
        /// </summary>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the HttpResponseMessage upon completion.</returns>
        public async Task<HttpResponseMessage> Get(Uri uri, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            return await client_.SendAsync(CreateRequest(uri, HttpMethod.Get, null, headers));
        }

        /// <summary>
        /// A non standard HTTP GET method with body content.
        /// </summary>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="content">The <see cref="HttpContent"/> to send.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the HttpResponseMessage upon completion.</returns>
        public async Task<HttpResponseMessage> Get(Uri uri, HttpContent content, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            return await client_.SendAsync(CreateRequest(uri, HttpMethod.Get, content, headers));
        }

        /// <summary>
        /// HTTP GET method.
        /// </summary>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="cancellationToken">The CancellationToken to cancel the operation.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the HttpResponseMessage upon completion.</returns>
        public async Task<HttpResponseMessage> Get(Uri uri, CancellationToken cancellationToken, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            return await client_.SendAsync(CreateRequest(uri, HttpMethod.Get, null, headers), cancellationToken);
        }

        /// <summary>
        /// HTTP POST method.
        /// </summary>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="content">The <see cref="HttpContent"/> to send.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the HttpResponseMessage upon completion.</returns>
        public async Task<HttpResponseMessage> Post(Uri uri, HttpContent content, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            return await client_.SendAsync(CreateRequest(uri, HttpMethod.Post, content, headers));
        }

        /// <summary>
        /// HTTP POST method.
        /// </summary>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="content">The <see cref="HttpContent"/> to send.</param>
        /// <param name="cancellationToken">The CancellationToken to cancel the operation.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the HttpResponseMessage upon completion.</returns>
        public async Task<HttpResponseMessage> Post(Uri uri, HttpContent content, CancellationToken cancellationToken, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            return await client_.SendAsync(CreateRequest(uri, HttpMethod.Post, content, headers), cancellationToken);
        }

        /// <summary>
        /// HTTP PUT method.
        /// </summary>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="content">The <see cref="HttpContent"/> to send.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the HttpResponseMessage upon completion.</returns>
        public async Task<HttpResponseMessage> Put(Uri uri, HttpContent content, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            return await client_.SendAsync(CreateRequest(uri, HttpMethod.Put, content, headers));
        }

        /// <summary>
        /// HTTP PATCH method.
        /// </summary>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="content">The <see cref="HttpContent"/> to send.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the HttpResponseMessage upon completion.</returns>
        public async Task<HttpResponseMessage> Patch(Uri uri, HttpContent content, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            return await client_.SendAsync(CreateRequest(uri, HttpMethod.Patch, content, headers));
        }

        /// <summary>
        /// HTTP DELETE method.
        /// </summary>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="content">The optional <see cref="HttpContent"/> to send.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the HttpResponseMessage upon completion.</returns>
        public async Task<HttpResponseMessage> Delete(Uri uri, HttpContent content = null, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            return await client_.SendAsync(CreateRequest(uri, HttpMethod.Delete, content, headers));
        }

        private HttpRequestMessage CreateRequest(Uri uri, HttpMethod method, HttpContent content, Dictionary<string, string> headers)
        {
            ArgumentNullException.ThrowIfNull(uri);
            ArgumentNullException.ThrowIfNull(method);

            var request = new HttpRequestMessage()
            {
                RequestUri = uri,
                Method = method,
                Content = content
            };

            if (headers is not null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            return request;
        }
    }
}
