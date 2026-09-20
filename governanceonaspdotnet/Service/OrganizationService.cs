using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Service;

public interface IOrganizationService {

    Task Create(Organization model , CancellationToken cancellationToken);
    Task<bool> Update(Organization model, CancellationToken cancellationToken);
    Task<Organization?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Organization>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToGovernanceBodies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromGovernanceBodies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRisks(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRisks(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToThirdParties(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromThirdParties(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRecordsRepositories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRecordsRepositories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCompliancePrograms(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCompliancePrograms(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAuditPrograms(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAuditPrograms(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToMatters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromMatters(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _repository;
    private readonly ILogger<OrganizationService> _logger;

    public OrganizationService(
        IOrganizationRepository repository, ILogger<OrganizationService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Organization model, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(Organization model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LegalName = model.LegalName;
            existing.Jurisdiction = model.Jurisdiction;
            existing.IndustrySector = model.IndustrySector;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Organization?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Organization>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }


    public async Task<bool> AddToGovernanceBodies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromGovernanceBodies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToRisks(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromRisks(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToThirdParties(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromThirdParties(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToRecordsRepositories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromRecordsRepositories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDataProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDataProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToCompliancePrograms(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCompliancePrograms(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToAuditPrograms(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAuditPrograms(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToMatters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromMatters(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
