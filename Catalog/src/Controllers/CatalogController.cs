﻿using Catalog.Api.Domain;
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
    public class CatalogController : ControllerBase // BaseController
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
        [HttpGet("[action]/{sku}", Name = "GetBySku")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBySku(string sku)
        {
            var produto = await _productRepository.GetBySkuAsync(sku);
            if (produto is null)
                return NotFound("Product not found");
            return Ok(produto);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var product = await _productRepository.GetProductsByCategoryAsync(category);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (product is null)
                return BadRequest("Invalid Product Request.");

            await _productRepository.CreateAsync(product).ConfigureAwait(false);

            return CreatedAtRoute("GetBySku", new { sku = product.Sku }, product);

            //return !ModelState.IsValid ? BadRequest(ModelState) : Created("",null);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (product is null)
                return BadRequest("Invalid Product Request.");
            
            return Ok(await _productRepository.UpdateAsync(product));
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
                return BadRequest("Invalid Product Request.");
            return Ok(await _productRepository.DeleteAsync(id));
        }

    }
}
