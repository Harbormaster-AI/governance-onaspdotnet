
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly ApplicationDbContext _db;

    public OrganizationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Organizations
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Organization>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Organizations
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Organization organization, CancellationToken cancellationToken)
    {
        _db.Organizations.Add(organization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Organization organization, CancellationToken cancellationToken)
    {
        _db.Organizations.Update(organization);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Organization organization, CancellationToken cancellationToken)
    {
        _db.Organizations.Remove(organization);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToGovernanceBodiesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GovernanceBodys
            .Where(governanceBody =>
                request.ChildIds.Contains(governanceBody.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    governanceBody =>
                        EF.Property<Guid?>(
                            governanceBody,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromGovernanceBodiesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.GovernanceBodys
            .Where(governanceBody =>
                request.ChildIds.Contains(governanceBody.Id) &&
                EF.Property<Guid?>(
                    governanceBody,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    governanceBody =>
                        EF.Property<Guid?>(
                            governanceBody,
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


    public async Task AddToRisksAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Risks
            .Where(risk =>
                request.ChildIds.Contains(risk.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    risk =>
                        EF.Property<Guid?>(
                            risk,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromRisksAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Risks
            .Where(risk =>
                request.ChildIds.Contains(risk.Id) &&
                EF.Property<Guid?>(
                    risk,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    risk =>
                        EF.Property<Guid?>(
                            risk,
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


    public async Task AddToRecordsRepositoriesAsync(
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

    public async Task RemoveFromRecordsRepositoriesAsync(
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


    public async Task AddToAuditProgramsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AuditPrograms
            .Where(auditProgram =>
                request.ChildIds.Contains(auditProgram.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    auditProgram =>
                        EF.Property<Guid?>(
                            auditProgram,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAuditProgramsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.AuditPrograms
            .Where(auditProgram =>
                request.ChildIds.Contains(auditProgram.Id) &&
                EF.Property<Guid?>(
                    auditProgram,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    auditProgram =>
                        EF.Property<Guid?>(
                            auditProgram,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToBusinessUnitsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BusinessUnits
            .Where(businessUnit =>
                request.ChildIds.Contains(businessUnit.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    businessUnit =>
                        EF.Property<Guid?>(
                            businessUnit,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBusinessUnitsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.BusinessUnits
            .Where(businessUnit =>
                request.ChildIds.Contains(businessUnit.Id) &&
                EF.Property<Guid?>(
                    businessUnit,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    businessUnit =>
                        EF.Property<Guid?>(
                            businessUnit,
                            "DataBreach_Id"),
                    (Guid?)null));
    }


    public async Task AddToMattersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Matters
            .Where(matter =>
                request.ChildIds.Contains(matter.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    matter =>
                        EF.Property<Guid?>(
                            matter,
                            "DataBreach_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromMattersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Matters
            .Where(matter =>
                request.ChildIds.Contains(matter.Id) &&
                EF.Property<Guid?>(
                    matter,
                    "DataBreach_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    matter =>
                        EF.Property<Guid?>(
                            matter,
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
