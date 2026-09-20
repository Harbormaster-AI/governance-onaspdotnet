using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IAuditFindingRepository
{
    Task<AuditFinding?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditFinding>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AuditFinding auditFinding, CancellationToken cancellationToken);
    Task UpdateAsync(AuditFinding auditFinding, CancellationToken cancellationToken);
    Task DeleteAsync(AuditFinding auditFinding, CancellationToken cancellationToken);
}
