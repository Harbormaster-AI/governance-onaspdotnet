using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface ISystem_Repository
{
    Task<System_?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<System_>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(System_ system_, CancellationToken cancellationToken);
    Task UpdateAsync(System_ system_, CancellationToken cancellationToken);
    Task DeleteAsync(System_ system_, CancellationToken cancellationToken);

    Task AddToProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRecordsRepositoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRecordsRepositoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
