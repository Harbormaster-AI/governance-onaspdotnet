
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class AuditEngagementRepository : IAuditEngagementRepository
{
    private readonly ApplicationDbContext _db;

    public AuditEngagementRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AuditEngagement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AuditEngagements
            .Include(x => x.AuditProgram)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AuditEngagement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AuditEngagements
            .AsNoTracking()
            .Include(x => x.AuditProgram)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken)
    {
        _db.AuditEngagements.Add(auditEngagement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken)
    {
        _db.AuditEngagements.Update(auditEngagement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AuditEngagement auditEngagement, CancellationToken cancellationToken)
    {
        _db.AuditEngagements.Remove(auditEngagement);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToBusinessUnitsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BusinessUnits
            .Where(businessUnit =>
                request.ChildIds.Contains(businessUnit.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    businessUnit =>
                        EF.Property<Guid?>(
                            businessUnit,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBusinessUnitsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BusinessUnits
            .Where(businessUnit =>
                request.ChildIds.Contains(businessUnit.Id) &&
                EF.Property<Guid?>(
                    businessUnit,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    businessUnit =>
                        EF.Property<Guid?>(
                            businessUnit,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToControlTestsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ControlTest_s
            .Where(controlTest_ =>
                request.ChildIds.Contains(controlTest_.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    controlTest_ =>
                        EF.Property<Guid?>(
                            controlTest_,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromControlTestsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ControlTest_s
            .Where(controlTest_ =>
                request.ChildIds.Contains(controlTest_.Id) &&
                EF.Property<Guid?>(
                    controlTest_,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    controlTest_ =>
                        EF.Property<Guid?>(
                            controlTest_,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToWorkpapersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AuditWorkpapers
            .Where(auditWorkpaper =>
                request.ChildIds.Contains(auditWorkpaper.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    auditWorkpaper =>
                        EF.Property<Guid?>(
                            auditWorkpaper,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromWorkpapersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AuditWorkpapers
            .Where(auditWorkpaper =>
                request.ChildIds.Contains(auditWorkpaper.Id) &&
                EF.Property<Guid?>(
                    auditWorkpaper,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    auditWorkpaper =>
                        EF.Property<Guid?>(
                            auditWorkpaper,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToFindingsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AuditFindings
            .Where(auditFinding =>
                request.ChildIds.Contains(auditFinding.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    auditFinding =>
                        EF.Property<Guid?>(
                            auditFinding,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromFindingsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AuditFindings
            .Where(auditFinding =>
                request.ChildIds.Contains(auditFinding.Id) &&
                EF.Property<Guid?>(
                    auditFinding,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    auditFinding =>
                        EF.Property<Guid?>(
                            auditFinding,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
