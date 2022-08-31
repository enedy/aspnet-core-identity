using AspnetCoreIdentity.Domain.Entities;

namespace AspnetCoreIdentity.Api.DTOs
{
    public class ProductResponseDTO
    {
        public ProductResponseDTO(Guid id, string code, string name, string description, decimal price, DateTime creationDate, 
            CategoryResponseDTO categoryResponse)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            Price = price;
            CreationDate = creationDate;
            CategoryDTO = categoryResponse;
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public CategoryResponseDTO CategoryDTO { get; set; }

        public static ProductResponseDTO ConvertToResponse(Product produto)
        {
            return new ProductResponseDTO
            (
                produto.Id,
                produto.Code,
                produto.Name,
                produto.Description,
                produto.Price,
                produto.CreationDate,
                new CategoryResponseDTO(produto.Category.Id, produto.Category.Name)
            );
        }
    }
}
