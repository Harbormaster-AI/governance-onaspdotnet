using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class AuditFindingRepository : IAuditFindingRepository
{
    private readonly ApplicationDbContext _db;

    public AuditFindingRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AuditFinding?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AuditFindings
            .Include(x => x.Engagement)
            .Include(x => x.Workpaper)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AuditFinding>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AuditFindings
            .AsNoTracking()
            .Include(x => x.Engagement)
            .Include(x => x.Workpaper)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AuditFinding auditFinding, CancellationToken cancellationToken)
    {
        _db.AuditFindings.Add(auditFinding);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AuditFinding auditFinding, CancellationToken cancellationToken)
    {
        _db.AuditFindings.Update(auditFinding);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AuditFinding auditFinding, CancellationToken cancellationToken)
    {
        _db.AuditFindings.Remove(auditFinding);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
