
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IRegulationService {

    Task Create(Regulation model , CancellationToken cancellationToken);
    Task<bool> Update(Regulation model, CancellationToken cancellationToken);
    Task<Regulation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Regulation>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToObligations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromObligations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCompliancePrograms(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCompliancePrograms(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class RegulationService : IRegulationService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IRegulationRepository _repository;
    private readonly ILogger<RegulationService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public RegulationService(
        ApplicationTelemetry telemetry,
        IRegulationRepository repository,
        ILogger<RegulationService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Regulation model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Regulation",
                "CreateRegulation",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Regulation model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Citation = model.Citation;
            existing.Jurisdiction = model.Jurisdiction;
            existing.PublicationUrl = model.PublicationUrl;

            await _telemetry.Execute(
                "Regulation",
                "UpdateRegulation",
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

    public Task<Regulation?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Regulation>> GetAll(CancellationToken cancellationToken)
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
                "Regulation",
                "UpdateRegulation",
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


    public async Task<bool> AddToObligations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Regulation",
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
                "Regulation",
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

    public async Task<bool> AddToCompliancePrograms(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Regulation",
                "AddToCompliancePrograms",
                () => _repository.AddToComplianceProgramsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCompliancePrograms(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Regulation",
                "RemoveFromCompliancePrograms",
                () => _repository.RemoveFromComplianceProgramsAsync(request, cancellationToken));
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
