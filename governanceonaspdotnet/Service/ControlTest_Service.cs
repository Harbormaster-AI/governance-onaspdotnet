
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IControlTest_Service {

    Task Create(ControlTest_ model , CancellationToken cancellationToken);
    Task<bool> Update(ControlTest_ model, CancellationToken cancellationToken);
    Task<ControlTest_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ControlTest_>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignControl(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignControl(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignEngagement(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEngagement(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ControlTest_Service : IControlTest_Service
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IControlTest_Repository _repository;
    private readonly ILogger<ControlTest_Service> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ControlTest_Service(
        ApplicationTelemetry telemetry,
        IControlTest_Repository repository,
        ILogger<ControlTest_Service> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ControlTest_ model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ControlTest_",
                "CreateControlTest_",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(ControlTest_ model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.TestPeriodStart = model.TestPeriodStart;
            existing.TestPeriodEnd = model.TestPeriodEnd;
            existing.SampleSize = model.SampleSize;
            existing.TestType = model.TestType;
            existing.Effectiveness = model.Effectiveness;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "ControlTest_",
                "UpdateControlTest_",
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

    public Task<ControlTest_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ControlTest_>> GetAll(CancellationToken cancellationToken)
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
                "ControlTest_",
                "UpdateControlTest_",
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

    public async Task<bool> AssignControl(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ControlTest_ found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ControlService>().Get(childRequest, cancellationToken);
            parent.Control = child;
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

    public async Task<bool> UnassignControl(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ControlTest_ found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Control = null;
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

    public async Task<bool> AssignEngagement(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ControlTest_ found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No ControlTest_ found using Id {ParentId}", request.ParentId);
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


    public async Task<bool> AddToEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ControlTest_",
                "AddToEvidence",
                () => _repository.AddToEvidenceAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ControlTest_",
                "RemoveFromEvidence",
                () => _repository.RemoveFromEvidenceAsync(request, cancellationToken));
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
