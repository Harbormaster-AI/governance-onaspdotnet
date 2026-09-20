using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Service;

public interface IEvidenceService {

    Task Create(Evidence model , CancellationToken cancellationToken);
    Task<bool> Update(Evidence model, CancellationToken cancellationToken);
    Task<Evidence?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Evidence>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignControlTest(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignControlTest(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignControl(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignControl(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignObligation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignObligation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWorkpaper(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWorkpaper(AssociationRequest request, CancellationToken cancellationToken);


}

public class EvidenceService : IEvidenceService
{
    private readonly IEvidenceRepository _repository;
    private readonly ILogger<EvidenceService> _logger;

    public EvidenceService(
        IEvidenceRepository repository, ILogger<EvidenceService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Evidence model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Evidence model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Title = model.Title;
            existing.LocationUrl = model.LocationUrl;
            existing.ReceivedDate = model.ReceivedDate;
            existing.EvidenceType = model.EvidenceType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Evidence?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Evidence>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignControlTest(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignControlTest(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignControl(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignControl(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignObligation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignObligation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignWorkpaper(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignWorkpaper(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
