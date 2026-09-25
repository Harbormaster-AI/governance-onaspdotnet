using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IMatterRepository
{
    Task<Matter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Matter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Matter matter, CancellationToken cancellationToken);
    Task UpdateAsync(Matter matter, CancellationToken cancellationToken);
    Task DeleteAsync(Matter matter, CancellationToken cancellationToken);

    Task AddToLegalHoldsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLegalHoldsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataBreachesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataBreachesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
