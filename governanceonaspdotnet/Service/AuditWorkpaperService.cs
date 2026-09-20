using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Service;

public interface IAuditWorkpaperService {

    Task Create(AuditWorkpaper model , CancellationToken cancellationToken);
    Task<bool> Update(AuditWorkpaper model, CancellationToken cancellationToken);
    Task<AuditWorkpaper?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditWorkpaper>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEngagement(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEngagement(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToFindings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFindings(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AuditWorkpaperService : IAuditWorkpaperService
{
    private readonly IAuditWorkpaperRepository _repository;
    private readonly ILogger<AuditWorkpaperService> _logger;

    public AuditWorkpaperService(
        IAuditWorkpaperRepository repository, ILogger<AuditWorkpaperService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(AuditWorkpaper model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(AuditWorkpaper model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.WorkpaperRef = model.WorkpaperRef;
            existing.Subject = model.Subject;
            existing.WorkpaperUrl = model.WorkpaperUrl;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<AuditWorkpaper?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AuditWorkpaper>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignEngagement(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignEngagement(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToFindings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromFindings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
