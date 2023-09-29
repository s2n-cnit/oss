
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc;

using MediatR;

using Oss.Services;
using Oss.Model.Response;
using Oss.Model.Nfvcl;
using Oss.Requests;

namespace Oss.Controllers
{
    [ApiController]
    [ApiVersion("0.3")]
    public class CallbackController: Controller
    {
        private readonly IMediator mediator_;
        private readonly ILogger logger_;

        private readonly DatabaseService db_;
        private readonly NfvclClientService nfvclClient_;
        private readonly RestClient restClient_;

        public CallbackController(IMediator mediator, ILogger<CallbackController> logger, DatabaseService db, NfvclClientService nfvclClient, RestClient restClient)
        {
            mediator_ = mediator;
            logger_ = logger;
            db_ = db;
            nfvclClient_ = nfvclClient;
            restClient_ = restClient;
        }

        [HttpPost]
        [Route("~/callback/slice/create/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateSlice(string id, [FromBody] Response reply)
        {
            var vasInfo = db_.GetVasInfo(id);

            if (vasInfo is null)
            {
                return NotFound();
            }

            logger_.LogInformation("**$** Received NFVCL response for Slice Intent {id} at {date} **$**", id, DateTime.UtcNow);

            if (reply.Status.ToLower() == "failed")
            {
                vasInfo.VasStatus.Status = Status.FAILED;
                vasInfo.VasStatus.Message = reply.Message;

                await Notify(vasInfo);
            }
            else if (reply.Status.ToLower() == "ready")
            {
                var result = await mediator_.Send(new CreateNamespaceRequest
                {
                    Id = id,
                    Response = reply
                });

                logger_.LogInformation("**$** Namespace created for Slice Intent {id} at {date} **$**", id, DateTime.UtcNow);

                // TODO: Use mediatR
                result.IfSucc(async (vasInfo) =>
                {
                    SaveSliceFile(vasInfo);
                    await Notify(vasInfo);
                });
            }

            return Ok();
        }

        [HttpPost]
        [Route("~/callback/slice/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSlice(string id, [FromBody] Response reply)
        {
            var vasInfo = db_.GetVasInfo(id);

            if (vasInfo is null)
            {
                return NotFound();
            }

            if (reply.Status.ToLower() == "failed")
            {
                vasInfo.VasStatus.Status = Status.FAILED;
            }
            else if (reply.Status.ToLower() == "ready")
            {
                vasInfo.VasStatus.Status = Status.TERMINATED;
            }

            _ = db_.DeleteVasInfo(vasInfo.Id);

            var sliceProfile = vasInfo.VasConfiguration.SliceProfiles.First();
            var geographicalAreaId = sliceProfile.LocationConstraints.First().GeographicalAreaId;
            var area = db_.GetGeographicArea(geographicalAreaId);
            var blueprint = db_.GetBlueprint(geographicalAreaId);
            var clusterId = blueprint.K8sClusterId;

            var namespaceName = vasInfo.VaQuotaInfo[0].Clusters[0].ClusterInfo.Namespace;
            var nfvclUrl = string.Format(area.NfvclUrlTemplateForK8s, clusterId, namespaceName);

            reply = await nfvclClient_.DeleteNamespace(nfvclUrl);

            logger_.LogInformation("DeleteNamespace: Sent request to NFVCL, result: {sc}", reply.Status);

            if (reply.Status.ToLower() == "failed")
            {
                vasInfo.VasStatus.Status = Status.FAILED;
                vasInfo.VasStatus.Message = reply.Message;
            }

            // TODO:
            /*Task.Run(async () =>
            {
                await Notify(vasInfo);
            });*/

            return NoContent();
        }

        [NonAction]
        private void SaveSliceFile(VasInfo vasInfo)
        {
            var intentFileName = $"{vasInfo.VasConfiguration.Name}-{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}";
            var content = Serialize(vasInfo);

            System.IO.File.WriteAllText($"/intents/callback-{intentFileName}.json", content);
        }

        [NonAction]
        private async Task Notify(VasInfo vasInfo)
        {
            var callbackUrl = vasInfo.VasConfiguration.CallbackUrl;

            if (string.IsNullOrWhiteSpace(callbackUrl))
            {
                return;
            }

            _ = await restClient_.Post(new Uri(callbackUrl), RestClient.CreateJsonContent(Serialize(vasInfo)));
        }

        [NonAction]
        private string Serialize<TObject>(TObject obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true });
        }
    }
}
