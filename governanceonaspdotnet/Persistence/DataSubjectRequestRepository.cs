
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class DataSubjectRequestRepository : IDataSubjectRequestRepository
{
    private readonly ApplicationDbContext _db;

    public DataSubjectRequestRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DataSubjectRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DataSubjectRequests
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DataSubjectRequest>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DataSubjectRequests
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DataSubjectRequest dataSubjectRequest, CancellationToken cancellationToken)
    {
        _db.DataSubjectRequests.Add(dataSubjectRequest);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DataSubjectRequest dataSubjectRequest, CancellationToken cancellationToken)
    {
        _db.DataSubjectRequests.Update(dataSubjectRequest);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DataSubjectRequest dataSubjectRequest, CancellationToken cancellationToken)
    {
        _db.DataSubjectRequests.Remove(dataSubjectRequest);
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

}
