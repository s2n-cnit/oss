
using Microsoft.AspNetCore.Mvc;

using Oss.Model;
using Oss.Services;

namespace Oss.Controllers
{
    [ApiController]
    [ApiVersion("0.3")]
    [Produces("application/json")]
    public class LocationController : Controller
    {
        private readonly DatabaseService db_;

        public LocationController(DatabaseService db)
        {
            db_ = db;
        }

        [HttpGet]
        [Route("~/location")]
        [ProducesResponseType(typeof(GeographicArea[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMsg), StatusCodes.Status500InternalServerError)]
        public JsonResult GetLocations()
        {
            return Json(db_.GetGeographicAreas());
        }
    }
}
