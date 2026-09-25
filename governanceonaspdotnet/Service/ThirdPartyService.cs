
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IThirdPartyService {

    Task Create(ThirdParty model , CancellationToken cancellationToken);
    Task<bool> Update(ThirdParty model, CancellationToken cancellationToken);
    Task<ThirdParty?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ThirdParty>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAssessments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAssessments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromContracts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToObligations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromObligations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataBreaches(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ThirdPartyService : IThirdPartyService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IThirdPartyRepository _repository;
    private readonly ILogger<ThirdPartyService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ThirdPartyService(
        ApplicationTelemetry telemetry,
        IThirdPartyRepository repository,
        ILogger<ThirdPartyService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ThirdParty model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ThirdParty",
                "CreateThirdParty",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(ThirdParty model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Country = model.Country;
            existing.ContactEmail = model.ContactEmail;
            existing.ThirdPartyType = model.ThirdPartyType;
            existing.Criticality = model.Criticality;

            await _telemetry.Execute(
                "ThirdParty",
                "UpdateThirdParty",
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

    public Task<ThirdParty?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ThirdParty>> GetAll(CancellationToken cancellationToken)
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
                "ThirdParty",
                "UpdateThirdParty",
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
            _logger.LogError("No ThirdParty found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No ThirdParty found using Id {ParentId}", request.ParentId);
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
                "ThirdParty",
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
                "ThirdParty",
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

    public async Task<bool> AddToAssessments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ThirdParty",
                "AddToAssessments",
                () => _repository.AddToAssessmentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAssessments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ThirdParty",
                "RemoveFromAssessments",
                () => _repository.RemoveFromAssessmentsAsync(request, cancellationToken));
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

    public async Task<bool> AddToContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ThirdParty",
                "AddToContracts",
                () => _repository.AddToContractsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromContracts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ThirdParty",
                "RemoveFromContracts",
                () => _repository.RemoveFromContractsAsync(request, cancellationToken));
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

    public async Task<bool> AddToObligations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ThirdParty",
                "AddToObligations",
                () => _repository.AddToObligationsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromObligations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ThirdParty",
                "RemoveFromObligations",
                () => _repository.RemoveFromObligationsAsync(request, cancellationToken));
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
                "ThirdParty",
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
                "ThirdParty",
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
