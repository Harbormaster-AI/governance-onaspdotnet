using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class AuditEngagementRepository : IAuditEngagementRepository
{
    private readonly ApplicationDbContext _db;

    public AuditEngagementRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AuditEngagement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AuditEngagements
            .Include(x => x.AuditProgram)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AuditEngagement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AuditEngagements
            .AsNoTracking()
            .Include(x => x.AuditProgram)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken)
    {
        _db.AuditEngagements.Add(auditEngagement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken)
    {
        _db.AuditEngagements.Update(auditEngagement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken)
    {
        _db.AuditEngagements.Remove(auditEngagement);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
