using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IRiskRepository
{
    Task<Risk?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Risk>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Risk risk, CancellationToken cancellationToken);
    Task UpdateAsync(Risk risk, CancellationToken cancellationToken);
    Task DeleteAsync(Risk risk, CancellationToken cancellationToken);

    Task AddToControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromControlsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAssessmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAssessmentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToIssuesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromIssuesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToFindingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromFindingsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
