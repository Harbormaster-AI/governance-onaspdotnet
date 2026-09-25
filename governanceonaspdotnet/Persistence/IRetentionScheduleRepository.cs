using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IRetentionScheduleRepository
{
    Task<RetentionSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<RetentionSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(RetentionSchedule retentionSchedule, CancellationToken cancellationToken);
    Task UpdateAsync(RetentionSchedule retentionSchedule, CancellationToken cancellationToken);
    Task DeleteAsync(RetentionSchedule retentionSchedule, CancellationToken cancellationToken);

    Task AddToRepositoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRepositoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRecordsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToExceptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromExceptionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDispositionReviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDispositionReviewsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
