using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class RecordsRepositoryRepository : IRecordsRepositoryRepository
{
    private readonly ApplicationDbContext _db;

    public RecordsRepositoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<RecordsRepository?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.RecordsRepositorys
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<RecordsRepository>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.RecordsRepositorys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken)
    {
        _db.RecordsRepositorys.Add(recordsRepository);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken)
    {
        _db.RecordsRepositorys.Update(recordsRepository);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken)
    {
        _db.RecordsRepositorys.Remove(recordsRepository);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
