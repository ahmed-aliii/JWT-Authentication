namespace WebAPIAssginment.Models.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByIdWithIncluse(int id);
    }
}
