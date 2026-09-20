using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IGovernanceBodyRepository
{
    Task<GovernanceBody?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<GovernanceBody>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(GovernanceBody governanceBody, CancellationToken cancellationToken);
    Task UpdateAsync(GovernanceBody governanceBody, CancellationToken cancellationToken);
    Task DeleteAsync(GovernanceBody governanceBody, CancellationToken cancellationToken);
}
