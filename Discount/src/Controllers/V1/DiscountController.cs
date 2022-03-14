using Core.Contracts.Responses;
using Discount.Api.Entities;
using Discount.Api.Repositories;
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

        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{productName}/has-discount")]
        public async Task<ActionResult<Coupon>> GetDiscountAsync(string productName)
        {
            return Ok(await _discountRepository.GetDiscount(productName));
        }



        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
           await _discountRepository.CreateDiscount(coupon);
            return CreatedAtAction("has-discount", new { productName = coupon.ProductName}, coupon);
        }


        [MapToApiVersion("1.0")]
        [HttpPut]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await _discountRepository.UpdateDiscount(coupon));

        }


        [MapToApiVersion("1.0")]
        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        public async Task<ActionResult<Coupon>> DeleteDiscount(string productName)
        {
            return Ok(await _discountRepository.DeleteDiscount(productName));

        }
    }
}
