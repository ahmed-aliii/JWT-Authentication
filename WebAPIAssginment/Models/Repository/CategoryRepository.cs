
using Microsoft.EntityFrameworkCore;
using WebAPIAssginment.Models.Data;

namespace WebAPIAssginment.Models.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebAPIContext context;

        public CategoryRepository(WebAPIContext context)
        {
            this.context = context;
        }

        public void Add(Category category)
            => context.Categories.Add(category);


        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return context.Categories.FirstOrDefault(cat => cat.Id == id);
        }

        public Category GetByIdWithIncluse(int id)
        {
            return context.Categories
                .Include(cat => cat.Products)
                .FirstOrDefault(cat => cat.Id == id);
        }

        public void Remove(int id)
        {
            context.Categories.Remove(GetById(id));
        }

        public void Save()
        {
            context.SaveChanges(); 
        }

        public void Update(Category category)
        {
            context.Categories.Update(category);
        }
    }
}
