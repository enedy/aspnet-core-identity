using AspnetCoreIdentity.Domain.Entities;

namespace AspnetCoreIdentity.Api.DTOs
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CategoryResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static CategoryResponse ConvertToResponse(Category category)
        {
            return new CategoryResponse
            (
                category.Id,
                category.Name
            );
        }
    }
}
