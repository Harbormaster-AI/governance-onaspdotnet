
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IDataSubjectRequestService {

    Task Create(DataSubjectRequest model , CancellationToken cancellationToken);
    Task<bool> Update(DataSubjectRequest model, CancellationToken cancellationToken);
    Task<DataSubjectRequest?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataSubjectRequest>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DataSubjectRequestService : IDataSubjectRequestService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IDataSubjectRequestRepository _repository;
    private readonly ILogger<DataSubjectRequestService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public DataSubjectRequestService(
        ApplicationTelemetry telemetry,
        IDataSubjectRequestRepository repository,
        ILogger<DataSubjectRequestService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(DataSubjectRequest model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "DataSubjectRequest",
                "CreateDataSubjectRequest",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(DataSubjectRequest model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ReceivedDate = model.ReceivedDate;
            existing.DueDate = model.DueDate;
            existing.RequesterCountry = model.RequesterCountry;
            existing.RequestType = model.RequestType;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "DataSubjectRequest",
                "UpdateDataSubjectRequest",
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

    public Task<DataSubjectRequest?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DataSubjectRequest>> GetAll(CancellationToken cancellationToken)
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
                "DataSubjectRequest",
                "UpdateDataSubjectRequest",
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

    public async Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DataSubjectRequest found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<OrganizationService>().Get(childRequest, cancellationToken);
            parent.Organization = child;
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

    public async Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No DataSubjectRequest found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Organization = null;
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
                "DataSubjectRequest",
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
                "DataSubjectRequest",
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
                "DataSubjectRequest",
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
                "DataSubjectRequest",
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



}
