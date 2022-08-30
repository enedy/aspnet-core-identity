using AspnetCoreIdentity.Api.Controllers.Shared;
using AspnetCoreIdentity.Api.DTOs;
using AspnetCoreIdentity.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreIdentity.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CategoryController : ApiControllerBase
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository) =>
            _categoryRepository = categoryRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> ObterTodas()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories.Select(category => CategoryResponse.ConvertToResponse(category)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> ObterPorId(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return NotFound();

            return Ok(CategoryResponse.ConvertToResponse(category));
        }
    }
}
