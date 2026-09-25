
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ControlRepository : IControlRepository
{
    private readonly ApplicationDbContext _db;

    public ControlRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Control?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Controls
            .Include(x => x.Policy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Control>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Controls
            .AsNoTracking()
            .Include(x => x.Policy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Control control, CancellationToken cancellationToken)
    {
        _db.Controls.Add(control);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Control control, CancellationToken cancellationToken)
    {
        _db.Controls.Update(control);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Control control, CancellationToken cancellationToken)
    {
        _db.Controls.Remove(control);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToEvidenceAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Evidences
            .Where(evidence =>
                request.ChildIds.Contains(evidence.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    evidence =>
                        EF.Property<Guid?>(
                            evidence,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromEvidenceAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Evidences
            .Where(evidence =>
                request.ChildIds.Contains(evidence.Id) &&
                EF.Property<Guid?>(
                    evidence,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    evidence =>
                        EF.Property<Guid?>(
                            evidence,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToRisksAsync(
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

    public async Task RemoveFromRisksAsync(
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
