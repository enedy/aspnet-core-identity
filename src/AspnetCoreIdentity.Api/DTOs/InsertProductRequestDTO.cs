using AspnetCoreIdentity.Domain.Entities;

namespace AspnetCoreIdentity.Api.DTOs
{
    public class InsertProductRequestDTO
    {
        public InsertProductRequestDTO(string code, Guid categoryId, string name, string description, decimal price)
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

        public static Product ConverterToEntity(InsertProductRequestDTO productDTO)
        {
            return new Product
            (
                productDTO.Code,
                productDTO.CategoryId,
                productDTO.Name,
                productDTO.Description,
                productDTO.Price
            );
        }
    }
}
