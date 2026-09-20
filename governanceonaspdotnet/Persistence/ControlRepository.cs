using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ControlRepository : IControlRepository
{
    private readonly ApplicationDbContext _db;

    public ControlRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Control?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Controls
            .Include(x => x.Policy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Control>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Controls
            .AsNoTracking()
            .Include(x => x.Policy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Control control, CancellationToken cancellationToken)
    {
        _db.Controls.Add(control);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Control control, CancellationToken cancellationToken)
    {
        _db.Controls.Update(control);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Control control, CancellationToken cancellationToken)
    {
        _db.Controls.Remove(control);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
