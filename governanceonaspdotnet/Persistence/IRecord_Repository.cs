using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IRecord_Repository
{
    Task<Record_?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Record_>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Record_ record_, CancellationToken cancellationToken);
    Task UpdateAsync(Record_ record_, CancellationToken cancellationToken);
    Task DeleteAsync(Record_ record_, CancellationToken cancellationToken);

    Task AddToProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLegalHoldsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLegalHoldsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataSubjectRequestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataSubjectRequestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
