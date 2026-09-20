using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class PrivacyNoticeRepository : IPrivacyNoticeRepository
{
    private readonly ApplicationDbContext _db;

    public PrivacyNoticeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PrivacyNotice?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PrivacyNotices
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PrivacyNotice>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PrivacyNotices
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PrivacyNotice privacyNotice, CancellationToken cancellationToken)
    {
        _db.PrivacyNotices.Add(privacyNotice);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PrivacyNotice privacyNotice, CancellationToken cancellationToken)
    {
        _db.PrivacyNotices.Update(privacyNotice);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PrivacyNotice privacyNotice, CancellationToken cancellationToken)
    {
        _db.PrivacyNotices.Remove(privacyNotice);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
