using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Person>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Person person, CancellationToken cancellationToken);
    Task UpdateAsync(Person person, CancellationToken cancellationToken);
    Task DeleteAsync(Person person, CancellationToken cancellationToken);

    Task AddToRoleAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRoleAssignmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToOwnedPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromOwnedPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCorrectiveActionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCorrectiveActionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
