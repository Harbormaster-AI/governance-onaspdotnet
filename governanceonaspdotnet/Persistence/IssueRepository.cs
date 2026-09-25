
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class IssueRepository : IIssueRepository
{
    private readonly ApplicationDbContext _db;

    public IssueRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Issue?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Issues
            .Include(x => x.Risk)
            .Include(x => x.Finding)
            .Include(x => x.Control)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Issue>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Issues
            .AsNoTracking()
            .Include(x => x.Risk)
            .Include(x => x.Finding)
            .Include(x => x.Control)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Issue issue, CancellationToken cancellationToken)
    {
        _db.Issues.Add(issue);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Issue issue, CancellationToken cancellationToken)
    {
        _db.Issues.Update(issue);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Issue issue, CancellationToken cancellationToken)
    {
        _db.Issues.Remove(issue);
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

}
