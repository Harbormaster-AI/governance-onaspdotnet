
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class DispositionReviewRepository : IDispositionReviewRepository
{
    private readonly ApplicationDbContext _db;

    public DispositionReviewRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DispositionReview?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DispositionReviews
            .Include(x => x.Record)
            .Include(x => x.RetentionSchedule)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DispositionReview>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DispositionReviews
            .AsNoTracking()
            .Include(x => x.Record)
            .Include(x => x.RetentionSchedule)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DispositionReview dispositionReview, CancellationToken cancellationToken)
    {
        _db.DispositionReviews.Add(dispositionReview);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DispositionReview dispositionReview, CancellationToken cancellationToken)
    {
        _db.DispositionReviews.Update(dispositionReview);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DispositionReview dispositionReview, CancellationToken cancellationToken)
    {
        _db.DispositionReviews.Remove(dispositionReview);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
