using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IPrivacyNoticeRepository
{
    Task<PrivacyNotice?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PrivacyNotice>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PrivacyNotice privacyNotice, CancellationToken cancellationToken);
    Task UpdateAsync(PrivacyNotice privacyNotice, CancellationToken cancellationToken);
    Task DeleteAsync(PrivacyNotice privacyNotice, CancellationToken cancellationToken);

    Task AddToProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToConsentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromConsentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
