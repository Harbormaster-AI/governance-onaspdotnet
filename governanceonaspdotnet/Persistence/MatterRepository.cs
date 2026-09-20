using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class MatterRepository : IMatterRepository
{
    private readonly ApplicationDbContext _db;

    public MatterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Matter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Matters
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Matter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Matters
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Matter matter, CancellationToken cancellationToken)
    {
        _db.Matters.Add(matter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Matter matter, CancellationToken cancellationToken)
    {
        _db.Matters.Update(matter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Matter matter, CancellationToken cancellationToken)
    {
        _db.Matters.Remove(matter);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
