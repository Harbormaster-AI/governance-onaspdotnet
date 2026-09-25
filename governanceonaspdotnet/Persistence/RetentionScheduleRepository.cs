
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class RetentionScheduleRepository : IRetentionScheduleRepository
{
    private readonly ApplicationDbContext _db;

    public RetentionScheduleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<RetentionSchedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.RetentionSchedules
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<RetentionSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.RetentionSchedules
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RetentionSchedule retentionSchedule, CancellationToken cancellationToken)
    {
        _db.RetentionSchedules.Add(retentionSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RetentionSchedule retentionSchedule, CancellationToken cancellationToken)
    {
        _db.RetentionSchedules.Update(retentionSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(RetentionSchedule retentionSchedule, CancellationToken cancellationToken)
    {
        _db.RetentionSchedules.Remove(retentionSchedule);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToRepositoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RecordsRepositorys
            .Where(recordsRepository =>
                request.ChildIds.Contains(recordsRepository.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    recordsRepository =>
                        EF.Property<Guid?>(
                            recordsRepository,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRepositoriesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RecordsRepositorys
            .Where(recordsRepository =>
                request.ChildIds.Contains(recordsRepository.Id) &&
                EF.Property<Guid?>(
                    recordsRepository,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    recordsRepository =>
                        EF.Property<Guid?>(
                            recordsRepository,
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


    public async Task AddToExceptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Exception_s
            .Where(exception_ =>
                request.ChildIds.Contains(exception_.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    exception_ =>
                        EF.Property<Guid?>(
                            exception_,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromExceptionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Exception_s
            .Where(exception_ =>
                request.ChildIds.Contains(exception_.Id) &&
                EF.Property<Guid?>(
                    exception_,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    exception_ =>
                        EF.Property<Guid?>(
                            exception_,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToDispositionReviewsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DispositionReviews
            .Where(dispositionReview =>
                request.ChildIds.Contains(dispositionReview.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dispositionReview =>
                        EF.Property<Guid?>(
                            dispositionReview,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDispositionReviewsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.DispositionReviews
            .Where(dispositionReview =>
                request.ChildIds.Contains(dispositionReview.Id) &&
                EF.Property<Guid?>(
                    dispositionReview,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    dispositionReview =>
                        EF.Property<Guid?>(
                            dispositionReview,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
