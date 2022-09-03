using AspnetCoreIdentity.Api.Controllers.Shared;
using AspnetCoreIdentity.Api.DTOs.Response;
using AspnetCoreIdentity.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreIdentity.Api.Controllers.v1
{
    [Authorize]
    [Route("api/v1/categories")]
    public class CategoriesController : ApiControllerBase
    {
        private ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository) =>
            _categoryRepository = categoryRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponseDTO>>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories.Select(category => CategoryResponseDTO.ConvertToResponse(category)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponseDTO>> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return NotFound();

            return Ok(CategoryResponseDTO.ConvertToResponse(category));
        }
    }
}
