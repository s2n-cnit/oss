
//using System.Text.Json.Serialization;
//using System.Text.Json;

//namespace Oss.Model
//{
//    internal class RestClient
//    {
//        private const string jsonContentType_ = "application/json";

//        private readonly string host_;
        
//        protected readonly ILogger logger_;

//        private readonly Rest.RestClient restClient_ = new(new());

//        public RestClient(string host, ILogger logger)
//        {
//            host_ = host;
//            logger_ = logger;
//        }

//        protected Uri GetUri(string path)
//        {
//            ArgumentNullException.ThrowIfNull(path);

//            var ub = new UriBuilder(host_);
//            ub.Path = path;

//            return ub.Uri;
//        }

//        protected Uri GetUriIdInQuery(string path, string id)
//        {
//            return GetUriWithQuery(path, "id", id);
//        }

//        protected Uri GetUriWithQuery(string path, string key, string value)
//        {
//            ArgumentNullException.ThrowIfNull(path);
//            ArgumentNullException.ThrowIfNull(key);
//            ArgumentNullException.ThrowIfNull(value);

//            var ub = new UriBuilder(host_);
//            ub.Path = path;

//            ub.Query = $"{key}={value}";

//            return ub.Uri;
//        }

//        protected Uri GetUriIdInPath(string path, string id)
//        {
//            ArgumentNullException.ThrowIfNull(path);
//            ArgumentNullException.ThrowIfNull(id);

//            var ub = new UriBuilder(host_);
//            ub.Path = string.Format(path, id);

//            return ub.Uri;
//        }

//        protected async Task<TObject> Get<TObject>(Uri uri)
//        {
//            return await restClient_.JsonFromGet<TObject>(uri);
//        }

//        protected async Task<HttpResponseMessage> Post<TObject>(Uri uri, TObject obj)
//        {
//            var content = Serialize(obj);

//            logger_.LogInformation(content);

//            return await restClient_.Post(uri, content, jsonContentType_);
//        }

//        protected async Task<TResult> Post<TResult, TObject>(Uri uri, TObject obj)
//        {
//            return await restClient_.JsonFromPost<TResult>(uri, Serialize(obj), jsonContentType_);
//        }

//        protected async Task<HttpResponseMessage> Put<TObject>(Uri uri, TObject obj)
//        {
//            logger_.LogInformation(Serialize(obj));

//            return await restClient_.Put(uri, Serialize(obj), jsonContentType_);
//        }

//        protected async Task<TResult> Put<TResult, TObject>(Uri uri, TObject obj)
//        {
//            logger_.LogInformation(Serialize(obj));

//            return await restClient_.JsonFromPut<TResult>(uri, Serialize(obj), jsonContentType_);
//        }

//        protected async Task<HttpResponseMessage> Delete(Uri uri)
//        {
//            return await restClient_.Delete(uri);
//        }

//        protected async Task<TResult> Delete<TResult>(Uri uri)
//        {
//            return await restClient_.Delete(uri)
//                .ContinueWith(restClient_.JsonFromResponse<TResult>).Unwrap();
//        }

//        protected string Serialize<TObject>(TObject obj)
//        {
//            var content = JsonSerializer.Serialize(obj, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

//            logger_.LogInformation(content);

//            return content;
//        }
//    }
//}
