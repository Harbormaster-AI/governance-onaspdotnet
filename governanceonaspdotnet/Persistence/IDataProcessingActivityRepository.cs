using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IDataProcessingActivityRepository
{
    Task<DataProcessingActivity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataProcessingActivity>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken);
    Task UpdateAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken);
    Task DeleteAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken);

    Task AddToDataCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSystemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSystemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPrivacyNoticesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPrivacyNoticesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToThirdPartiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromThirdPartiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToConsentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromConsentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataBreachesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataBreachesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataSubjectRequestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataSubjectRequestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
