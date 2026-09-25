
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IDispositionReviewRepository _repository;
    private readonly ILogger<DispositionReviewService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public DispositionReviewService(
        ApplicationTelemetry telemetry,
        IDispositionReviewRepository repository,
        ILogger<DispositionReviewService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(DispositionReview model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "DispositionReview",
                "CreateDispositionReview",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "DispositionReview",
                "UpdateDispositionReview",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "DispositionReview",
                "UpdateDispositionReview",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignRecord(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DispositionReview found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<Record_Service>().Get(childRequest, cancellationToken);
            parent.Record = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignRecord(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DispositionReview found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Record = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DispositionReview found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<RetentionScheduleService>().Get(childRequest, cancellationToken);
            parent.RetentionSchedule = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DispositionReview found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.RetentionSchedule = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }




}
