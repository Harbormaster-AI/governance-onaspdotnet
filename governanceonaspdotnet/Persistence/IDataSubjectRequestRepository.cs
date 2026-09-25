using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IDataSubjectRequestRepository
{
    Task<DataSubjectRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataSubjectRequest>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DataSubjectRequest dataSubjectRequest, CancellationToken cancellationToken);
    Task UpdateAsync(DataSubjectRequest dataSubjectRequest, CancellationToken cancellationToken);
    Task DeleteAsync(DataSubjectRequest dataSubjectRequest, CancellationToken cancellationToken);

    Task AddToProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
