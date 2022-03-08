using Catalog.Api.Domain;
using Catalog.Api.Repository;
using Catalog.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(MessageErrorBase), 400)]
    [ProducesResponseType(typeof(MessageErrorBase), 500)]
    //[ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class CatalogController : BaseController
    {

        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            this._productRepository = productRepository ??
                throw new ArgumentNullException(nameof(productRepository));
        }



        /// <summary>
        /// Obtem um Producto pela pesquisa de SKU
        /// </summary>
        /// <param name="sku">Sku do produto.</param>
        /// <response code="200">Se o produto existir.</response>
        /// <response code="400">Se a requisição não atender os requisitos mínimos.</response>
        /// <response code="404">Se o produto não for encontrado.</response>
        /// <response code="429">Se ocorrer muitas solicitações ao servidor.</response>       
        /// <response code="500">Se ocorrer um erro no servidor.</response>
        /// <returns></returns>
        [HttpGet("{sku}", Name = "GetBySku")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string sku)
        {

            var produto = await _productRepository.GetBySkuAsync(sku);
            if (produto is null)
            {
                return NotFound("Product not found");
            }
            return JsonResult(produto);
        }








        /// <summary>
        /// Insere um novo produto.
        /// </summary>
        /// <param name="product">Produto a ser adicionado.</param>
        /// <response code="200">Se o produto for criado com sucesso.</response>
        /// <response code="400">Se a requisição não atender os requisitos mínimos.</response>
        /// <response code="404">Se o produto informado não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro no servidor.</response>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Product product)
        {         
            await _productRepository.CreateAsync(product).ConfigureAwait(false);
            return !ModelState.IsValid ? JsonResult(ModelState) : JsonResult();
        }


    }
}
