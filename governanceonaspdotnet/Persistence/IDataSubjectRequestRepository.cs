using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IDataSubjectRequestRepository
{
    Task<DataSubjectRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataSubjectRequest>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DataSubjectRequest dataSubjectRequest, CancellationToken cancellationToken);
    Task UpdateAsync(DataSubjectRequest dataSubjectRequest, CancellationToken cancellationToken);
    Task DeleteAsync(DataSubjectRequest dataSubjectRequest, CancellationToken cancellationToken);
}
