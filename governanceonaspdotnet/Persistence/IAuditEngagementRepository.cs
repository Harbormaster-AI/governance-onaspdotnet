using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IAuditEngagementRepository
{
    Task<AuditEngagement?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditEngagement>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken);
    Task UpdateAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken);
    Task DeleteAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken);

    Task AddToBusinessUnitsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBusinessUnitsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToControlTestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromControlTestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToWorkpapersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromWorkpapersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToFindingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFindingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
