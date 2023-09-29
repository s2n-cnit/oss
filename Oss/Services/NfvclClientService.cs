
using System.Text.Json.Serialization;
using System.Text.Json;

using Oss.Model.Response;
using Oss.Model.Nfvcl;

namespace Oss.Services
{
    public class NfvclClientService
    {
        private readonly RestClient restClient_;

        public NfvclClientService(RestClient restClient)
        {
            ArgumentNullException.ThrowIfNull(restClient);

            restClient_ = restClient;
        }

        public async Task<Response> CreateSlice(string addSliceUrl, VasInfo vasInfo, string callbackUrl)
        {
            ArgumentNullException.ThrowIfNull(vasInfo);
            ArgumentException.ThrowIfNullOrEmpty(addSliceUrl);

            var request = CreateRequest(vasInfo, callbackUrl);

            return await restClient_.Post<Response>(new Uri(addSliceUrl), Serialize(request));
        }

        public async Task<Response> DeleteSlice(string delSliceUrl, VasInfo vasInfo, string callbackUrl)
        {
            ArgumentNullException.ThrowIfNull(vasInfo);
            ArgumentException.ThrowIfNullOrEmpty(delSliceUrl);

            var request = CreateRequest(vasInfo, callbackUrl);

            return await restClient_.Delete<Response>(new Uri(delSliceUrl), Serialize(request));
        }

        public async Task<Response> CreateNamespace(string addNamespaceUrl, Dictionary<string, string> labels)
        {
            ArgumentException.ThrowIfNullOrEmpty(addNamespaceUrl);

            return await restClient_.Put<Response>(new Uri(addNamespaceUrl), Serialize(labels));
        }

        public async Task<Response> DeleteNamespace(string delNamespaceUrl)
        {
            ArgumentException.ThrowIfNullOrEmpty(delNamespaceUrl);

            return await restClient_.Delete<Response>(new Uri(delNamespaceUrl));
        }

        private Request CreateRequest(VasInfo vasInfo, string callbackUrl)
        {
            var default5qi = vasInfo.VasConfiguration.SliceProfiles.FirstOrDefault()?.ProfileParams?.PduSessions?.FirstOrDefault()?.Flows?.FirstOrDefault()?.FlowQos;

            return new Request
            {
                CallbackUrl = callbackUrl,
                Config = new()
                {
                    SliceProfiles = vasInfo.VasConfiguration.SliceProfiles,
                    NetworkEndpoints = new()
                    {
                        DataNets = new DataNet[]
                        {
                            new()
                            {
                                Default5qi = default5qi,
                            }
                        }
                    },
                }
            };
        }

        private string Serialize<TObject>(TObject obj)
        {
            ArgumentNullException.ThrowIfNull(obj);

            return JsonSerializer.Serialize(obj, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true });
        }
    }
}
