using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IThirdPartyRepository
{
    Task<ThirdParty?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ThirdParty>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ThirdParty thirdParty, CancellationToken cancellationToken);
    Task UpdateAsync(ThirdParty thirdParty, CancellationToken cancellationToken);
    Task DeleteAsync(ThirdParty thirdParty, CancellationToken cancellationToken);

    Task AddToProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAssessmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAssessmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToObligationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromObligationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataBreachesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataBreachesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
