
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IAuditEngagementService {

    Task Create(AuditEngagement model , CancellationToken cancellationToken);
    Task<bool> Update(AuditEngagement model, CancellationToken cancellationToken);
    Task<AuditEngagement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditEngagement>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignAuditProgram(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAuditProgram(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToControlTests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromControlTests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToWorkpapers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromWorkpapers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToFindings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFindings(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AuditEngagementService : IAuditEngagementService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IAuditEngagementRepository _repository;
    private readonly ILogger<AuditEngagementService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public AuditEngagementService(
        ApplicationTelemetry telemetry,
        IAuditEngagementRepository repository,
        ILogger<AuditEngagementService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(AuditEngagement model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "AuditEngagement",
                "CreateAuditEngagement",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(AuditEngagement model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Title = model.Title;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "AuditEngagement",
                "UpdateAuditEngagement",
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

    public Task<AuditEngagement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AuditEngagement>> GetAll(CancellationToken cancellationToken)
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
                "AuditEngagement",
                "UpdateAuditEngagement",
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

    public async Task<bool> AssignAuditProgram(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AuditEngagement found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AuditProgramService>().Get(childRequest, cancellationToken);
            parent.AuditProgram = child;
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

    public async Task<bool> UnassignAuditProgram(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AuditEngagement found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.AuditProgram = null;
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


    public async Task<bool> AddToBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditEngagement",
                "AddToBusinessUnits",
                () => _repository.AddToBusinessUnitsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromBusinessUnits(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditEngagement",
                "RemoveFromBusinessUnits",
                () => _repository.RemoveFromBusinessUnitsAsync(request, cancellationToken));
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

    public async Task<bool> AddToControlTests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditEngagement",
                "AddToControlTests",
                () => _repository.AddToControlTestsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromControlTests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditEngagement",
                "RemoveFromControlTests",
                () => _repository.RemoveFromControlTestsAsync(request, cancellationToken));
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

    public async Task<bool> AddToWorkpapers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditEngagement",
                "AddToWorkpapers",
                () => _repository.AddToWorkpapersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromWorkpapers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditEngagement",
                "RemoveFromWorkpapers",
                () => _repository.RemoveFromWorkpapersAsync(request, cancellationToken));
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

    public async Task<bool> AddToFindings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditEngagement",
                "AddToFindings",
                () => _repository.AddToFindingsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromFindings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditEngagement",
                "RemoveFromFindings",
                () => _repository.RemoveFromFindingsAsync(request, cancellationToken));
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
