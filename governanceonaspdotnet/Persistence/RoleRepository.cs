using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _db;

    public RoleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Roles
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Roles
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Role role, CancellationToken cancellationToken)
    {
        _db.Roles.Add(role);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        _db.Roles.Update(role);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        _db.Roles.Remove(role);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
