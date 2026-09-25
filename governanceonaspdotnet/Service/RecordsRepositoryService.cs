
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IRecordsRepositoryService {

    Task Create(RecordsRepository model , CancellationToken cancellationToken);
    Task<bool> Update(RecordsRepository model, CancellationToken cancellationToken);
    Task<RecordsRepository?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<RecordsRepository>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSystems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSystems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRetentionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRetentionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class RecordsRepositoryService : IRecordsRepositoryService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IRecordsRepositoryRepository _repository;
    private readonly ILogger<RecordsRepositoryService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public RecordsRepositoryService(
        ApplicationTelemetry telemetry,
        IRecordsRepositoryRepository repository,
        ILogger<RecordsRepositoryService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(RecordsRepository model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "RecordsRepository",
                "CreateRecordsRepository",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(RecordsRepository model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Location = model.Location;
            existing.OwnerDepartment = model.OwnerDepartment;
            existing.RepositoryType = model.RepositoryType;

            await _telemetry.Execute(
                "RecordsRepository",
                "UpdateRecordsRepository",
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

    public Task<RecordsRepository?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<RecordsRepository>> GetAll(CancellationToken cancellationToken)
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
                "RecordsRepository",
                "UpdateRecordsRepository",
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
            _logger.LogError("No RecordsRepository found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No RecordsRepository found using Id {ParentId}", request.ParentId);
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


    public async Task<bool> AddToRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RecordsRepository",
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
                "RecordsRepository",
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

    public async Task<bool> AddToSystems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RecordsRepository",
                "AddToSystems",
                () => _repository.AddToSystemsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSystems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RecordsRepository",
                "RemoveFromSystems",
                () => _repository.RemoveFromSystemsAsync(request, cancellationToken));
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

    public async Task<bool> AddToRetentionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RecordsRepository",
                "AddToRetentionSchedules",
                () => _repository.AddToRetentionSchedulesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRetentionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "RecordsRepository",
                "RemoveFromRetentionSchedules",
                () => _repository.RemoveFromRetentionSchedulesAsync(request, cancellationToken));
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
                "RecordsRepository",
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
                "RecordsRepository",
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



}
