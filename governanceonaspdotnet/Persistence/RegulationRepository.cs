using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class RegulationRepository : IRegulationRepository
{
    private readonly ApplicationDbContext _db;

    public RegulationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Regulation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Regulations
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Regulation>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Regulations
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Regulation regulation, CancellationToken cancellationToken)
    {
        _db.Regulations.Add(regulation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Regulation regulation, CancellationToken cancellationToken)
    {
        _db.Regulations.Update(regulation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Regulation regulation, CancellationToken cancellationToken)
    {
        _db.Regulations.Remove(regulation);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
