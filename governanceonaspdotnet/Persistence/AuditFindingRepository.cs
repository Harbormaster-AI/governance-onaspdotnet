
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class AuditFindingRepository : IAuditFindingRepository
{
    private readonly ApplicationDbContext _db;

    public AuditFindingRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AuditFinding?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.AuditFindings
            .Include(x => x.Engagement)
            .Include(x => x.Workpaper)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AuditFinding>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.AuditFindings
            .AsNoTracking()
            .Include(x => x.Engagement)
            .Include(x => x.Workpaper)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AuditFinding auditFinding, CancellationToken cancellationToken)
    {
        _db.AuditFindings.Add(auditFinding);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(AuditFinding auditFinding, CancellationToken cancellationToken)
    {
        _db.AuditFindings.Update(auditFinding);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(AuditFinding auditFinding, CancellationToken cancellationToken)
    {
        _db.AuditFindings.Remove(auditFinding);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToRelatedRisksAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Risks
            .Where(risk =>
                request.ChildIds.Contains(risk.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    risk =>
                        EF.Property<Guid?>(
                            risk,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRelatedRisksAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Risks
            .Where(risk =>
                request.ChildIds.Contains(risk.Id) &&
                EF.Property<Guid?>(
                    risk,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    risk =>
                        EF.Property<Guid?>(
                            risk,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToRelatedControlsAsync(
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

    public async Task RemoveFromRelatedControlsAsync(
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


    public async Task AddToIssuesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Issues
            .Where(issue =>
                request.ChildIds.Contains(issue.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    issue =>
                        EF.Property<Guid?>(
                            issue,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromIssuesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Issues
            .Where(issue =>
                request.ChildIds.Contains(issue.Id) &&
                EF.Property<Guid?>(
                    issue,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    issue =>
                        EF.Property<Guid?>(
                            issue,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
