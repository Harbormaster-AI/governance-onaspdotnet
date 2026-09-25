
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ObligationRepository : IObligationRepository
{
    private readonly ApplicationDbContext _db;

    public ObligationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Obligation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Obligations
            .Include(x => x.Regulation)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Obligation>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Obligations
            .AsNoTracking()
            .Include(x => x.Regulation)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Obligation obligation, CancellationToken cancellationToken)
    {
        _db.Obligations.Add(obligation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Obligation obligation, CancellationToken cancellationToken)
    {
        _db.Obligations.Update(obligation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Obligation obligation, CancellationToken cancellationToken)
    {
        _db.Obligations.Remove(obligation);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToControlsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Controls
            .Where(control =>
                request.ChildIds.Contains(control.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    control =>
                        EF.Property<Guid?>(
                            control,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromControlsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Controls
            .Where(control =>
                request.ChildIds.Contains(control.Id) &&
                EF.Property<Guid?>(
                    control,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    control =>
                        EF.Property<Guid?>(
                            control,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Policys
            .Where(policy =>
                request.ChildIds.Contains(policy.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    policy =>
                        EF.Property<Guid?>(
                            policy,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Policys
            .Where(policy =>
                request.ChildIds.Contains(policy.Id) &&
                EF.Property<Guid?>(
                    policy,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    policy =>
                        EF.Property<Guid?>(
                            policy,
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
