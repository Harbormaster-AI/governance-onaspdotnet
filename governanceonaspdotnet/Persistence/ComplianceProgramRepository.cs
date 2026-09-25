
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ComplianceProgramRepository : IComplianceProgramRepository
{
    private readonly ApplicationDbContext _db;

    public ComplianceProgramRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ComplianceProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CompliancePrograms
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ComplianceProgram>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CompliancePrograms
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken)
    {
        _db.CompliancePrograms.Add(complianceProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken)
    {
        _db.CompliancePrograms.Update(complianceProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken)
    {
        _db.CompliancePrograms.Remove(complianceProgram);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToRequirementsAsync(
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

    public async Task RemoveFromRequirementsAsync(
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


    public async Task AddToRegulationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Regulations
            .Where(regulation =>
                request.ChildIds.Contains(regulation.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    regulation =>
                        EF.Property<Guid?>(
                            regulation,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRegulationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Regulations
            .Where(regulation =>
                request.ChildIds.Contains(regulation.Id) &&
                EF.Property<Guid?>(
                    regulation,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    regulation =>
                        EF.Property<Guid?>(
                            regulation,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
