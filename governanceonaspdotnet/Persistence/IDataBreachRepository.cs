using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IDataBreachRepository
{
    Task<DataBreach?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataBreach>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DataBreach dataBreach, CancellationToken cancellationToken);
    Task UpdateAsync(DataBreach dataBreach, CancellationToken cancellationToken);
    Task DeleteAsync(DataBreach dataBreach, CancellationToken cancellationToken);
}
