using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IDataCategoryRepository
{
    Task<DataCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataCategory>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DataCategory dataCategory, CancellationToken cancellationToken);
    Task UpdateAsync(DataCategory dataCategory, CancellationToken cancellationToken);
    Task DeleteAsync(DataCategory dataCategory, CancellationToken cancellationToken);
}
