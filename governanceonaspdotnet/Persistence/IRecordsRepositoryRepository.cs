using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IRecordsRepositoryRepository
{
    Task<RecordsRepository?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<RecordsRepository>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken);
    Task UpdateAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken);
    Task DeleteAsync(RecordsRepository recordsRepository, CancellationToken cancellationToken);
}
