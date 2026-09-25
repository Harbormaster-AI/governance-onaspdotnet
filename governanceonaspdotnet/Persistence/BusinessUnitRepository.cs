
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class BusinessUnitRepository : IBusinessUnitRepository
{
    private readonly ApplicationDbContext _db;

    public BusinessUnitRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BusinessUnit?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BusinessUnits
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BusinessUnit>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BusinessUnits
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BusinessUnit businessUnit, CancellationToken cancellationToken)
    {
        _db.BusinessUnits.Add(businessUnit);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BusinessUnit businessUnit, CancellationToken cancellationToken)
    {
        _db.BusinessUnits.Update(businessUnit);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BusinessUnit businessUnit, CancellationToken cancellationToken)
    {
        _db.BusinessUnits.Remove(businessUnit);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToAuditsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AuditEngagements
            .Where(auditEngagement =>
                request.ChildIds.Contains(auditEngagement.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    auditEngagement =>
                        EF.Property<Guid?>(
                            auditEngagement,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAuditsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AuditEngagements
            .Where(auditEngagement =>
                request.ChildIds.Contains(auditEngagement.Id) &&
                EF.Property<Guid?>(
                    auditEngagement,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    auditEngagement =>
                        EF.Property<Guid?>(
                            auditEngagement,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
