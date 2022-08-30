using AspnetCoreIdentity.Domain.Shared;

namespace AspnetCoreIdentity.Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; private set; }

        public ICollection<Product> Products { get; private set; }

        public Category(string name)
        {
            Name = name;
        }
    }
}
