using AspnetCoreIdentity.Domain.Shared;

namespace AspnetCoreIdentity.Domain.Entities
{
    public class Product : Entity
    {
        protected Product() { }
        public Product(string code, int categoryId, Category category, string name, string description, decimal price, DateTime creationDate)
        {
            Code = code;
            CategoryId = categoryId;
            Category = category;
            Name = name;
            Description = description;
            Price = price;
            CreationDate = creationDate;
        }

        public string Code { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}
