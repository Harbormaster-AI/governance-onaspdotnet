
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class RegulationRepository : IRegulationRepository
{
    private readonly ApplicationDbContext _db;

    public RegulationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Regulation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Regulations
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Regulation>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Regulations
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Regulation regulation, CancellationToken cancellationToken)
    {
        _db.Regulations.Add(regulation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Regulation regulation, CancellationToken cancellationToken)
    {
        _db.Regulations.Update(regulation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Regulation regulation, CancellationToken cancellationToken)
    {
        _db.Regulations.Remove(regulation);
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


    public async Task AddToComplianceProgramsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CompliancePrograms
            .Where(complianceProgram =>
                request.ChildIds.Contains(complianceProgram.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    complianceProgram =>
                        EF.Property<Guid?>(
                            complianceProgram,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromComplianceProgramsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.CompliancePrograms
            .Where(complianceProgram =>
                request.ChildIds.Contains(complianceProgram.Id) &&
                EF.Property<Guid?>(
                    complianceProgram,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    complianceProgram =>
                        EF.Property<Guid?>(
                            complianceProgram,
                            "DataBreach_Id"),
                    (Guid?)null));
    }

}
