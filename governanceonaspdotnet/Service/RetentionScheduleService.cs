
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IRetentionScheduleService {

    Task Create(RetentionSchedule model , CancellationToken cancellationToken);
    Task<bool> Update(RetentionSchedule model, CancellationToken cancellationToken);
    Task<RetentionSchedule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<RetentionSchedule>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToRepositories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRepositories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToExceptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromExceptions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDispositionReviews(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDispositionReviews(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class RetentionScheduleService : IRetentionScheduleService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IRetentionScheduleRepository _repository;
    private readonly ILogger<RetentionScheduleService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public RetentionScheduleService(
        ApplicationTelemetry telemetry,
        IRetentionScheduleRepository repository,
        ILogger<RetentionScheduleService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(RetentionSchedule model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "RetentionSchedule",
                "CreateRetentionSchedule",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(RetentionSchedule model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.RetentionPeriodMonths = model.RetentionPeriodMonths;
            existing.RetentionTrigger = model.RetentionTrigger;
            existing.DispositionAction = model.DispositionAction;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "RetentionSchedule",
                "UpdateRetentionSchedule",
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

    public Task<RetentionSchedule?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<RetentionSchedule>> GetAll(CancellationToken cancellationToken)
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
                "RetentionSchedule",
                "UpdateRetentionSchedule",
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


    public async Task<bool> AddToRepositories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RetentionSchedule",
                "AddToRepositories",
                () => _repository.AddToRepositoriesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRepositories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RetentionSchedule",
                "RemoveFromRepositories",
                () => _repository.RemoveFromRepositoriesAsync(request, cancellationToken));
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

    public async Task<bool> AddToRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RetentionSchedule",
                "AddToRecords",
                () => _repository.AddToRecordsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RetentionSchedule",
                "RemoveFromRecords",
                () => _repository.RemoveFromRecordsAsync(request, cancellationToken));
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

    public async Task<bool> AddToExceptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RetentionSchedule",
                "AddToExceptions",
                () => _repository.AddToExceptionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromExceptions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RetentionSchedule",
                "RemoveFromExceptions",
                () => _repository.RemoveFromExceptionsAsync(request, cancellationToken));
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

    public async Task<bool> AddToDispositionReviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RetentionSchedule",
                "AddToDispositionReviews",
                () => _repository.AddToDispositionReviewsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDispositionReviews(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RetentionSchedule",
                "RemoveFromDispositionReviews",
                () => _repository.RemoveFromDispositionReviewsAsync(request, cancellationToken));
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
