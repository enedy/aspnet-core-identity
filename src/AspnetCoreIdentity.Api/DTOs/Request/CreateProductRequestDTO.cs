using AspnetCoreIdentity.Domain.Entities;

namespace AspnetCoreIdentity.Api.DTOs.Request
{
    public class CreateProductRequestDTO
    {
        public CreateProductRequestDTO(string code, Guid categoryId, string name, string description, decimal price)
        {
            Code = code;
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Price = price;
        }

        public string Code { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public static Product ConverterToEntity(CreateProductRequestDTO createProductRequestDTO)
        {
            return new Product
            (
                createProductRequestDTO.Code,
                createProductRequestDTO.CategoryId,
                createProductRequestDTO.Name,
                createProductRequestDTO.Description,
                createProductRequestDTO.Price
            );
        }
    }
}
