using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IAuditWorkpaperRepository
{
    Task<AuditWorkpaper?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditWorkpaper>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AuditWorkpaper auditWorkpaper, CancellationToken cancellationToken);
    Task UpdateAsync(AuditWorkpaper auditWorkpaper, CancellationToken cancellationToken);
    Task DeleteAsync(AuditWorkpaper auditWorkpaper, CancellationToken cancellationToken);
}
