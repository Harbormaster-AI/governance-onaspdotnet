using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ControlTest_Repository : IControlTest_Repository
{
    private readonly ApplicationDbContext _db;

    public ControlTest_Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ControlTest_?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ControlTest_s
            .Include(x => x.Control)
            .Include(x => x.Engagement)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ControlTest_>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ControlTest_s
            .AsNoTracking()
            .Include(x => x.Control)
            .Include(x => x.Engagement)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ControlTest_ controlTest_, CancellationToken cancellationToken)
    {
        _db.ControlTest_s.Add(controlTest_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ControlTest_ controlTest_, CancellationToken cancellationToken)
    {
        _db.ControlTest_s.Update(controlTest_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ControlTest_ controlTest_, CancellationToken cancellationToken)
    {
        _db.ControlTest_s.Remove(controlTest_);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
