using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class LegalHoldRepository : ILegalHoldRepository
{
    private readonly ApplicationDbContext _db;

    public LegalHoldRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<LegalHold?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.LegalHolds
            .Include(x => x.Matter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<LegalHold>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.LegalHolds
            .AsNoTracking()
            .Include(x => x.Matter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(LegalHold legalHold, CancellationToken cancellationToken)
    {
        _db.LegalHolds.Add(legalHold);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(LegalHold legalHold, CancellationToken cancellationToken)
    {
        _db.LegalHolds.Update(legalHold);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LegalHold legalHold, CancellationToken cancellationToken)
    {
        _db.LegalHolds.Remove(legalHold);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
