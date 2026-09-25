
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IControlService {

    Task Create(Control model , CancellationToken cancellationToken);
    Task<bool> Update(Control model, CancellationToken cancellationToken);
    Task<Control?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Control>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToControlTests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromControlTests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRisks(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRisks(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToObligations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromObligations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToProcedures(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProcedures(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToIssues(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromIssues(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ControlService : IControlService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IControlRepository _repository;
    private readonly ILogger<ControlService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ControlService(
        ApplicationTelemetry telemetry,
        IControlRepository repository,
        ILogger<ControlService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Control model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Control",
                "CreateControl",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Control model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Objective = model.Objective;
            existing.OwnerDepartment = model.OwnerDepartment;
            existing.ControlType = model.ControlType;
            existing.Frequency = model.Frequency;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "Control",
                "UpdateControl",
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

    public Task<Control?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Control>> GetAll(CancellationToken cancellationToken)
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
                "Control",
                "UpdateControl",
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

    public async Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Control found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<PolicyService>().Get(childRequest, cancellationToken);
            parent.Policy = child;
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

    public async Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Control found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Policy = null;
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


    public async Task<bool> AddToControlTests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Control",
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
                "Control",
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

    public async Task<bool> AddToEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Control",
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
                "Control",
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

    public async Task<bool> AddToRisks(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Control",
                "AddToRisks",
                () => _repository.AddToRisksAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRisks(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Control",
                "RemoveFromRisks",
                () => _repository.RemoveFromRisksAsync(request, cancellationToken));
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
                "Control",
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
                "Control",
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

    public async Task<bool> AddToProcedures(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Control",
                "AddToProcedures",
                () => _repository.AddToProceduresAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromProcedures(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Control",
                "RemoveFromProcedures",
                () => _repository.RemoveFromProceduresAsync(request, cancellationToken));
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
                "Control",
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
                "Control",
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
