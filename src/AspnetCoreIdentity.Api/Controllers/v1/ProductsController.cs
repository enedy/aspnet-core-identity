using AspnetCoreIdentity.Api.Attributes;
using AspnetCoreIdentity.Api.Controllers.Shared;
using AspnetCoreIdentity.Api.DTOs.Request;
using AspnetCoreIdentity.Api.DTOs.Response;
using AspnetCoreIdentity.Domain.Interfaces.Services;
using AspnetCoreIdentity.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreIdentity.Api.Controllers.v1
{
    [Route("api/v1/products")]
    public class ProductsController : ApiControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService) =>
            _productService = productService;

        [ClaimsAuthorize(ClaimTypes.Produto, ClaimValues.Read)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();
            var productsResponse = products.Select(product => ProductResponseDTO.ConvertToResponse(product));
            return Ok(productsResponse);
        }

        [ClaimsAuthorize(ClaimTypes.Produto, ClaimValues.Read)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> GetByIdAsync(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product is null)
                return NotFound();

            var productResponse = ProductResponseDTO.ConvertToResponse(product);
            return Ok(productResponse);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<ActionResult<int>> AddAsync(CreateProductRequestDTO insertProductDTO)
        {
            var product = CreateProductRequestDTO.ConverterToEntity(insertProductDTO);
            var id = (int)await _productService.AddAsync(product);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = id }, id);
        }

        [Authorize(Policy = Policies.CommercialTime)]
        [ClaimsAuthorize(ClaimTypes.Produto, ClaimValues.Delete)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            await _productService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
