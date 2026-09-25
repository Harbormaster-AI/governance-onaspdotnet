using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IRoleAssignmentRepository
{
    Task<RoleAssignment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<RoleAssignment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(RoleAssignment roleAssignment, CancellationToken cancellationToken);
    Task UpdateAsync(RoleAssignment roleAssignment, CancellationToken cancellationToken);
    Task DeleteAsync(RoleAssignment roleAssignment, CancellationToken cancellationToken);


}
