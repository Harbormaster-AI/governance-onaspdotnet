using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IComplianceRequirementRepository
{
    Task<ComplianceRequirement?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ComplianceRequirement>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken);
    Task UpdateAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken);
    Task DeleteAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken);

    Task AddToPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToObligationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromObligationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
