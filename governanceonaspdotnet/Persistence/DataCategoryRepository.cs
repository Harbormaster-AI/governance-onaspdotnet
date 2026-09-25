
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class DataCategoryRepository : IDataCategoryRepository
{
    private readonly ApplicationDbContext _db;

    public DataCategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DataCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DataCategorys
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DataCategory>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DataCategorys
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DataCategory dataCategory, CancellationToken cancellationToken)
    {
        _db.DataCategorys.Add(dataCategory);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DataCategory dataCategory, CancellationToken cancellationToken)
    {
        _db.DataCategorys.Update(dataCategory);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DataCategory dataCategory, CancellationToken cancellationToken)
    {
        _db.DataCategorys.Remove(dataCategory);
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


    public async Task AddToRecordsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Record_s
            .Where(record_ =>
                request.ChildIds.Contains(record_.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    record_ =>
                        EF.Property<Guid?>(
                            record_,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRecordsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Record_s
            .Where(record_ =>
                request.ChildIds.Contains(record_.Id) &&
                EF.Property<Guid?>(
                    record_,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    record_ =>
                        EF.Property<Guid?>(
                            record_,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToDataBreachesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DataBreachs
            .Where(dataBreach =>
                request.ChildIds.Contains(dataBreach.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dataBreach =>
                        EF.Property<Guid?>(
                            dataBreach,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDataBreachesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DataBreachs
            .Where(dataBreach =>
                request.ChildIds.Contains(dataBreach.Id) &&
                EF.Property<Guid?>(
                    dataBreach,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dataBreach =>
                        EF.Property<Guid?>(
                            dataBreach,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
