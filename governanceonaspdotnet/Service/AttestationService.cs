
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IAttestationService {

    Task Create(Attestation model , CancellationToken cancellationToken);
    Task<bool> Update(Attestation model, CancellationToken cancellationToken);
    Task<Attestation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Attestation>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignControl(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignControl(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignComplianceProgram(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignComplianceProgram(AssociationRequest request, CancellationToken cancellationToken);


}

public class AttestationService : IAttestationService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IAttestationRepository _repository;
    private readonly ILogger<AttestationService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public AttestationService(
        ApplicationTelemetry telemetry,
        IAttestationRepository repository,
        ILogger<AttestationService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Attestation model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Attestation",
                "CreateAttestation",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Attestation model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Statement = model.Statement;
            existing.Attestor = model.Attestor;
            existing.DateSigned = model.DateSigned;
            existing.Result = model.Result;

            await _telemetry.Execute(
                "Attestation",
                "UpdateAttestation",
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

    public Task<Attestation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Attestation>> GetAll(CancellationToken cancellationToken)
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
                "Attestation",
                "UpdateAttestation",
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
            _logger.LogError("No Attestation found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No Attestation found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Attestation found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No Attestation found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignComplianceProgram(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Attestation found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No Attestation found using Id {ParentId}", request.ParentId);
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




}
