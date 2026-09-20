using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Service;

public interface IRecordsRepositoryService {

    Task Create(RecordsRepository model , CancellationToken cancellationToken);
    Task<bool> Update(RecordsRepository model, CancellationToken cancellationToken);
    Task<RecordsRepository?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<RecordsRepository>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRecords(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSystems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSystems(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToRetentionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromRetentionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class RecordsRepositoryService : IRecordsRepositoryService
{
    private readonly IRecordsRepositoryRepository _repository;
    private readonly ILogger<RecordsRepositoryService> _logger;

    public RecordsRepositoryService(
        IRecordsRepositoryRepository repository, ILogger<RecordsRepositoryService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(RecordsRepository model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(RecordsRepository model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.Location = model.Location;
            existing.OwnerDepartment = model.OwnerDepartment;
            existing.RepositoryType = model.RepositoryType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<RecordsRepository?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<RecordsRepository>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignOrganization(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignOrganization(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromRecords(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToSystems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSystems(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToRetentionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromRetentionSchedules(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
