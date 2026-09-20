using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class DataCategoryRepository : IDataCategoryRepository
{
    private readonly ApplicationDbContext _db;

    public DataCategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DataCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DataCategorys
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DataCategory>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DataCategorys
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DataCategory dataCategory, CancellationToken cancellationToken)
    {
        _db.DataCategorys.Add(dataCategory);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DataCategory dataCategory, CancellationToken cancellationToken)
    {
        _db.DataCategorys.Update(dataCategory);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DataCategory dataCategory, CancellationToken cancellationToken)
    {
        _db.DataCategorys.Remove(dataCategory);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
