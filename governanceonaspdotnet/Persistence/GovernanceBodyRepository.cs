using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class GovernanceBodyRepository : IGovernanceBodyRepository
{
    private readonly ApplicationDbContext _db;

    public GovernanceBodyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<GovernanceBody?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.GovernanceBodys
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<GovernanceBody>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.GovernanceBodys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(GovernanceBody governanceBody, CancellationToken cancellationToken)
    {
        _db.GovernanceBodys.Add(governanceBody);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(GovernanceBody governanceBody, CancellationToken cancellationToken)
    {
        _db.GovernanceBodys.Update(governanceBody);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(GovernanceBody governanceBody, CancellationToken cancellationToken)
    {
        _db.GovernanceBodys.Remove(governanceBody);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
