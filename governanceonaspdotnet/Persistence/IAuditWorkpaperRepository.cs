using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IAuditWorkpaperRepository
{
    Task<AuditWorkpaper?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditWorkpaper>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AuditWorkpaper auditWorkpaper, CancellationToken cancellationToken);
    Task UpdateAsync(AuditWorkpaper auditWorkpaper, CancellationToken cancellationToken);
    Task DeleteAsync(AuditWorkpaper auditWorkpaper, CancellationToken cancellationToken);

    Task AddToEvidenceAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEvidenceAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToFindingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFindingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
