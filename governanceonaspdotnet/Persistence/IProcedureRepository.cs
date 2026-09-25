using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IProcedureRepository
{
    Task<Procedure?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Procedure>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Procedure procedure, CancellationToken cancellationToken);
    Task UpdateAsync(Procedure procedure, CancellationToken cancellationToken);
    Task DeleteAsync(Procedure procedure, CancellationToken cancellationToken);

    Task AddToControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
