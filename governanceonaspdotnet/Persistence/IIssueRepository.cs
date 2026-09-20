using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IIssueRepository
{
    Task<Issue?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Issue>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Issue issue, CancellationToken cancellationToken);
    Task UpdateAsync(Issue issue, CancellationToken cancellationToken);
    Task DeleteAsync(Issue issue, CancellationToken cancellationToken);
}
