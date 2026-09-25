
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ComplianceRequirementRepository : IComplianceRequirementRepository
{
    private readonly ApplicationDbContext _db;

    public ComplianceRequirementRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ComplianceRequirement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ComplianceRequirements
            .Include(x => x.ComplianceProgram)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ComplianceRequirement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ComplianceRequirements
            .AsNoTracking()
            .Include(x => x.ComplianceProgram)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken)
    {
        _db.ComplianceRequirements.Add(complianceRequirement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken)
    {
        _db.ComplianceRequirements.Update(complianceRequirement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken)
    {
        _db.ComplianceRequirements.Remove(complianceRequirement);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToControlsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Controls
            .Where(control =>
                request.ChildIds.Contains(control.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    control =>
                        EF.Property<Guid?>(
                            control,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromControlsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Controls
            .Where(control =>
                request.ChildIds.Contains(control.Id) &&
                EF.Property<Guid?>(
                    control,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    control =>
                        EF.Property<Guid?>(
                            control,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToObligationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Obligations
            .Where(obligation =>
                request.ChildIds.Contains(obligation.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    obligation =>
                        EF.Property<Guid?>(
                            obligation,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromObligationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Obligations
            .Where(obligation =>
                request.ChildIds.Contains(obligation.Id) &&
                EF.Property<Guid?>(
                    obligation,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    obligation =>
                        EF.Property<Guid?>(
                            obligation,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
