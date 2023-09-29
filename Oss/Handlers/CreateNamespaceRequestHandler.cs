
using System.Text.Json;

using LanguageExt.Common;

using Oss.Requests;
using Oss.Model.Response;
using Oss.Services;

namespace Oss.Handlers
{
    public class CreateNamespaceRequestHandler: AbstractHandler<CreateNamespaceRequest, Result<VasInfo>>
    {
        private readonly DatabaseService db_;
        private readonly NfvclClientService nfvclClient_;

        public CreateNamespaceRequestHandler(DatabaseService db, NfvclClientService nfvclClient, ILogger<CreateNamespaceRequestHandler> logger, RestClient restClient, IBackgroundTaskQueue taskQueue) : base(logger, restClient, taskQueue)
        {
            db_ = db;
            nfvclClient_ = nfvclClient;
        }

        public override async Task<Result<VasInfo>> Handle(CreateNamespaceRequest request, CancellationToken cancellationToken)
        {
            var vasInfo = db_.GetVasInfo(request.Id);

            if (vasInfo is null)
            {
                return new Result<VasInfo>(new Exception("Not found"));
            }

            var reply = request.Response;

            if (reply.Status.ToLower() == "failed")
            {
                vasInfo.VasStatus.Status = Status.FAILED;
            }
            else if (reply.Status.ToLower() == "ready")
            {
                vasInfo.VasStatus.Status = Status.INSTANTIATED;
                vasInfo.VaQuotaInfo = JsonSerializer.Deserialize<KubeConfig[]>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "kube-config.json")));
            }

            _ = db_.ReplaceVasInfo(vasInfo);

            if (vasInfo.VasStatus.Status == Status.FAILED)
            {
                return new Result<VasInfo>(new Exception("Error creating the Slice."));
            }

            var sliceProfile = vasInfo.VasConfiguration.SliceProfiles.First();
            var geographicalAreaId = sliceProfile.LocationConstraints.First().GeographicalAreaId;
            var area = db_.GetGeographicArea(geographicalAreaId);
            var blueprint = db_.GetBlueprint(geographicalAreaId);
            var clusterId = blueprint.K8sClusterId;
            var namespaceName = $"{sliceProfile.SliceType}{sliceProfile.SliceId}".ToLower();

            foreach (var config in vasInfo.VaQuotaInfo)
            {
                if (config.Clusters is not null)
                {
                    foreach (var cluster in config.Clusters)
                    {
                        if (cluster.ClusterInfo is not null)
                        {
                            cluster.ClusterInfo.Namespace = namespaceName;
                        }
                    }
                }

                if (config.Contexts is not null)
                {
                    foreach (var context in config.Contexts)
                    {
                        if (context.ContextInfo is not null)
                        {
                            context.ContextInfo.Namespace = namespaceName;
                        }
                    }
                }
            }

            _ = db_.ReplaceVasInfo(vasInfo);

            var nfvclUrl = string.Format(area.NfvclUrlTemplateForK8s, clusterId, namespaceName);
            reply = await nfvclClient_.CreateNamespace(nfvclUrl, new());

            logger_.LogInformation("CreateNamespace: Sent request to NFVCL, result: {sc}", reply.Status);

            if (reply.Status.ToLower() == "failed")
            {
                vasInfo.VasStatus.Status = Status.FAILED;
                vasInfo.VasStatus.Message = reply.Message;
            }

            return new Result<VasInfo>(vasInfo);
        }
    }
}
