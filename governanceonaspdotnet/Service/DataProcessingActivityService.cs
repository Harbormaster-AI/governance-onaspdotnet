
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IDataProcessingActivityService {

    Task Create(DataProcessingActivity model , CancellationToken cancellationToken);
    Task<bool> Update(DataProcessingActivity model, CancellationToken cancellationToken);
    Task<DataProcessingActivity?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataProcessingActivity>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDataCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSystems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSystems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPrivacyNotices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPrivacyNotices(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToThirdParties(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromThirdParties(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToConsents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromConsents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataSubjectRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataSubjectRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DataProcessingActivityService : IDataProcessingActivityService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IDataProcessingActivityRepository _repository;
    private readonly ILogger<DataProcessingActivityService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public DataProcessingActivityService(
        ApplicationTelemetry telemetry,
        IDataProcessingActivityRepository repository,
        ILogger<DataProcessingActivityService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(DataProcessingActivity model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "DataProcessingActivity",
                "CreateDataProcessingActivity",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(DataProcessingActivity model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Purpose = model.Purpose;
            existing.StartDate = model.StartDate;
            existing.LawfulBasis = model.LawfulBasis;

            await _telemetry.Execute(
                "DataProcessingActivity",
                "UpdateDataProcessingActivity",
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

    public Task<DataProcessingActivity?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<DataProcessingActivity>> GetAll(CancellationToken cancellationToken)
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
                "DataProcessingActivity",
                "UpdateDataProcessingActivity",
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
            _logger.LogError("No DataProcessingActivity found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No DataProcessingActivity found using Id {ParentId}", request.ParentId);
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


    public async Task<bool> AddToDataCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataProcessingActivity",
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
                "DataProcessingActivity",
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

    public async Task<bool> AddToSystems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataProcessingActivity",
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
                "DataProcessingActivity",
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

    public async Task<bool> AddToRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataProcessingActivity",
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
                "DataProcessingActivity",
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

    public async Task<bool> AddToPrivacyNotices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataProcessingActivity",
                "AddToPrivacyNotices",
                () => _repository.AddToPrivacyNoticesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPrivacyNotices(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataProcessingActivity",
                "RemoveFromPrivacyNotices",
                () => _repository.RemoveFromPrivacyNoticesAsync(request, cancellationToken));
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

    public async Task<bool> AddToThirdParties(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataProcessingActivity",
                "AddToThirdParties",
                () => _repository.AddToThirdPartiesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromThirdParties(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataProcessingActivity",
                "RemoveFromThirdParties",
                () => _repository.RemoveFromThirdPartiesAsync(request, cancellationToken));
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

    public async Task<bool> AddToConsents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataProcessingActivity",
                "AddToConsents",
                () => _repository.AddToConsentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromConsents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataProcessingActivity",
                "RemoveFromConsents",
                () => _repository.RemoveFromConsentsAsync(request, cancellationToken));
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
                "DataProcessingActivity",
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
                "DataProcessingActivity",
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

    public async Task<bool> AddToDataSubjectRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "DataProcessingActivity",
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
                "DataProcessingActivity",
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
