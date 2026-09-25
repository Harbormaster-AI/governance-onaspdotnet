
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class GovernanceBodyRepository : IGovernanceBodyRepository
{
    private readonly ApplicationDbContext _db;

    public GovernanceBodyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<GovernanceBody?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.GovernanceBodys
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<GovernanceBody>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.GovernanceBodys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(GovernanceBody governanceBody, CancellationToken cancellationToken)
    {
        _db.GovernanceBodys.Add(governanceBody);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(GovernanceBody governanceBody, CancellationToken cancellationToken)
    {
        _db.GovernanceBodys.Update(governanceBody);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(GovernanceBody governanceBody, CancellationToken cancellationToken)
    {
        _db.GovernanceBodys.Remove(governanceBody);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToRoleAssignmentsAsync(
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

    public async Task RemoveFromRoleAssignmentsAsync(
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


    public async Task AddToPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Policys
            .Where(policy =>
                request.ChildIds.Contains(policy.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    policy =>
                        EF.Property<Guid?>(
                            policy,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Policys
            .Where(policy =>
                request.ChildIds.Contains(policy.Id) &&
                EF.Property<Guid?>(
                    policy,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    policy =>
                        EF.Property<Guid?>(
                            policy,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
