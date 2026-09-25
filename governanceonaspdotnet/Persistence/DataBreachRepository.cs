
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class DataBreachRepository : IDataBreachRepository
{
    private readonly ApplicationDbContext _db;

    public DataBreachRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DataBreach?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DataBreachs
            .Include(x => x.Organization)
            .Include(x => x.Matter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DataBreach>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DataBreachs
            .AsNoTracking()
            .Include(x => x.Organization)
            .Include(x => x.Matter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DataBreach dataBreach, CancellationToken cancellationToken)
    {
        _db.DataBreachs.Add(dataBreach);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DataBreach dataBreach, CancellationToken cancellationToken)
    {
        _db.DataBreachs.Update(dataBreach);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DataBreach dataBreach, CancellationToken cancellationToken)
    {
        _db.DataBreachs.Remove(dataBreach);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToProcessingActivitiesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DataProcessingActivitys
            .Where(dataProcessingActivity =>
                request.ChildIds.Contains(dataProcessingActivity.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dataProcessingActivity =>
                        EF.Property<Guid?>(
                            dataProcessingActivity,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProcessingActivitiesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DataProcessingActivitys
            .Where(dataProcessingActivity =>
                request.ChildIds.Contains(dataProcessingActivity.Id) &&
                EF.Property<Guid?>(
                    dataProcessingActivity,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dataProcessingActivity =>
                        EF.Property<Guid?>(
                            dataProcessingActivity,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToDataCategoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DataCategorys
            .Where(dataCategory =>
                request.ChildIds.Contains(dataCategory.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dataCategory =>
                        EF.Property<Guid?>(
                            dataCategory,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDataCategoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DataCategorys
            .Where(dataCategory =>
                request.ChildIds.Contains(dataCategory.Id) &&
                EF.Property<Guid?>(
                    dataCategory,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dataCategory =>
                        EF.Property<Guid?>(
                            dataCategory,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToThirdPartiesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ThirdPartys
            .Where(thirdParty =>
                request.ChildIds.Contains(thirdParty.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    thirdParty =>
                        EF.Property<Guid?>(
                            thirdParty,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromThirdPartiesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ThirdPartys
            .Where(thirdParty =>
                request.ChildIds.Contains(thirdParty.Id) &&
                EF.Property<Guid?>(
                    thirdParty,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    thirdParty =>
                        EF.Property<Guid?>(
                            thirdParty,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
