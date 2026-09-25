
using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Telemetry;

namespace governanceonaspdotnet.Service;

public interface IPersonService {

    Task Create(Person model , CancellationToken cancellationToken);
    Task<bool> Update(Person model, CancellationToken cancellationToken);
    Task<Person?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Person>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToRoleAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRoleAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOwnedPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOwnedPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PersonService : IPersonService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IPersonRepository _repository;
    private readonly ILogger<PersonService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public PersonService(
        ApplicationTelemetry telemetry,
        IPersonRepository repository,
        ILogger<PersonService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Person model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Person",
                "CreatePerson",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Person model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.Email = model.Email;
            existing.Department = model.Department;

            await _telemetry.Execute(
                "Person",
                "UpdatePerson",
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

    public Task<Person?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Person>> GetAll(CancellationToken cancellationToken)
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
                "Person",
                "UpdatePerson",
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


    public async Task<bool> AddToRoleAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Person",
                "AddToRoleAssignments",
                () => _repository.AddToRoleAssignmentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromRoleAssignments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Person",
                "RemoveFromRoleAssignments",
                () => _repository.RemoveFromRoleAssignmentsAsync(request, cancellationToken));
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

    public async Task<bool> AddToOwnedPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Person",
                "AddToOwnedPolicies",
                () => _repository.AddToOwnedPoliciesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromOwnedPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Person",
                "RemoveFromOwnedPolicies",
                () => _repository.RemoveFromOwnedPoliciesAsync(request, cancellationToken));
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

    public async Task<bool> AddToCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Person",
                "AddToCorrectiveActions",
                () => _repository.AddToCorrectiveActionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Person",
                "RemoveFromCorrectiveActions",
                () => _repository.RemoveFromCorrectiveActionsAsync(request, cancellationToken));
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
