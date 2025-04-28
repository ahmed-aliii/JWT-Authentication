
using System.Reflection.Metadata.Ecma335;
using WebAPIAssginment.Models.Data;

namespace WebAPIAssginment.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebAPIContext context;

        public ProductRepository(WebAPIContext context)
        {
            this.context = context;
        }

        public void Add(Product product)
            => context.Products.Add(product);

        public List<Product> GetAll()
            => context.Products.ToList();

        public Product GetById(int id)
            => context.Products.FirstOrDefault(cat => cat.Id == id);

        public void Remove(int id)
            => context.Products.Remove( GetById(id) );

        public void Update(Product product)
            => context.Products.Update( product );

        
        public void Save()
            => context.SaveChanges();

        
    }
}
