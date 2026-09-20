using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class AuditProgramRepository : IAuditProgramRepository
{
    private readonly ApplicationDbContext _db;

    public AuditProgramRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AuditProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AuditPrograms
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AuditProgram>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AuditPrograms
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AuditProgram auditProgram, CancellationToken cancellationToken)
    {
        _db.AuditPrograms.Add(auditProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AuditProgram auditProgram, CancellationToken cancellationToken)
    {
        _db.AuditPrograms.Update(auditProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AuditProgram auditProgram, CancellationToken cancellationToken)
    {
        _db.AuditPrograms.Remove(auditProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
