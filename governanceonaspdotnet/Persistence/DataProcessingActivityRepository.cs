
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class DataProcessingActivityRepository : IDataProcessingActivityRepository
{
    private readonly ApplicationDbContext _db;

    public DataProcessingActivityRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<DataProcessingActivity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.DataProcessingActivitys
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<DataProcessingActivity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.DataProcessingActivitys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken)
    {
        _db.DataProcessingActivitys.Add(dataProcessingActivity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken)
    {
        _db.DataProcessingActivitys.Update(dataProcessingActivity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken)
    {
        _db.DataProcessingActivitys.Remove(dataProcessingActivity);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToSystemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.System_s
            .Where(system_ =>
                request.ChildIds.Contains(system_.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    system_ =>
                        EF.Property<Guid?>(
                            system_,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSystemsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.System_s
            .Where(system_ =>
                request.ChildIds.Contains(system_.Id) &&
                EF.Property<Guid?>(
                    system_,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    system_ =>
                        EF.Property<Guid?>(
                            system_,
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


    public async Task AddToPrivacyNoticesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PrivacyNotices
            .Where(privacyNotice =>
                request.ChildIds.Contains(privacyNotice.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    privacyNotice =>
                        EF.Property<Guid?>(
                            privacyNotice,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPrivacyNoticesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PrivacyNotices
            .Where(privacyNotice =>
                request.ChildIds.Contains(privacyNotice.Id) &&
                EF.Property<Guid?>(
                    privacyNotice,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    privacyNotice =>
                        EF.Property<Guid?>(
                            privacyNotice,
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


    public async Task AddToConsentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Consents
            .Where(consent =>
                request.ChildIds.Contains(consent.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    consent =>
                        EF.Property<Guid?>(
                            consent,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromConsentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Consents
            .Where(consent =>
                request.ChildIds.Contains(consent.Id) &&
                EF.Property<Guid?>(
                    consent,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    consent =>
                        EF.Property<Guid?>(
                            consent,
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
