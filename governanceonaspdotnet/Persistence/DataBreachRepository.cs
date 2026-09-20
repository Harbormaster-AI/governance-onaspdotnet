using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class DataBreachRepository : IDataBreachRepository
{
    private readonly ApplicationDbContext _db;

    public DataBreachRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DataBreach?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DataBreachs
            .Include(x => x.Organization)
            .Include(x => x.Matter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DataBreach>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DataBreachs
            .AsNoTracking()
            .Include(x => x.Organization)
            .Include(x => x.Matter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DataBreach dataBreach, CancellationToken cancellationToken)
    {
        _db.DataBreachs.Add(dataBreach);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DataBreach dataBreach, CancellationToken cancellationToken)
    {
        _db.DataBreachs.Update(dataBreach);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DataBreach dataBreach, CancellationToken cancellationToken)
    {
        _db.DataBreachs.Remove(dataBreach);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
