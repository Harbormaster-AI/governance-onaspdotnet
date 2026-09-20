using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IMatterRepository
{
    Task<Matter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Matter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Matter matter, CancellationToken cancellationToken);
    Task UpdateAsync(Matter matter, CancellationToken cancellationToken);
    Task DeleteAsync(Matter matter, CancellationToken cancellationToken);
}
