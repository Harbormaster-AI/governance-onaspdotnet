using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IDataCategoryRepository
{
    Task<DataCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataCategory>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DataCategory dataCategory, CancellationToken cancellationToken);
    Task UpdateAsync(DataCategory dataCategory, CancellationToken cancellationToken);
    Task DeleteAsync(DataCategory dataCategory, CancellationToken cancellationToken);

    Task AddToProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataBreachesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataBreachesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
