
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ThirdPartyAssessmentRepository : IThirdPartyAssessmentRepository
{
    private readonly ApplicationDbContext _db;

    public ThirdPartyAssessmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ThirdPartyAssessment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ThirdPartyAssessments
            .Include(x => x.ThirdParty)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ThirdPartyAssessment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ThirdPartyAssessments
            .AsNoTracking()
            .Include(x => x.ThirdParty)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ThirdPartyAssessment thirdPartyAssessment, CancellationToken cancellationToken)
    {
        _db.ThirdPartyAssessments.Add(thirdPartyAssessment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ThirdPartyAssessment thirdPartyAssessment, CancellationToken cancellationToken)
    {
        _db.ThirdPartyAssessments.Update(thirdPartyAssessment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ThirdPartyAssessment thirdPartyAssessment, CancellationToken cancellationToken)
    {
        _db.ThirdPartyAssessments.Remove(thirdPartyAssessment);
        await _db.SaveChangesAsync(cancellationToken);
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
