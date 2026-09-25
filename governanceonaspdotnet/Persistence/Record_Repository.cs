
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class Record_Repository : IRecord_Repository
{
    private readonly ApplicationDbContext _db;

    public Record_Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Record_?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Record_s
            .Include(x => x.Repository)
            .Include(x => x.RetentionSchedule)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Record_>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Record_s
            .AsNoTracking()
            .Include(x => x.Repository)
            .Include(x => x.RetentionSchedule)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Record_ record_, CancellationToken cancellationToken)
    {
        _db.Record_s.Add(record_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Record_ record_, CancellationToken cancellationToken)
    {
        _db.Record_s.Update(record_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Record_ record_, CancellationToken cancellationToken)
    {
        _db.Record_s.Remove(record_);
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


    public async Task AddToLegalHoldsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LegalHolds
            .Where(legalHold =>
                request.ChildIds.Contains(legalHold.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    legalHold =>
                        EF.Property<Guid?>(
                            legalHold,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromLegalHoldsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.LegalHolds
            .Where(legalHold =>
                request.ChildIds.Contains(legalHold.Id) &&
                EF.Property<Guid?>(
                    legalHold,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    legalHold =>
                        EF.Property<Guid?>(
                            legalHold,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToDataSubjectRequestsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DataSubjectRequests
            .Where(dataSubjectRequest =>
                request.ChildIds.Contains(dataSubjectRequest.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dataSubjectRequest =>
                        EF.Property<Guid?>(
                            dataSubjectRequest,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDataSubjectRequestsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DataSubjectRequests
            .Where(dataSubjectRequest =>
                request.ChildIds.Contains(dataSubjectRequest.Id) &&
                EF.Property<Guid?>(
                    dataSubjectRequest,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dataSubjectRequest =>
                        EF.Property<Guid?>(
                            dataSubjectRequest,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
