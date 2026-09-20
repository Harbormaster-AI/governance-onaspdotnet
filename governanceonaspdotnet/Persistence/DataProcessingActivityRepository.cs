using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class DataProcessingActivityRepository : IDataProcessingActivityRepository
{
    private readonly ApplicationDbContext _db;

    public DataProcessingActivityRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DataProcessingActivity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DataProcessingActivitys
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DataProcessingActivity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DataProcessingActivitys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken)
    {
        _db.DataProcessingActivitys.Add(dataProcessingActivity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken)
    {
        _db.DataProcessingActivitys.Update(dataProcessingActivity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken)
    {
        _db.DataProcessingActivitys.Remove(dataProcessingActivity);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
