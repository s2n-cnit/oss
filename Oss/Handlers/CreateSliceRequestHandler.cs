
using MongoDB.Bson;

using LanguageExt.Common;

using Oss.Requests;
using Oss.Services;
using Oss.Model.Response;

namespace Oss.Handlers
{
    public class CreateSliceRequestHandler: AbstractHandler<CreateSliceRequest, Result<string>>
    {
        private const string callbackUrlCreate_ = "http://oss.maas/callback/slice/create/{0}";

        private readonly DatabaseService db_;
        private readonly NfvclClientService nfvclClient_;

        public CreateSliceRequestHandler(DatabaseService db, NfvclClientService nfvclClient, RestClient restClient, IBackgroundTaskQueue taskQueue, ILogger<CreateSliceRequestHandler> logger): base(logger, restClient, taskQueue)
        {
            db_ = db;
            nfvclClient_ = nfvclClient;
        }

        public override Task<Result<string>> Handle(CreateSliceRequest request, CancellationToken cancellationToken)
        {
            var intent = request.Intent;
            var sliceProfile = intent.SliceProfiles[0];
            var locationConstraint = sliceProfile.LocationConstraints[0];
            var geographicalAreaId = locationConstraint.GeographicalAreaId;
            var geographicalArea = db_.GetGeographicArea(geographicalAreaId);

            var blueprint = db_.GetBlueprint(geographicalAreaId);
            var blueId = blueprint.Free5GcBlueId;

            if (string.IsNullOrWhiteSpace(blueId) || string.IsNullOrWhiteSpace(blueprint.K8sClusterId))
            {
                return Task.FromResult(new Result<string>(new Exception("Request not compatible with setup or setup not completed")));
            }

            if (string.IsNullOrWhiteSpace(intent.Id))
            {
                intent.Id = Guid.NewGuid().ToString();
            }

            logger_.LogInformation("**$** Received Slice Intent {id} at {date} **$**", intent.Id, DateTime.UtcNow);

            if (geographicalArea.Labels is not null)
            {
                locationConstraint.Extra = new();

                foreach (var label in geographicalArea.Labels)
                {
                    locationConstraint.Extra.Add(label.Key, label.Value);
                }
            }

            var vasInfo = new VasInfo
            {
                Id = ObjectId.GenerateNewId().ToString(),
                VasStatus = new VasStatus
                {
                    Vasi = intent.Id,
                    Status = Status.INSTANTIATING,
                },
                VasConfiguration = intent
            };

            db_.SaveVasInfo(vasInfo);

            RunBackground(Task.Run(async delegate ()
            {
                try
                {
                    var nfvclUrl = string.Format(geographicalArea.NfvclUrlTemplateFor5G, blueId, "add_slice");
                    var callbackUrl = string.Format(callbackUrlCreate_, vasInfo.Id);
                    var reply = await nfvclClient_.CreateSlice(nfvclUrl, vasInfo, callbackUrl);

                    logger_.LogInformation("CreateSlice: Sent request to NFVCL, result: {sc}", reply.Status);

                    if (reply.Status.ToLower() == "failed")
                    {
                        vasInfo.VasConfiguration.CallbackUrl = null;
                        vasInfo.VasStatus.Status = Status.FAILED;
                        vasInfo.VasStatus.Message = reply.Message;

                        logger_.LogError("CreateSlice: {m}", reply.Message);

                        await Notify(vasInfo, intent.CallbackUrl);
                    }
                }
                catch (Exception ex)
                {
                    logger_.LogError(ex, "CreateSlice");

                    vasInfo.VasConfiguration.CallbackUrl = null;
                    vasInfo.VasStatus.Status = Status.FAILED;
                    vasInfo.VasStatus.Message = ex.Message;

                    await Notify(vasInfo, intent.CallbackUrl);
                }
            }));

            logger_.LogInformation("CreateSlice => {i}", intent.Id);

            return Task.FromResult(new Result<string>(intent.Id));
        }
    }
}
