using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ConsentRepository : IConsentRepository
{
    private readonly ApplicationDbContext _db;

    public ConsentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Consent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Consents
            .Include(x => x.PrivacyNotice)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Consent>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Consents
            .AsNoTracking()
            .Include(x => x.PrivacyNotice)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Consent consent, CancellationToken cancellationToken)
    {
        _db.Consents.Add(consent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Consent consent, CancellationToken cancellationToken)
    {
        _db.Consents.Update(consent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Consent consent, CancellationToken cancellationToken)
    {
        _db.Consents.Remove(consent);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
