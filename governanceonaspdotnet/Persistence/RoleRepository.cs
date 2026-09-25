
using governanceonaspdotnet.Contracts;
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


    public async Task AddToAssignmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RoleAssignments
            .Where(roleAssignment =>
                request.ChildIds.Contains(roleAssignment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    roleAssignment =>
                        EF.Property<Guid?>(
                            roleAssignment,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAssignmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RoleAssignments
            .Where(roleAssignment =>
                request.ChildIds.Contains(roleAssignment.Id) &&
                EF.Property<Guid?>(
                    roleAssignment,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    roleAssignment =>
                        EF.Property<Guid?>(
                            roleAssignment,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
