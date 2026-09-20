using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IPrivacyNoticeRepository
{
    Task<PrivacyNotice?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PrivacyNotice>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PrivacyNotice privacyNotice, CancellationToken cancellationToken);
    Task UpdateAsync(PrivacyNotice privacyNotice, CancellationToken cancellationToken);
    Task DeleteAsync(PrivacyNotice privacyNotice, CancellationToken cancellationToken);
}
