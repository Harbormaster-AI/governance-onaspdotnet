using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IException_Repository
{
    Task<Exception_?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Exception_>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Exception_ exception_, CancellationToken cancellationToken);
    Task UpdateAsync(Exception_ exception_, CancellationToken cancellationToken);
    Task DeleteAsync(Exception_ exception_, CancellationToken cancellationToken);
}
