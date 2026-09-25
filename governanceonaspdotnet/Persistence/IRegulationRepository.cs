using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IRegulationRepository
{
    Task<Regulation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Regulation>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Regulation regulation, CancellationToken cancellationToken);
    Task UpdateAsync(Regulation regulation, CancellationToken cancellationToken);
    Task DeleteAsync(Regulation regulation, CancellationToken cancellationToken);

    Task AddToObligationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromObligationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToComplianceProgramsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromComplianceProgramsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
