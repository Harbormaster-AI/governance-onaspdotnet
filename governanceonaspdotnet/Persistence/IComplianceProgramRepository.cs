using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IComplianceProgramRepository
{
    Task<ComplianceProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ComplianceProgram>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken);
    Task UpdateAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken);
    Task DeleteAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken);

    Task AddToRequirementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRequirementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAttestationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAttestationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRegulationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRegulationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
