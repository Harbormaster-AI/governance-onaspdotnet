
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _db;

    public PersonRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Persons
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Person>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Persons
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Person person, CancellationToken cancellationToken)
    {
        _db.Persons.Add(person);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Person person, CancellationToken cancellationToken)
    {
        _db.Persons.Update(person);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Person person, CancellationToken cancellationToken)
    {
        _db.Persons.Remove(person);
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


    public async Task AddToOwnedPoliciesAsync(
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

    public async Task RemoveFromOwnedPoliciesAsync(
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


    public async Task AddToCorrectiveActionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CorrectiveActions
            .Where(correctiveAction =>
                request.ChildIds.Contains(correctiveAction.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    correctiveAction =>
                        EF.Property<Guid?>(
                            correctiveAction,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCorrectiveActionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CorrectiveActions
            .Where(correctiveAction =>
                request.ChildIds.Contains(correctiveAction.Id) &&
                EF.Property<Guid?>(
                    correctiveAction,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    correctiveAction =>
                        EF.Property<Guid?>(
                            correctiveAction,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
