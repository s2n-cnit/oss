
using LanguageExt.Common;

using Oss.Requests;
using Oss.Model.Response;
using Oss.Services;

namespace Oss.Handlers
{
    public class DeleteSliceRequestHandler: AbstractHandler<DeleteSliceRequest, Result<string>>
    {
        private const string callbackUrlDelete_ = "http://oss.maas/callback/slice/delete/{0}";

        private readonly DatabaseService db_;
        private readonly NfvclClientService nfvclClient_;

        public DeleteSliceRequestHandler(DatabaseService db, NfvclClientService nfvclClient, ILogger<DeleteSliceRequestHandler> logger, RestClient restClient, IBackgroundTaskQueue taskQueue) : base(logger, restClient, taskQueue)
        {
            db_ = db;
            nfvclClient_ = nfvclClient;
        }

        public override Task<Result<string>> Handle(DeleteSliceRequest request, CancellationToken cancellationToken)
        {
            var vasInfo = db_.GetVasInfoFromSliceId(request.SliceId);

            if (vasInfo is null)
            {
                return Task.FromResult(new Result<string>(new Exception("Slice not found.")));
            }

            vasInfo.VasStatus.Status = Status.TERMINATING;

            _ = db_.ReplaceVasInfo(vasInfo);

            RunBackground(Task.Run(async delegate ()
            {
                try
                {
                    var sliceProfile = vasInfo.VasConfiguration.SliceProfiles[0];
                    var locationConstraint = sliceProfile.LocationConstraints[0];
                    var geographicalAreaId = locationConstraint.GeographicalAreaId;
                    var geographicalArea = db_.GetGeographicArea(geographicalAreaId);
                    var blueprint = db_.GetBlueprint(geographicalAreaId);
                    var blueId = blueprint.Free5GcBlueId;

                    if (geographicalArea.Labels is not null)
                    {
                        locationConstraint.Extra = new();

                        foreach (var label in geographicalArea.Labels)
                        {
                            locationConstraint.Extra.Add(label.Key, label.Value);
                        }
                    }

                    var nfvclUrl = string.Format(geographicalArea.NfvclUrlTemplateFor5G, blueId, "del_slice");
                    var callbackUrl = string.Format(callbackUrlDelete_, vasInfo.Id);
                    var reply = await nfvclClient_.DeleteSlice(nfvclUrl, vasInfo, callbackUrl);

                    logger_.LogInformation("DeleteSlice: Sent request to NFVCL, result: {sc}", reply.Status);

                    if (reply.Status.ToLower() == "failed")
                    {
                        logger_.LogError("DeleteSlice: {m}", reply.Message);

                        vasInfo.VasStatus.Status = Status.FAILED;
                        vasInfo.VasStatus.Message = reply.Message;

                        await Notify(vasInfo, vasInfo.VasConfiguration.CallbackUrl);
                    }
                }
                catch (Exception ex)
                {
                    logger_.LogError(ex, "DeleteInstance");
                    vasInfo.VasConfiguration.CallbackUrl = null;
                    vasInfo.VasStatus.Status = Status.FAILED;

                    await Notify(vasInfo, vasInfo.VasConfiguration.CallbackUrl);
                }
            }));

            logger_.LogInformation("DeleteInstance => {i}", vasInfo.VasConfiguration.Id);

            return Task.FromResult(new Result<string>(vasInfo.VasConfiguration.Id));
        }
    }
}
