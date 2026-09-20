using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IObligationRepository
{
    Task<Obligation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Obligation>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Obligation obligation, CancellationToken cancellationToken);
    Task UpdateAsync(Obligation obligation, CancellationToken cancellationToken);
    Task DeleteAsync(Obligation obligation, CancellationToken cancellationToken);
}
