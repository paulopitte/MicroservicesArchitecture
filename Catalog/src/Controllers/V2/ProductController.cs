using Core.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Catalog.Api.Controllers.V2
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(typeof(MessageErrorBase), 400)]
    [ProducesResponseType(typeof(MessageErrorBase), 500)]
    [Produces("application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class ProductController : BaseController
    {
        [MapToApiVersion("2.0")]
        [HttpGet("{sku}/has-product")]
        public IActionResult Index()
        {
            return Ok(false);
        }
    }
}
