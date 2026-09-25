
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class RecordsRepositoryRepository : IRecordsRepositoryRepository
{
    private readonly ApplicationDbContext _db;

    public RecordsRepositoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<RecordsRepository?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.RecordsRepositorys
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<RecordsRepository>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.RecordsRepositorys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken)
    {
        _db.RecordsRepositorys.Add(recordsRepository);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken)
    {
        _db.RecordsRepositorys.Update(recordsRepository);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken)
    {
        _db.RecordsRepositorys.Remove(recordsRepository);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToRetentionSchedulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RetentionSchedules
            .Where(retentionSchedule =>
                request.ChildIds.Contains(retentionSchedule.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    retentionSchedule =>
                        EF.Property<Guid?>(
                            retentionSchedule,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRetentionSchedulesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.RetentionSchedules
            .Where(retentionSchedule =>
                request.ChildIds.Contains(retentionSchedule.Id) &&
                EF.Property<Guid?>(
                    retentionSchedule,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    retentionSchedule =>
                        EF.Property<Guid?>(
                            retentionSchedule,
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

}
