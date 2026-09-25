using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IConsentRepository
{
    Task<Consent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Consent>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Consent consent, CancellationToken cancellationToken);
    Task UpdateAsync(Consent consent, CancellationToken cancellationToken);
    Task DeleteAsync(Consent consent, CancellationToken cancellationToken);

    Task AddToProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
