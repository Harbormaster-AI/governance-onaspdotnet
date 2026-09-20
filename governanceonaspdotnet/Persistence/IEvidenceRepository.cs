using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IEvidenceRepository
{
    Task<Evidence?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Evidence>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Evidence evidence, CancellationToken cancellationToken);
    Task UpdateAsync(Evidence evidence, CancellationToken cancellationToken);
    Task DeleteAsync(Evidence evidence, CancellationToken cancellationToken);
}
