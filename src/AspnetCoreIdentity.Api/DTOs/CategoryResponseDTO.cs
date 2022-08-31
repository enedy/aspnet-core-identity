using AspnetCoreIdentity.Domain.Entities;

namespace AspnetCoreIdentity.Api.DTOs
{
    public class CategoryResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CategoryResponseDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static CategoryResponseDTO ConvertToResponse(Category category)
        {
            return new CategoryResponseDTO
            (
                category.Id,
                category.Name
            );
        }
    }
}
