using AspnetCoreIdentity.Domain.Shared;

namespace AspnetCoreIdentity.Domain.Entities
{
    public class Product : Entity
    {
        protected Product() { }
        public Product(string code, Guid categoryId, string name, string description, decimal price)
        {
            Code = code;
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Price = price;
            CreationDate = DateTime.UtcNow;
        }

        public string Code { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}
