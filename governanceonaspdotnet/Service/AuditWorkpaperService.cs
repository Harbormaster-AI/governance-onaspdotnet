
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IAuditWorkpaperService {

    Task Create(AuditWorkpaper model , CancellationToken cancellationToken);
    Task<bool> Update(AuditWorkpaper model, CancellationToken cancellationToken);
    Task<AuditWorkpaper?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditWorkpaper>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignEngagement(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignEngagement(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEvidence(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToFindings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromFindings(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AuditWorkpaperService : IAuditWorkpaperService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IAuditWorkpaperRepository _repository;
    private readonly ILogger<AuditWorkpaperService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public AuditWorkpaperService(
        ApplicationTelemetry telemetry,
        IAuditWorkpaperRepository repository,
        ILogger<AuditWorkpaperService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(AuditWorkpaper model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "AuditWorkpaper",
                "CreateAuditWorkpaper",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(AuditWorkpaper model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.WorkpaperRef = model.WorkpaperRef;
            existing.Subject = model.Subject;
            existing.WorkpaperUrl = model.WorkpaperUrl;

            await _telemetry.Execute(
                "AuditWorkpaper",
                "UpdateAuditWorkpaper",
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

    public Task<AuditWorkpaper?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<AuditWorkpaper>> GetAll(CancellationToken cancellationToken)
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
                "AuditWorkpaper",
                "UpdateAuditWorkpaper",
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
            _logger.LogError("No AuditWorkpaper found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No AuditWorkpaper found using Id {ParentId}", request.ParentId);
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
                "AuditWorkpaper",
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
                "AuditWorkpaper",
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

    public async Task<bool> AddToFindings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "AuditWorkpaper",
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
                "AuditWorkpaper",
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
