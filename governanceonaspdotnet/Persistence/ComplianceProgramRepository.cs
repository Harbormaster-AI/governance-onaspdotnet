using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ComplianceProgramRepository : IComplianceProgramRepository
{
    private readonly ApplicationDbContext _db;

    public ComplianceProgramRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ComplianceProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CompliancePrograms
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ComplianceProgram>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CompliancePrograms
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken)
    {
        _db.CompliancePrograms.Add(complianceProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken)
    {
        _db.CompliancePrograms.Update(complianceProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken)
    {
        _db.CompliancePrograms.Remove(complianceProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
