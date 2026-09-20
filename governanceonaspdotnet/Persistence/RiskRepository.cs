using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class RiskRepository : IRiskRepository
{
    private readonly ApplicationDbContext _db;

    public RiskRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Risk?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Risks
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Risk>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Risks
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Risk risk, CancellationToken cancellationToken)
    {
        _db.Risks.Add(risk);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Risk risk, CancellationToken cancellationToken)
    {
        _db.Risks.Update(risk);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Risk risk, CancellationToken cancellationToken)
    {
        _db.Risks.Remove(risk);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
