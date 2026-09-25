using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IGovernanceBodyRepository
{
    Task<GovernanceBody?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<GovernanceBody>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(GovernanceBody governanceBody, CancellationToken cancellationToken);
    Task UpdateAsync(GovernanceBody governanceBody, CancellationToken cancellationToken);
    Task DeleteAsync(GovernanceBody governanceBody, CancellationToken cancellationToken);

    Task AddToRoleAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRoleAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
