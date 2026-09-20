using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Service;

public interface IException_Service {

    Task Create(Exception_ model , CancellationToken cancellationToken);
    Task<bool> Update(Exception_ model, CancellationToken cancellationToken);
    Task<Exception_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Exception_>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignControl(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignControl(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRisk(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRisk(AssociationRequest request, CancellationToken cancellationToken);


}

public class Exception_Service : IException_Service
{
    private readonly IException_Repository _repository;
    private readonly ILogger<Exception_Service> _logger;

    public Exception_Service(
        IException_Repository repository, ILogger<Exception_Service> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Exception_ model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Exception_ model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Title = model.Title;
            existing.Justification = model.Justification;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.ExceptionType = model.ExceptionType;
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

    public Task<Exception_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Exception_>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignControl(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignControl(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRisk(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRisk(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
