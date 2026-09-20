using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Service;

public interface IRecord_Service {

    Task Create(Record_ model , CancellationToken cancellationToken);
    Task<bool> Update(Record_ model, CancellationToken cancellationToken);
    Task<Record_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Record_>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRepository(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRepository(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataCategories(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDataSubjectRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDataSubjectRequests(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class Record_Service : IRecord_Service
{
    private readonly IRecord_Repository _repository;
    private readonly ILogger<Record_Service> _logger;

    public Record_Service(
        IRecord_Repository repository, ILogger<Record_Service> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Record_ model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Record_ model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Title = model.Title;
            existing.CreationDate = model.CreationDate;
            existing.RecordType = model.RecordType;
            existing.Classification = model.Classification;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Record_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Record_>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignRepository(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRepository(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRetentionSchedule(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromProcessingActivities(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDataCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDataCategories(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromLegalHolds(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToDataSubjectRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromDataSubjectRequests(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
