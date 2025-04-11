namespace R1.CosmosClient.Domain.Data
{
    public interface ICosmosDbRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(string id, string partitionKey);

        Task AddAsync(T entity, string partitionKey);

        Task UpdateAsync(string id, T entity, string partitionKey);

        Task DeleteAsync(string id, string partitionKey);
    }
}