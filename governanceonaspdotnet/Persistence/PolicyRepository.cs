
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class PolicyRepository : IPolicyRepository
{
    private readonly ApplicationDbContext _db;

    public PolicyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Policy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Policys
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Policy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Policys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Policy policy, CancellationToken cancellationToken)
    {
        _db.Policys.Add(policy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Policy policy, CancellationToken cancellationToken)
    {
        _db.Policys.Update(policy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Policy policy, CancellationToken cancellationToken)
    {
        _db.Policys.Remove(policy);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToOwnersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Persons
            .Where(person =>
                request.ChildIds.Contains(person.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    person =>
                        EF.Property<Guid?>(
                            person,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromOwnersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Persons
            .Where(person =>
                request.ChildIds.Contains(person.Id) &&
                EF.Property<Guid?>(
                    person,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    person =>
                        EF.Property<Guid?>(
                            person,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToRelatedRequirementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ComplianceRequirements
            .Where(complianceRequirement =>
                request.ChildIds.Contains(complianceRequirement.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    complianceRequirement =>
                        EF.Property<Guid?>(
                            complianceRequirement,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRelatedRequirementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ComplianceRequirements
            .Where(complianceRequirement =>
                request.ChildIds.Contains(complianceRequirement.Id) &&
                EF.Property<Guid?>(
                    complianceRequirement,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    complianceRequirement =>
                        EF.Property<Guid?>(
                            complianceRequirement,
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


    public async Task AddToProceduresAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Procedures
            .Where(procedure =>
                request.ChildIds.Contains(procedure.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    procedure =>
                        EF.Property<Guid?>(
                            procedure,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProceduresAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Procedures
            .Where(procedure =>
                request.ChildIds.Contains(procedure.Id) &&
                EF.Property<Guid?>(
                    procedure,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    procedure =>
                        EF.Property<Guid?>(
                            procedure,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToExceptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Exception_s
            .Where(exception_ =>
                request.ChildIds.Contains(exception_.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    exception_ =>
                        EF.Property<Guid?>(
                            exception_,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromExceptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Exception_s
            .Where(exception_ =>
                request.ChildIds.Contains(exception_.Id) &&
                EF.Property<Guid?>(
                    exception_,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    exception_ =>
                        EF.Property<Guid?>(
                            exception_,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToAttestationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Attestations
            .Where(attestation =>
                request.ChildIds.Contains(attestation.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    attestation =>
                        EF.Property<Guid?>(
                            attestation,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAttestationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Attestations
            .Where(attestation =>
                request.ChildIds.Contains(attestation.Id) &&
                EF.Property<Guid?>(
                    attestation,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    attestation =>
                        EF.Property<Guid?>(
                            attestation,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
