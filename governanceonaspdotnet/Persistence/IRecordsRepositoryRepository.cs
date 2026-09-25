using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IRecordsRepositoryRepository
{
    Task<RecordsRepository?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<RecordsRepository>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken);
    Task UpdateAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken);
    Task DeleteAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken);

    Task AddToRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSystemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSystemsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRetentionSchedulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRetentionSchedulesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToLegalHoldsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromLegalHoldsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
