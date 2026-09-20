using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class Record_Repository : IRecord_Repository
{
    private readonly ApplicationDbContext _db;

    public Record_Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Record_?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Record_s
            .Include(x => x.Repository)
            .Include(x => x.RetentionSchedule)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Record_>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Record_s
            .AsNoTracking()
            .Include(x => x.Repository)
            .Include(x => x.RetentionSchedule)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Record_ record_, CancellationToken cancellationToken)
    {
        _db.Record_s.Add(record_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Record_ record_, CancellationToken cancellationToken)
    {
        _db.Record_s.Update(record_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Record_ record_, CancellationToken cancellationToken)
    {
        _db.Record_s.Remove(record_);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
