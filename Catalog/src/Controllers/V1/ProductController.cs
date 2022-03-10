using Request = Core.Contracts.Requests;
using Core.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Catalog.Api.Core.Application.Products.Services;

namespace Catalog.Api.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(typeof(MessageErrorBase), 400)]
    [ProducesResponseType(typeof(MessageErrorBase), 500)]
    [Produces("application/json")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class ProductController : BaseController
    {

        private readonly IProductService _productService;


        public ProductController(IProductService productService) =>
            this._productService = productService ??
                throw new ArgumentNullException(nameof(productService));








        /// <summary>
        /// Obtem um Producto pela pesquisa de ID
        /// </summary>
        /// <param name="id">Id do produto.</param>
        /// <response code="200">Se o produto existir.</response>
        /// <response code="400">Se a requisição não atender os requisitos mínimos.</response>
        /// <response code="404">Se o produto não for encontrado.</response>
        /// <response code="429">Se ocorrer muitas solicitações ao servidor.</response>       
        /// <response code="500">Se ocorrer um erro no servidor.</response>
        /// <returns></returns>
        [HttpGet("[action]/{id}", Name = "GetById")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(Request.Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product is null)
                return NotFound("Product not found");
            return Ok(product);
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
        [HttpGet("[action]/{sku}", Name = "GetBySku")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(Request.Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBySku(string sku)
        {
            var product = await _productService.GetBySkuAsync(sku, GetChannelId());
            if (product is null)
                return NotFound("Product not found");
            return Ok(product);
        }










        /// <summary>
        /// Obtem um Producto pela pesquisa de Categoria
        /// </summary>
        /// <param name="sku">Sku do produto.</param>
        /// <response code="200">Se o produto existir.</response>
        /// <response code="400">Se a requisição não atender os requisitos mínimos.</response>
        /// <response code="404">Se o produto não for encontrado.</response>
        /// <response code="429">Se ocorrer muitas solicitações ao servidor.</response>       
        /// <response code="500">Se ocorrer um erro no servidor.</response>
        /// <returns></returns>
        [HttpGet("[action]/{category}", Name = "GetByCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Request.Product>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var product = await _productService.GetProductsByCategoryAsync(category);
            if (product is null)
                return NotFound("Product not found");
            return Ok(product);
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
        // [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Request.Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveAsync([FromBody] Request.Product request)
        {
            if (request is null)
                return BadRequest("Invalid Product Request.");

            return !ModelState.IsValid ? JsonResult(ModelState) : JsonResult(await _productService.SaveAsync(request, GetChannelId(), CreateHeaderDefault()));
            // return CreatedAtRoute("GetBySku", new { sku = request.Sku }, request); 
        }







        /// <summary>
        /// Atualiza um produto.
        /// </summary>
        /// <param name="product">Produto a ser atualizado.</param>
        /// <response code="200">Se o produto for atualizado com sucesso.</response>
        /// <response code="400">Se a requisição não atender os requisitos mínimos.</response>
        /// <response code="404">Se o produto informado não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro no servidor.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Request.Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] Request.Product request)
        {
            if (request is null)
                return BadRequest("Invalid Product Request.");
            return !ModelState.IsValid ? JsonResult(ModelState) : JsonResult(await _productService.UpdateAsync(request, GetChannelId(), CreateHeaderDefault()));
        }







        /// <summary>
        /// EXclui um produto.
        /// </summary>
        /// <param name="product">Produto a ser excluido.</param>
        /// <response code="200">Se o produto for excluido com sucesso.</response>
        /// <response code="400">Se a requisição não atender os requisitos mínimos.</response>
        /// <response code="404">Se o produto informado não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro no servidor.</response>
        [HttpDelete("{id}")]
        //[Route("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Request.Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
                return BadRequest("Invalid Product Request.");
            return !ModelState.IsValid ? JsonResult(ModelState) : JsonResult(await _productService.DeleteAsync(id, GetChannelId(), CreateHeaderDefault()));
        }
    }
}
