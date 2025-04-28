namespace WebAPIAssginment.Models.Repository
{
    public interface IRepository<T> where T : class
    {
        //Task<List<T>> GetAllAsync();
        List<T> GetAll();
        T GetById(int id);
        void Remove(int id);
        void Add(T sender);
        void Update(T sender);
        void Save();
        //T GetByIdWithIncludes(int id);
        //Task<T> GetByIdAsync(int id);
        //Task<T> GetByIdWithIncludesAsync(int id);
        //Task<int> SaveAsync();
        //public T Select(Expression<Func<T, bool>> predicate);
        //public Task<T> SelectAsync(Expression<Func<T, bool>> predicate);
    }
}
