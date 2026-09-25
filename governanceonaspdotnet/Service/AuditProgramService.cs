
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IAuditProgramService {

    Task Create(AuditProgram model , CancellationToken cancellationToken);
    Task<bool> Update(AuditProgram model, CancellationToken cancellationToken);
    Task<AuditProgram?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditProgram>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToEngagements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEngagements(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AuditProgramService : IAuditProgramService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IAuditProgramRepository _repository;
    private readonly ILogger<AuditProgramService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public AuditProgramService(
        ApplicationTelemetry telemetry,
        IAuditProgramRepository repository,
        ILogger<AuditProgramService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(AuditProgram model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "AuditProgram",
                "CreateAuditProgram",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(AuditProgram model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Scope = model.Scope;
            existing.Cycle = model.Cycle;
            existing.Status = model.Status;

            await _telemetry.Execute(
                "AuditProgram",
                "UpdateAuditProgram",
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

    public Task<AuditProgram?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AuditProgram>> GetAll(CancellationToken cancellationToken)
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
                "AuditProgram",
                "UpdateAuditProgram",
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
            _logger.LogError("No AuditProgram found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No AuditProgram found using Id {ParentId}", request.ParentId);
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


    public async Task<bool> AddToEngagements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditProgram",
                "AddToEngagements",
                () => _repository.AddToEngagementsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromEngagements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditProgram",
                "RemoveFromEngagements",
                () => _repository.RemoveFromEngagementsAsync(request, cancellationToken));
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
