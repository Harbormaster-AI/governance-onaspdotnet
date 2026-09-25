
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IDataCategoryService {

    Task Create(DataCategory model , CancellationToken cancellationToken);
    Task<bool> Update(DataCategory model, CancellationToken cancellationToken);
    Task<DataCategory?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataCategory>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DataCategoryService : IDataCategoryService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IDataCategoryRepository _repository;
    private readonly ILogger<DataCategoryService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public DataCategoryService(
        ApplicationTelemetry telemetry,
        IDataCategoryRepository repository,
        ILogger<DataCategoryService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(DataCategory model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "DataCategory",
                "CreateDataCategory",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(DataCategory model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.Classification = model.Classification;

            await _telemetry.Execute(
                "DataCategory",
                "UpdateDataCategory",
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

    public Task<DataCategory?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DataCategory>> GetAll(CancellationToken cancellationToken)
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
                "DataCategory",
                "UpdateDataCategory",
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


    public async Task<bool> AddToProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataCategory",
                "AddToProcessingActivities",
                () => _repository.AddToProcessingActivitiesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataCategory",
                "RemoveFromProcessingActivities",
                () => _repository.RemoveFromProcessingActivitiesAsync(request, cancellationToken));
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
                "DataCategory",
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
                "DataCategory",
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

    public async Task<bool> AddToDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataCategory",
                "AddToDataBreaches",
                () => _repository.AddToDataBreachesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataCategory",
                "RemoveFromDataBreaches",
                () => _repository.RemoveFromDataBreachesAsync(request, cancellationToken));
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
