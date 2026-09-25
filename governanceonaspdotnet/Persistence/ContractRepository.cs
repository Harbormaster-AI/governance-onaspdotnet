
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ContractRepository : IContractRepository
{
    private readonly ApplicationDbContext _db;

    public ContractRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Contract?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Contracts
            .Include(x => x.ThirdParty)
            .Include(x => x.Matter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Contract>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Contracts
            .AsNoTracking()
            .Include(x => x.ThirdParty)
            .Include(x => x.Matter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Contract contract, CancellationToken cancellationToken)
    {
        _db.Contracts.Add(contract);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Contract contract, CancellationToken cancellationToken)
    {
        _db.Contracts.Update(contract);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Contract contract, CancellationToken cancellationToken)
    {
        _db.Contracts.Remove(contract);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToObligationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Obligations
            .Where(obligation =>
                request.ChildIds.Contains(obligation.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    obligation =>
                        EF.Property<Guid?>(
                            obligation,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromObligationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Obligations
            .Where(obligation =>
                request.ChildIds.Contains(obligation.Id) &&
                EF.Property<Guid?>(
                    obligation,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    obligation =>
                        EF.Property<Guid?>(
                            obligation,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToDataProcessingActivitiesAsync(
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

    public async Task RemoveFromDataProcessingActivitiesAsync(
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

}
