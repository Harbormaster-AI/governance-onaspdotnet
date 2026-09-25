
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class ThirdPartyRepository : IThirdPartyRepository
{
    private readonly ApplicationDbContext _db;

    public ThirdPartyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ThirdParty?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ThirdPartys
            .Include(x => x.Organization)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ThirdParty>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ThirdPartys
            .AsNoTracking()
            .Include(x => x.Organization)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ThirdParty thirdParty, CancellationToken cancellationToken)
    {
        _db.ThirdPartys.Add(thirdParty);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ThirdParty thirdParty, CancellationToken cancellationToken)
    {
        _db.ThirdPartys.Update(thirdParty);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ThirdParty thirdParty, CancellationToken cancellationToken)
    {
        _db.ThirdPartys.Remove(thirdParty);
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


    public async Task AddToAssessmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ThirdPartyAssessments
            .Where(thirdPartyAssessment =>
                request.ChildIds.Contains(thirdPartyAssessment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    thirdPartyAssessment =>
                        EF.Property<Guid?>(
                            thirdPartyAssessment,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAssessmentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ThirdPartyAssessments
            .Where(thirdPartyAssessment =>
                request.ChildIds.Contains(thirdPartyAssessment.Id) &&
                EF.Property<Guid?>(
                    thirdPartyAssessment,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    thirdPartyAssessment =>
                        EF.Property<Guid?>(
                            thirdPartyAssessment,
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
