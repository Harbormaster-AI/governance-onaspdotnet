using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Service;

public interface IRoleAssignmentService {

    Task Create(RoleAssignment model , CancellationToken cancellationToken);
    Task<bool> Update(RoleAssignment model, CancellationToken cancellationToken);
    Task<RoleAssignment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<RoleAssignment>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPerson(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPerson(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRole(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRole(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignGovernanceBody(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignGovernanceBody(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);


}

public class RoleAssignmentService : IRoleAssignmentService
{
    private readonly IRoleAssignmentRepository _repository;
    private readonly ILogger<RoleAssignmentService> _logger;

    public RoleAssignmentService(
        IRoleAssignmentRepository repository, ILogger<RoleAssignmentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(RoleAssignment model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(RoleAssignment model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.EffectiveFrom = model.EffectiveFrom;
            existing.EffectiveTo = model.EffectiveTo;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<RoleAssignment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<RoleAssignment>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPerson(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPerson(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRole(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRole(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignGovernanceBody(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignGovernanceBody(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
