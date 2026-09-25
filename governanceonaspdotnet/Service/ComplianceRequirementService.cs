
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IComplianceRequirementService {

    Task Create(ComplianceRequirement model , CancellationToken cancellationToken);
    Task<bool> Update(ComplianceRequirement model, CancellationToken cancellationToken);
    Task<ComplianceRequirement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ComplianceRequirement>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignComplianceProgram(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignComplianceProgram(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToControls(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromControls(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToObligations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromObligations(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ComplianceRequirementService : IComplianceRequirementService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IComplianceRequirementRepository _repository;
    private readonly ILogger<ComplianceRequirementService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ComplianceRequirementService(
        ApplicationTelemetry telemetry,
        IComplianceRequirementRepository repository,
        ILogger<ComplianceRequirementService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ComplianceRequirement model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ComplianceRequirement",
                "CreateComplianceRequirement",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(ComplianceRequirement model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Source = model.Source;
            existing.Citation = model.Citation;
            existing.Applicability = model.Applicability;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "ComplianceRequirement",
                "UpdateComplianceRequirement",
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

    public Task<ComplianceRequirement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ComplianceRequirement>> GetAll(CancellationToken cancellationToken)
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
                "ComplianceRequirement",
                "UpdateComplianceRequirement",
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

    public async Task<bool> AssignComplianceProgram(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ComplianceRequirement found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ComplianceProgramService>().Get(childRequest, cancellationToken);
            parent.ComplianceProgram = child;
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

    public async Task<bool> UnassignComplianceProgram(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ComplianceRequirement found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.ComplianceProgram = null;
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


    public async Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ComplianceRequirement",
                "AddToPolicies",
                () => _repository.AddToPoliciesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ComplianceRequirement",
                "RemoveFromPolicies",
                () => _repository.RemoveFromPoliciesAsync(request, cancellationToken));
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

    public async Task<bool> AddToControls(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ComplianceRequirement",
                "AddToControls",
                () => _repository.AddToControlsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromControls(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ComplianceRequirement",
                "RemoveFromControls",
                () => _repository.RemoveFromControlsAsync(request, cancellationToken));
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
                "ComplianceRequirement",
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
                "ComplianceRequirement",
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



}
