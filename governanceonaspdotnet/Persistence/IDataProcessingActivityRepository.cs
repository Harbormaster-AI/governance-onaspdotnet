using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IDataProcessingActivityRepository
{
    Task<DataProcessingActivity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataProcessingActivity>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken);
    Task UpdateAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken);
    Task DeleteAsync(DataProcessingActivity dataProcessingActivity, CancellationToken cancellationToken);
}
