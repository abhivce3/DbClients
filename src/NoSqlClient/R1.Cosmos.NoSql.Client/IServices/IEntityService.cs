namespace R1.CosmosClient.IServices
{
    public interface IEntityService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(string id, string partitionKey);

        Task AddAsync(T item, string partitionKey);

        Task UpdateAsync(string id, T item, string partitionKey);

        Task DeleteAsync(string id, string partitionKey);
    }
}