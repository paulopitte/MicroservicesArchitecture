using Basket.Api.Entities;
using Basket.Api.Repositories;
using Core.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Basket.Api.Controllers.V2
{

    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(typeof(MessageErrorBase), 400)]
    [ProducesResponseType(typeof(MessageErrorBase), 500)]
    [Produces("application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBasketAsync(string userName)
        {
            var basket = await _basketRepository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }


    }
}
