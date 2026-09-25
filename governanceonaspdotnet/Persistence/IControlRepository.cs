using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IControlRepository
{
    Task<Control?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Control>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Control control, CancellationToken cancellationToken);
    Task UpdateAsync(Control control, CancellationToken cancellationToken);
    Task DeleteAsync(Control control, CancellationToken cancellationToken);

    Task AddToControlTestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromControlTestsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEvidenceAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEvidenceAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToRisksAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromRisksAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToObligationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromObligationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToProceduresAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProceduresAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToIssuesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromIssuesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
