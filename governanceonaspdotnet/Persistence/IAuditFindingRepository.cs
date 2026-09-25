using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IAuditFindingRepository
{
    Task<AuditFinding?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditFinding>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AuditFinding auditFinding, CancellationToken cancellationToken);
    Task UpdateAsync(AuditFinding auditFinding, CancellationToken cancellationToken);
    Task DeleteAsync(AuditFinding auditFinding, CancellationToken cancellationToken);

    Task AddToCorrectiveActionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCorrectiveActionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRelatedRisksAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRelatedRisksAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRelatedControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRelatedControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToIssuesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromIssuesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
