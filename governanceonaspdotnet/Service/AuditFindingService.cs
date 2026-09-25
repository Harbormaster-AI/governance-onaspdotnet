
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IAuditFindingService {

    Task Create(AuditFinding model , CancellationToken cancellationToken);
    Task<bool> Update(AuditFinding model, CancellationToken cancellationToken);
    Task<AuditFinding?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditFinding>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEngagement(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEngagement(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignWorkpaper(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignWorkpaper(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRelatedRisks(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRelatedRisks(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRelatedControls(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRelatedControls(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToIssues(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromIssues(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AuditFindingService : IAuditFindingService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IAuditFindingRepository _repository;
    private readonly ILogger<AuditFindingService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public AuditFindingService(
        ApplicationTelemetry telemetry,
        IAuditFindingRepository repository,
        ILogger<AuditFindingService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(AuditFinding model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "AuditFinding",
                "CreateAuditFinding",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(AuditFinding model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.DueDate = model.DueDate;
            existing.Severity = model.Severity;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "AuditFinding",
                "UpdateAuditFinding",
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

    public Task<AuditFinding?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AuditFinding>> GetAll(CancellationToken cancellationToken)
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
                "AuditFinding",
                "UpdateAuditFinding",
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

    public async Task<bool> AssignEngagement(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AuditFinding found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AuditEngagementService>().Get(childRequest, cancellationToken);
            parent.Engagement = child;
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

    public async Task<bool> UnassignEngagement(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AuditFinding found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Engagement = null;
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

    public async Task<bool> AssignWorkpaper(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AuditFinding found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AuditWorkpaperService>().Get(childRequest, cancellationToken);
            parent.Workpaper = child;
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

    public async Task<bool> UnassignWorkpaper(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No AuditFinding found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Workpaper = null;
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


    public async Task<bool> AddToCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditFinding",
                "AddToCorrectiveActions",
                () => _repository.AddToCorrectiveActionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditFinding",
                "RemoveFromCorrectiveActions",
                () => _repository.RemoveFromCorrectiveActionsAsync(request, cancellationToken));
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

    public async Task<bool> AddToRelatedRisks(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditFinding",
                "AddToRelatedRisks",
                () => _repository.AddToRelatedRisksAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRelatedRisks(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditFinding",
                "RemoveFromRelatedRisks",
                () => _repository.RemoveFromRelatedRisksAsync(request, cancellationToken));
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

    public async Task<bool> AddToRelatedControls(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditFinding",
                "AddToRelatedControls",
                () => _repository.AddToRelatedControlsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRelatedControls(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditFinding",
                "RemoveFromRelatedControls",
                () => _repository.RemoveFromRelatedControlsAsync(request, cancellationToken));
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

    public async Task<bool> AddToIssues(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditFinding",
                "AddToIssues",
                () => _repository.AddToIssuesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromIssues(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditFinding",
                "RemoveFromIssues",
                () => _repository.RemoveFromIssuesAsync(request, cancellationToken));
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
