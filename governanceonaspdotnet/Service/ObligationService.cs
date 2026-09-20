using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Service;

public interface IObligationService {

    Task Create(Obligation model , CancellationToken cancellationToken);
    Task<bool> Update(Obligation model, CancellationToken cancellationToken);
    Task<Obligation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Obligation>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRegulation(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRegulation(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToControls(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromControls(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ObligationService : IObligationService
{
    private readonly IObligationRepository _repository;
    private readonly ILogger<ObligationService> _logger;

    public ObligationService(
        IObligationRepository repository, ILogger<ObligationService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Obligation model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Obligation model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ReferenceNumber = model.ReferenceNumber;
            existing.DescriptionText = model.DescriptionText;
            existing.ObligationType = model.ObligationType;
            existing.ReviewFrequency = model.ReviewFrequency;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Obligation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Obligation>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignRegulation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRegulation(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToControls(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromControls(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
