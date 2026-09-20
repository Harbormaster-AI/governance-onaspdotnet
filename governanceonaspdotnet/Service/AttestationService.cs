using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

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
    private readonly IAttestationRepository _repository;
    private readonly ILogger<AttestationService> _logger;

    public AttestationService(
        IAttestationRepository repository, ILogger<AttestationService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Attestation model, CancellationToken cancellationToken)
    {

 
 
         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignControl(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignControl(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignComplianceProgram(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignComplianceProgram(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
