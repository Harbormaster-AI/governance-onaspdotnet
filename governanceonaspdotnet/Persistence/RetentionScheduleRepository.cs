using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class RetentionScheduleRepository : IRetentionScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public RetentionScheduleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<RetentionSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.RetentionSchedules
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<RetentionSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.RetentionSchedules
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RetentionSchedule retentionSchedule, CancellationToken cancellationToken)
    {
        _db.RetentionSchedules.Add(retentionSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RetentionSchedule retentionSchedule, CancellationToken cancellationToken)
    {
        _db.RetentionSchedules.Update(retentionSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(RetentionSchedule retentionSchedule, CancellationToken cancellationToken)
    {
        _db.RetentionSchedules.Remove(retentionSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
