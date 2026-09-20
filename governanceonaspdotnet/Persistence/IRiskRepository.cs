using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IRiskRepository
{
    Task<Risk?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Risk>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Risk risk, CancellationToken cancellationToken);
    Task UpdateAsync(Risk risk, CancellationToken cancellationToken);
    Task DeleteAsync(Risk risk, CancellationToken cancellationToken);
}
