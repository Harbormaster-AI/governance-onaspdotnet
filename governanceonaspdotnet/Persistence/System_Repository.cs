using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class System_Repository : ISystem_Repository
{
    private readonly ApplicationDbContext _db;

    public System_Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<System_?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.System_s
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<System_>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.System_s
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(System_ system_, CancellationToken cancellationToken)
    {
        _db.System_s.Add(system_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(System_ system_, CancellationToken cancellationToken)
    {
        _db.System_s.Update(system_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(System_ system_, CancellationToken cancellationToken)
    {
        _db.System_s.Remove(system_);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
