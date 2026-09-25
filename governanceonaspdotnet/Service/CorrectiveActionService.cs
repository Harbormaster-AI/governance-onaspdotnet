
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface ICorrectiveActionService {

    Task Create(CorrectiveAction model , CancellationToken cancellationToken);
    Task<bool> Update(CorrectiveAction model, CancellationToken cancellationToken);
    Task<CorrectiveAction?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<CorrectiveAction>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignFinding(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignFinding(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignIssue(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignIssue(AssociationRequest request, CancellationToken cancellationToken);


}

public class CorrectiveActionService : ICorrectiveActionService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICorrectiveActionRepository _repository;
    private readonly ILogger<CorrectiveActionService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CorrectiveActionService(
        ApplicationTelemetry telemetry,
        ICorrectiveActionRepository repository,
        ILogger<CorrectiveActionService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(CorrectiveAction model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "CorrectiveAction",
                "CreateCorrectiveAction",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(CorrectiveAction model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ActionTitle = model.ActionTitle;
            existing.Owner = model.Owner;
            existing.TargetDate = model.TargetDate;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "CorrectiveAction",
                "UpdateCorrectiveAction",
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

    public Task<CorrectiveAction?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<CorrectiveAction>> GetAll(CancellationToken cancellationToken)
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
                "CorrectiveAction",
                "UpdateCorrectiveAction",
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

    public async Task<bool> AssignFinding(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CorrectiveAction found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AuditFindingService>().Get(childRequest, cancellationToken);
            parent.Finding = child;
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

    public async Task<bool> UnassignFinding(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CorrectiveAction found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Finding = null;
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

    public async Task<bool> AssignIssue(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CorrectiveAction found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<IssueService>().Get(childRequest, cancellationToken);
            parent.Issue = child;
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

    public async Task<bool> UnassignIssue(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No CorrectiveAction found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Issue = null;
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
