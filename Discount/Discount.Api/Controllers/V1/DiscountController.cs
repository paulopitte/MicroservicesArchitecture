using Core.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Discount.Api.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(typeof(MessageErrorBase), 400)]
    [ProducesResponseType(typeof(MessageErrorBase), 500)]
    [Produces("application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class DiscountController : ControllerBase
    {

        [MapToApiVersion("1.0")]
        [HttpGet("{sku}/has-discount")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
