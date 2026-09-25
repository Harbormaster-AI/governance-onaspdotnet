
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class MatterRepository : IMatterRepository
{
    private readonly ApplicationDbContext _db;

    public MatterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Matter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Matters
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Matter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Matters
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Matter matter, CancellationToken cancellationToken)
    {
        _db.Matters.Add(matter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Matter matter, CancellationToken cancellationToken)
    {
        _db.Matters.Update(matter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Matter matter, CancellationToken cancellationToken)
    {
        _db.Matters.Remove(matter);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToContractsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Contracts
            .Where(contract =>
                request.ChildIds.Contains(contract.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    contract =>
                        EF.Property<Guid?>(
                            contract,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromContractsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Contracts
            .Where(contract =>
                request.ChildIds.Contains(contract.Id) &&
                EF.Property<Guid?>(
                    contract,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    contract =>
                        EF.Property<Guid?>(
                            contract,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
