using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ComplianceRequirementRepository : IComplianceRequirementRepository
{
    private readonly ApplicationDbContext _db;

    public ComplianceRequirementRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ComplianceRequirement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ComplianceRequirements
            .Include(x => x.ComplianceProgram)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ComplianceRequirement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ComplianceRequirements
            .AsNoTracking()
            .Include(x => x.ComplianceProgram)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken)
    {
        _db.ComplianceRequirements.Add(complianceRequirement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken)
    {
        _db.ComplianceRequirements.Update(complianceRequirement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken)
    {
        _db.ComplianceRequirements.Remove(complianceRequirement);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
