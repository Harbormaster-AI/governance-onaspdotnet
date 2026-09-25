
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class EvidenceRepository : IEvidenceRepository
{
    private readonly ApplicationDbContext _db;

    public EvidenceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Evidence?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Evidences
            .Include(x => x.ControlTest)
            .Include(x => x.Control)
            .Include(x => x.Obligation)
            .Include(x => x.Workpaper)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Evidence>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Evidences
            .AsNoTracking()
            .Include(x => x.ControlTest)
            .Include(x => x.Control)
            .Include(x => x.Obligation)
            .Include(x => x.Workpaper)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Evidence evidence, CancellationToken cancellationToken)
    {
        _db.Evidences.Add(evidence);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Evidence evidence, CancellationToken cancellationToken)
    {
        _db.Evidences.Update(evidence);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Evidence evidence, CancellationToken cancellationToken)
    {
        _db.Evidences.Remove(evidence);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
