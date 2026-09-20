using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class AuditWorkpaperRepository : IAuditWorkpaperRepository
{
    private readonly ApplicationDbContext _db;

    public AuditWorkpaperRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AuditWorkpaper?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AuditWorkpapers
            .Include(x => x.Engagement)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AuditWorkpaper>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AuditWorkpapers
            .AsNoTracking()
            .Include(x => x.Engagement)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AuditWorkpaper auditWorkpaper, CancellationToken cancellationToken)
    {
        _db.AuditWorkpapers.Add(auditWorkpaper);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AuditWorkpaper auditWorkpaper, CancellationToken cancellationToken)
    {
        _db.AuditWorkpapers.Update(auditWorkpaper);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AuditWorkpaper auditWorkpaper, CancellationToken cancellationToken)
    {
        _db.AuditWorkpapers.Remove(auditWorkpaper);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
