using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ObligationRepository : IObligationRepository
{
    private readonly ApplicationDbContext _db;

    public ObligationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Obligation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Obligations
            .Include(x => x.Regulation)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Obligation>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Obligations
            .AsNoTracking()
            .Include(x => x.Regulation)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Obligation obligation, CancellationToken cancellationToken)
    {
        _db.Obligations.Add(obligation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Obligation obligation, CancellationToken cancellationToken)
    {
        _db.Obligations.Update(obligation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Obligation obligation, CancellationToken cancellationToken)
    {
        _db.Obligations.Remove(obligation);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
