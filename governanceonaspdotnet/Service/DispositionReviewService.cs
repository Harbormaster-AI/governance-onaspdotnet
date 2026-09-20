using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Service;

public interface IDispositionReviewService {

    Task Create(DispositionReview model , CancellationToken cancellationToken);
    Task<bool> Update(DispositionReview model, CancellationToken cancellationToken);
    Task<DispositionReview?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DispositionReview>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRecord(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRecord(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken);


}

public class DispositionReviewService : IDispositionReviewService
{
    private readonly IDispositionReviewRepository _repository;
    private readonly ILogger<DispositionReviewService> _logger;

    public DispositionReviewService(
        IDispositionReviewRepository repository, ILogger<DispositionReviewService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(DispositionReview model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(DispositionReview model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ReviewDate = model.ReviewDate;
            existing.Reviewer = model.Reviewer;
            existing.Notes = model.Notes;
            existing.Outcome = model.Outcome;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<DispositionReview?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DispositionReview>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignRecord(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRecord(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
