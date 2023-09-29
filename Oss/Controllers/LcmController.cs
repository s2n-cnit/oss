
using System.Text.Json;
using System.Runtime.InteropServices;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Filters;

using MediatR;

using Extensions;

using Oss.Code;
using Oss.Model;
using Oss.Model.Request;
using Oss.Model.Response;
using Oss.Extensions;
using Oss.Requests;

namespace Oss.Controllers
{
    [ApiController]
    [ApiVersion("0.3")]
    [Produces("application/json")]
    public class LcmController: Controller
    {
        private readonly IMediator mediator_;
        private readonly ILogger logger_;

        public LcmController(IMediator mediator, ILogger<LcmController> logger)
        {
            mediator_ = mediator;
            logger_ = logger;
        }

        [HttpGet]
        [Route("~/lcm/instances")]
        [ProducesResponseType(typeof(VasInfo[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInstancesAsync([FromHeader(Name = "X-Fields")] string[] fieldsHeaders)
        {
            var result = await mediator_.Send(new GetInstancesRequest
            {
                Fields = fieldsHeaders
            });

            return result.Match<IActionResult>(Json, m => StatusCode(StatusCodes.Status500InternalServerError, m.MapToResponse()));
        }

        [HttpGet]
        [Route("~/lcm/instances/{id}")]
        [ProducesResponseType(typeof(VasInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInstanceAsync([FromHeader(Name = "X-Fields")] string[] fieldsHeaders, string id)
        {
            var result = await mediator_.Send(new GetInstanceRequest
            {
                InstanceId = id,
                Fields = fieldsHeaders,
            });

            return result.Match<IActionResult>(Json, m => NotFound(m.MapToResponse()));
        }

        [HttpPost]
        [Route("~/lcm/instances")]
        [SwaggerRequestExample(typeof(SliceIntent), typeof(IntentExample))]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateInstanceAsync([FromBody] SliceIntent intent)
        {
            SaveIntentFile(intent);

            var result = await mediator_.Send(new CreateSliceRequest
            {
                Intent = intent
            });

            return result.Match<IActionResult>(m => Accepted(m as object), m => BadRequest(m.MapToResponse()));
        }

        [HttpDelete]
        [Route("~/lcm/instances/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteInstanceAsync(string id)
        {
            var result = await mediator_.Send(new DeleteSliceRequest
            {
                SliceId = id
            });

            return result.Match<IActionResult>(m => Accepted(m as object), m => NotFound(m.MapToResponse()));
        }

        [NonAction]
        private void SaveIntentFile(SliceIntent intent)
        {
            var intentFileName = $"slice-{intent.Name}-{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}";

            if (intent.Extra is not null && !intent.Extra.IsEmpty())
            {
                logger_.LogWarning("CreateInstance({n}) extra data is present.", intentFileName);
            }

            var templatePath = RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? "/intents/{0}.json"
                : @"D:\Documents\___Desktop\intents\{0}.json";

            System.IO.File.WriteAllTextAsync(string.Format(templatePath, intentFileName), JsonSerializer.Serialize(intent));
        }
    }
}
