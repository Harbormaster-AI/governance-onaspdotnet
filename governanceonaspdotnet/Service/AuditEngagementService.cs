using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Service;

public interface IAuditEngagementService {

    Task Create(AuditEngagement model , CancellationToken cancellationToken);
    Task<bool> Update(AuditEngagement model, CancellationToken cancellationToken);
    Task<AuditEngagement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditEngagement>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAuditProgram(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAuditProgram(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToControlTests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromControlTests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWorkpapers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWorkpapers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToFindings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFindings(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AuditEngagementService : IAuditEngagementService
{
    private readonly IAuditEngagementRepository _repository;
    private readonly ILogger<AuditEngagementService> _logger;

    public AuditEngagementService(
        IAuditEngagementRepository repository, ILogger<AuditEngagementService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AuditEngagement model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(AuditEngagement model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Title = model.Title;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<AuditEngagement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AuditEngagement>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignAuditProgram(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAuditProgram(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToControlTests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromControlTests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToWorkpapers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromWorkpapers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToFindings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromFindings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
