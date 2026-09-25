using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IOrganizationRepository
{
    Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Organization>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Organization organization, CancellationToken cancellationToken);
    Task UpdateAsync(Organization organization, CancellationToken cancellationToken);
    Task DeleteAsync(Organization organization, CancellationToken cancellationToken);

    Task AddToGovernanceBodiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromGovernanceBodiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRisksAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRisksAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToThirdPartiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromThirdPartiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRecordsRepositoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRecordsRepositoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToComplianceProgramsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromComplianceProgramsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAuditProgramsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAuditProgramsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToBusinessUnitsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBusinessUnitsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToMattersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromMattersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataBreachesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataBreachesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
