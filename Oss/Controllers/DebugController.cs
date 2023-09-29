
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc;

using MongoDB.Bson;

using Swashbuckle.AspNetCore.Filters;

using Oss.Code;
using Oss.Model;
using Oss.Model.Request;
using Oss.Model.Response;
using Oss.Services;

namespace Oss.Controllers
{
    [ApiController]
    [ApiVersion("0.3")]
    [Produces("application/json")]
    public class DebugController: Controller
    {
        private const string callbackUrl_ = "http://oss.maas/debug/slice/{0}?operation={1}";
        private const string jsonContentType_ = "application/json";

        private readonly DatabaseService db_;
        private readonly RestClient restClient_;
        private readonly IServiceScopeFactory scopeFactory_;
        private readonly IBackgroundTaskQueue taskQueue_;
        private readonly ILogger logger_;

        public DebugController(DatabaseService db, RestClient restClient, IServiceScopeFactory scopeFactory, IBackgroundTaskQueue taskQueue, ILogger<DebugController> logger)
        {
            db_ = db;
            restClient_ = restClient;
            scopeFactory_ = scopeFactory;
            taskQueue_ = taskQueue;
            logger_ = logger;
        }

        [HttpPost]
        [Route("~/debug/instances")]
        [SwaggerRequestExample(typeof(SliceIntent), typeof(IntentExample))]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateInstance(SliceIntent intent)
        {
            var sliceProfile = intent.SliceProfiles[0];
            var locationConstraint = sliceProfile.LocationConstraints[0];
            var geographicalAreaId = locationConstraint.GeographicalAreaId;
            var geographicalArea = db_.GetGeographicArea(geographicalAreaId);

            if (geographicalArea.Labels is not null)
            {
                locationConstraint.Extra = new();

                foreach (var label in geographicalArea.Labels)
                {
                    locationConstraint.Extra2 = new BsonDocument
                        {
                            new BsonElement(label.Key, label.Value)
                        };

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

            db_.SaveTestObject(vasInfo);

            var saved = db_.GetTestObject(vasInfo.Id);

            logger_.LogInformation(Serialize(saved));

            return Ok();
        }

        [HttpPost]
        [Route("~/debug/callback/slice")]
        [ProducesResponseType(typeof(JsonDocument), StatusCodes.Status200OK)]
        public IActionResult Callback(VasInfo vasInfo)
        {
            logger_.LogInformation("NAO callback: {content}", Serialize(vasInfo));

            return Ok();
        } 

        [HttpDelete]
        [Route("~/debug/callback/namespace/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteNamespace(string name)
        {
            logger_.LogInformation("DeleteNamespace {n}", name);

            return Ok();
        }

        [NonAction]
        private string Serialize<TObject>(TObject obj)
        {
            ArgumentNullException.ThrowIfNull(obj);

            return JsonSerializer.Serialize(obj, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true });
        }

        [NonAction]
        private string BsonSerialize<TObject>(TObject obj)
        {
            ArgumentNullException.ThrowIfNull(obj);

            return obj.ToJson();
        }
    }
}
