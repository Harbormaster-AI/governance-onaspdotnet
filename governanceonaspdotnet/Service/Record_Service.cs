
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IRecord_Service {

    Task Create(Record_ model , CancellationToken cancellationToken);
    Task<bool> Update(Record_ model, CancellationToken cancellationToken);
    Task<Record_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Record_>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRepository(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRepository(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataSubjectRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataSubjectRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class Record_Service : IRecord_Service
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IRecord_Repository _repository;
    private readonly ILogger<Record_Service> _logger;
    private readonly IServiceResolver _serviceResolver;


    public Record_Service(
        ApplicationTelemetry telemetry,
        IRecord_Repository repository,
        ILogger<Record_Service> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Record_ model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Record_",
                "CreateRecord_",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Record_ model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Title = model.Title;
            existing.CreationDate = model.CreationDate;
            existing.RecordType = model.RecordType;
            existing.Classification = model.Classification;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "Record_",
                "UpdateRecord_",
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

    public Task<Record_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Record_>> GetAll(CancellationToken cancellationToken)
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
                "Record_",
                "UpdateRecord_",
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

    public async Task<bool> AssignRepository(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Record_ found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<RecordsRepositoryService>().Get(childRequest, cancellationToken);
            parent.Repository = child;
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

    public async Task<bool> UnassignRepository(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Record_ found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Repository = null;
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
            _logger.LogError("No Record_ found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No Record_ found using Id {ParentId}", request.ParentId);
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


    public async Task<bool> AddToProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Record_",
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
                "Record_",
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

    public async Task<bool> AddToDataCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Record_",
                "AddToDataCategories",
                () => _repository.AddToDataCategoriesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDataCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Record_",
                "RemoveFromDataCategories",
                () => _repository.RemoveFromDataCategoriesAsync(request, cancellationToken));
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

    public async Task<bool> AddToLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Record_",
                "AddToLegalHolds",
                () => _repository.AddToLegalHoldsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Record_",
                "RemoveFromLegalHolds",
                () => _repository.RemoveFromLegalHoldsAsync(request, cancellationToken));
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

    public async Task<bool> AddToDataSubjectRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Record_",
                "AddToDataSubjectRequests",
                () => _repository.AddToDataSubjectRequestsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDataSubjectRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Record_",
                "RemoveFromDataSubjectRequests",
                () => _repository.RemoveFromDataSubjectRequestsAsync(request, cancellationToken));
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
