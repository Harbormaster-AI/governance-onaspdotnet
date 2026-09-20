using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IRecord_Repository
{
    Task<Record_?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Record_>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Record_ record_, CancellationToken cancellationToken);
    Task UpdateAsync(Record_ record_, CancellationToken cancellationToken);
    Task DeleteAsync(Record_ record_, CancellationToken cancellationToken);
}
