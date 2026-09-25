
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class RiskRepository : IRiskRepository
{
    private readonly ApplicationDbContext _db;

    public RiskRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Risk?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Risks
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Risk>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Risks
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Risk risk, CancellationToken cancellationToken)
    {
        _db.Risks.Add(risk);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Risk risk, CancellationToken cancellationToken)
    {
        _db.Risks.Update(risk);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Risk risk, CancellationToken cancellationToken)
    {
        _db.Risks.Remove(risk);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToAssessmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RiskAssessments
            .Where(riskAssessment =>
                request.ChildIds.Contains(riskAssessment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    riskAssessment =>
                        EF.Property<Guid?>(
                            riskAssessment,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAssessmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RiskAssessments
            .Where(riskAssessment =>
                request.ChildIds.Contains(riskAssessment.Id) &&
                EF.Property<Guid?>(
                    riskAssessment,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    riskAssessment =>
                        EF.Property<Guid?>(
                            riskAssessment,
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
