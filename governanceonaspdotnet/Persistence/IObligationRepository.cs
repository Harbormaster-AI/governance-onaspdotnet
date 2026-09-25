using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IObligationRepository
{
    Task<Obligation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Obligation>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Obligation obligation, CancellationToken cancellationToken);
    Task UpdateAsync(Obligation obligation, CancellationToken cancellationToken);
    Task DeleteAsync(Obligation obligation, CancellationToken cancellationToken);

    Task AddToControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromContractsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
