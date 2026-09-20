using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IAuditEngagementRepository
{
    Task<AuditEngagement?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditEngagement>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken);
    Task UpdateAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken);
    Task DeleteAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken);
}
