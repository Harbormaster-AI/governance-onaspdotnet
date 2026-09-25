
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IThirdPartyAssessmentService {

    Task Create(ThirdPartyAssessment model , CancellationToken cancellationToken);
    Task<bool> Update(ThirdPartyAssessment model, CancellationToken cancellationToken);
    Task<ThirdPartyAssessment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ThirdPartyAssessment>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignThirdParty(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignThirdParty(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToIssues(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromIssues(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ThirdPartyAssessmentService : IThirdPartyAssessmentService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IThirdPartyAssessmentRepository _repository;
    private readonly ILogger<ThirdPartyAssessmentService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ThirdPartyAssessmentService(
        ApplicationTelemetry telemetry,
        IThirdPartyAssessmentRepository repository,
        ILogger<ThirdPartyAssessmentService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ThirdPartyAssessment model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ThirdPartyAssessment",
                "CreateThirdPartyAssessment",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(ThirdPartyAssessment model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.AssessmentDate = model.AssessmentDate;
            existing.Assessor = model.Assessor;
            existing.AssessmentType = model.AssessmentType;
            existing.Result = model.Result;

            await _telemetry.Execute(
                "ThirdPartyAssessment",
                "UpdateThirdPartyAssessment",
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

    public Task<ThirdPartyAssessment?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ThirdPartyAssessment>> GetAll(CancellationToken cancellationToken)
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
                "ThirdPartyAssessment",
                "UpdateThirdPartyAssessment",
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

    public async Task<bool> AssignThirdParty(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ThirdPartyAssessment found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<ThirdPartyService>().Get(childRequest, cancellationToken);
            parent.ThirdParty = child;
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

    public async Task<bool> UnassignThirdParty(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No ThirdPartyAssessment found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.ThirdParty = null;
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


    public async Task<bool> AddToIssues(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ThirdPartyAssessment",
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
                "ThirdPartyAssessment",
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
