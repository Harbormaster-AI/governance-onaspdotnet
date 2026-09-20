using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Service;

public interface IIssueService {

    Task Create(Issue model , CancellationToken cancellationToken);
    Task<bool> Update(Issue model, CancellationToken cancellationToken);
    Task<Issue?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Issue>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignRisk(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignRisk(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignFinding(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignFinding(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignControl(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignControl(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class IssueService : IIssueService
{
    private readonly IIssueRepository _repository;
    private readonly ILogger<IssueService> _logger;

    public IssueService(
        IIssueRepository repository, ILogger<IssueService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Issue model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Issue model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Title = model.Title;
            existing.OpenedDate = model.OpenedDate;
            existing.ClosedDate = model.ClosedDate;
            existing.IssueType = model.IssueType;
            existing.Priority = model.Priority;
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

    public Task<Issue?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Issue>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignRisk(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignRisk(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignFinding(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignFinding(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignControl(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignControl(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCorrectiveActions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
