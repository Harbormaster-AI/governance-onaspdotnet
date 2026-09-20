using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IDispositionReviewRepository
{
    Task<DispositionReview?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DispositionReview>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DispositionReview dispositionReview, CancellationToken cancellationToken);
    Task UpdateAsync(DispositionReview dispositionReview, CancellationToken cancellationToken);
    Task DeleteAsync(DispositionReview dispositionReview, CancellationToken cancellationToken);
}
