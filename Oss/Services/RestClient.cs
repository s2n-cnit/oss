
using System.Text.Json;
using System.Text;

using Oss.Code;

namespace Oss.Services
{
    public class RestClient : HttpClientWrapper
    {
        /// <summary>
        /// The MIME content-type for JSON.
        /// </summary>
        public const string JsonContentType = "application/json";

        /// <summary>
        /// If true the client will send a request without the encoding in the content-type header.
        /// </summary>
        public bool NoEncodingInContentType { get; set; } = false;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="client">The possibly shared HttpClient.</param>
        public RestClient(HttpClient client) : base(client) { }

        /// <summary>
        /// Creates a StringContent from a JSON string.
        /// </summary>
        /// <param name="jsonContent">The string representing the JSON content to send in the request.</param>
        /// <param name="noEncodingInContentType">If true the client will send a request without the encoding in the content-type header.</param>
        /// <returns>The StringContent.</returns>
        public static StringContent CreateJsonContent(string jsonContent, bool noEncodingInContentType = false)
        {
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, JsonContentType);

            if (noEncodingInContentType)
            {
                stringContent.Headers.ContentType.CharSet = "";
            }

            return stringContent;
        }

        /// <summary>
        /// Gets the string content from an HttpResponseMessage.
        /// </summary>
        /// <param name="response">The HttpResponseMessage to get the string content from.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the string content upon completion.</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">The response doesn't indicate a successful result.</exception>
        public static async Task<string> StringContentFromResponse(HttpResponseMessage response)
        {
            ArgumentNullException.ThrowIfNull(response);

            var message = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                if (string.IsNullOrWhiteSpace(message))
                {
                    message = response.ReasonPhrase;
                }

                throw new HttpRequestException($"{response.StatusCode}: {message}");
            }

            return message;
        }

        /// <summary>
        /// Deserialize an object from the JSON content of an HttpResponseMessage.
        /// </summary>
        /// <typeparam name="TResult">The type of the object to retrieve.</typeparam>
        /// <param name="response">The HttpResponseMessage to get the object from.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the deserialized object upon completion.</returns>
        public static async Task<TResult> JsonFromResponse<TResult>(HttpResponseMessage response)
        {
            ArgumentNullException.ThrowIfNull(response);

            var stringContent = await StringContentFromResponse(response);

            return ParseJson<TResult>(stringContent);
        }

        /// <summary>
        /// Retrieves an object from an Uri via HTTP GET method.
        /// </summary>
        /// <typeparam name="TResult">The type of the object to retrieve.</typeparam>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the requested object upon completion.</returns>
        public async Task<TResult> Get<TResult>(Uri uri, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            var response = await Get(uri, headers);

            return await JsonFromResponse<TResult>(response);
        }

        /// <summary>
        /// Retrieves an object from an Uri via HTTP GET method.
        /// </summary>
        /// <typeparam name="TResult">The type of the object to retrieve.</typeparam>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="cancellationToken">The CancellationToken to cancel the operation.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the requested object upon completion.</returns>
        public async Task<TResult> Get<TResult>(Uri uri, CancellationToken cancellationToken, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            var response = await Get(uri, cancellationToken, headers);

            return await JsonFromResponse<TResult>(response);
        }

        /// <summary>
        /// Sends the JSON content via HTTP POST method.
        /// </summary>
        /// <typeparam name="TResult">The type of the object received in the response.</typeparam>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="content">The string representing the JSON content to send in the request.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the received object upon completion.</returns>
        public async Task<TResult> Post<TResult>(Uri uri, string content, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            var response = await Post(uri, CreateJsonContent(content, NoEncodingInContentType), headers);

            return await JsonFromResponse<TResult>(response);
        }

        /// <summary>
        /// Sends the JSON content via HTTP POST method.
        /// </summary>
        /// <typeparam name="TResult">The type of the object received in the response.</typeparam>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="content">The string representing the JSON content to send in the request.</param>
        /// <param name="cancellationToken">The CancellationToken to cancel the operation.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the received object upon completion.</returns>
        public async Task<TResult> Post<TResult>(Uri uri, string content, CancellationToken cancellationToken, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            var response = await Post(uri, CreateJsonContent(content, NoEncodingInContentType), cancellationToken, headers);

            return await JsonFromResponse<TResult>(response);
        }

        /// <summary>
        /// Sends the JSON content via HTTP PUT method.
        /// </summary>
        /// <typeparam name="TResult">The type of the object received in the response.</typeparam>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="content">The string representing the JSON content to send in the request.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the received object upon completion.</returns>
        public async Task<TResult> Put<TResult>(Uri uri, string content, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            var response = await Put(uri, CreateJsonContent(content, NoEncodingInContentType), headers);

            return await JsonFromResponse<TResult>(response);
        }

        /// <summary>
        /// Sends the JSON content via HTTP PATCH method.
        /// </summary>
        /// <typeparam name="TResult">The type of the object received in the response.</typeparam>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="content">The string representing the JSON content to send in the request.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the received object upon completion.</returns>
        public async Task<TResult> Patch<TResult>(Uri uri, string content, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            var response = await Patch(uri, CreateJsonContent(content, NoEncodingInContentType), headers);

            return await JsonFromResponse<TResult>(response);
        }

        /// <summary>
        /// Sends an HTTP DELETE request.
        /// </summary>
        /// <typeparam name="TResult">The type of the object received in the response.</typeparam>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="content">The string representing the optional JSON content to send in the request.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the received object upon completion.</returns>
        public async Task<TResult> Delete<TResult>(Uri uri, string content, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            var response = await Delete(uri, CreateJsonContent(content, NoEncodingInContentType), headers);

            return await JsonFromResponse<TResult>(response);
        }

        /// <summary>
        /// Sends an HTTP DELETE request.
        /// </summary>
        /// <typeparam name="TResult">The type of the object received in the response.</typeparam>
        /// <param name="uri">The Uri the request is sent to.</param>
        /// <param name="headers">An optional set of headers to add to the request.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> returning the received object upon completion.</returns>
        public async Task<TResult> Delete<TResult>(Uri uri, Dictionary<string, string> headers = null)
        {
            ArgumentNullException.ThrowIfNull(uri);

            var response = await Delete(uri, null, headers);

            return await JsonFromResponse<TResult>(response);
        }

        private static TResult ParseJson<TResult>(string content)
        {
            ArgumentNullException.ThrowIfNull(content);

            return JsonSerializer.Deserialize<TResult>(content);
        }
    }
}
