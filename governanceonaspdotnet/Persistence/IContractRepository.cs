using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IContractRepository
{
    Task<Contract?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Contract>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Contract contract, CancellationToken cancellationToken);
    Task UpdateAsync(Contract contract, CancellationToken cancellationToken);
    Task DeleteAsync(Contract contract, CancellationToken cancellationToken);

    Task AddToObligationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromObligationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
