
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class Exception_Repository : IException_Repository
{
    private readonly ApplicationDbContext _db;

    public Exception_Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Exception_?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Exception_s
            .Include(x => x.RetentionSchedule)
            .Include(x => x.Policy)
            .Include(x => x.Control)
            .Include(x => x.Risk)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Exception_>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Exception_s
            .AsNoTracking()
            .Include(x => x.RetentionSchedule)
            .Include(x => x.Policy)
            .Include(x => x.Control)
            .Include(x => x.Risk)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Exception_ exception_, CancellationToken cancellationToken)
    {
        _db.Exception_s.Add(exception_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Exception_ exception_, CancellationToken cancellationToken)
    {
        _db.Exception_s.Update(exception_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Exception_ exception_, CancellationToken cancellationToken)
    {
        _db.Exception_s.Remove(exception_);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
