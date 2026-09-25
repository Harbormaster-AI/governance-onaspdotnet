using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Role role, CancellationToken cancellationToken);
    Task UpdateAsync(Role role, CancellationToken cancellationToken);
    Task DeleteAsync(Role role, CancellationToken cancellationToken);

    Task AddToAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
