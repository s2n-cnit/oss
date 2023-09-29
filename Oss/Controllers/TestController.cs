
using Microsoft.AspNetCore.Mvc;

using Oss.Model;
using Oss.Services;

namespace Oss.Controllers
{
    public class TestController: Controller
    {
        private readonly DatabaseService db_;
        private readonly NfvclClientService nfvclClient_;
        private readonly ILogger logger_;

        public TestController(DatabaseService db, NfvclClientService nfvclClient, ILogger<TestController> logger)
        {
            db_ = db;
            nfvclClient_ = nfvclClient;
            logger_ = logger;
        }

        [HttpPost]
        [Route("~/test/namespace/{areaId}/{name}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNamespace(string areaId, string name)
        {
            var area = db_.GetGeographicArea(areaId);
            var blueprint = db_.GetBlueprint(areaId);
            var clusterId = blueprint.K8sClusterId;

            var nfvclUrl = string.Format(area.NfvclUrlTemplateForK8s, clusterId, name);
            var reply = await nfvclClient_.CreateNamespace(nfvclUrl, new());

            logger_.LogInformation("CreateNamespace: Sent request to NFVCL, result: {sc}", reply.Status);

            return NoContent();
        }

        [HttpDelete]
        [Route("~/test/namespace/{areaId}/{name}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteNamespace(string areaId, string name)
        {
            var area = db_.GetGeographicArea(areaId);
            var blueprint = db_.GetBlueprint(areaId);
            var clusterId = blueprint.K8sClusterId;

            var nfvclUrl = string.Format(area.NfvclUrlTemplateForK8s, clusterId, name);
            var reply = await nfvclClient_.DeleteNamespace(nfvclUrl);

            logger_.LogInformation("DeleteNamespace: Sent request to NFVCL, result: {sc}", reply.Status);

            return NoContent();
        }
    }
}
