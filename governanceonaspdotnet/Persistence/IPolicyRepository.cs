using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IPolicyRepository
{
    Task<Policy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Policy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Policy policy, CancellationToken cancellationToken);
    Task UpdateAsync(Policy policy, CancellationToken cancellationToken);
    Task DeleteAsync(Policy policy, CancellationToken cancellationToken);

    Task AddToOwnersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOwnersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRelatedRequirementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRelatedRequirementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToProceduresAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProceduresAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToExceptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromExceptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAttestationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAttestationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
