using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class RoleAssignmentRepository : IRoleAssignmentRepository
{
    private readonly ApplicationDbContext _db;

    public RoleAssignmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<RoleAssignment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.RoleAssignments
            .Include(x => x.Person)
            .Include(x => x.Role)
            .Include(x => x.GovernanceBody)
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<RoleAssignment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.RoleAssignments
            .AsNoTracking()
            .Include(x => x.Person)
            .Include(x => x.Role)
            .Include(x => x.GovernanceBody)
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RoleAssignment roleAssignment, CancellationToken cancellationToken)
    {
        _db.RoleAssignments.Add(roleAssignment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RoleAssignment roleAssignment, CancellationToken cancellationToken)
    {
        _db.RoleAssignments.Update(roleAssignment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(RoleAssignment roleAssignment, CancellationToken cancellationToken)
    {
        _db.RoleAssignments.Remove(roleAssignment);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
