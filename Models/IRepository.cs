namespace TequliasResturant.Models
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id, QueryOptions<T> options);
        Task<IEnumerable<T>> GetAllByIdAsync<Tkey>(Tkey id, string propertyName, QueryOptions<T> options);
        Task AddAsync(T entity);
        Task UpgateAsync(T entity);
        Task DeleteAsync(int id);

    }
}