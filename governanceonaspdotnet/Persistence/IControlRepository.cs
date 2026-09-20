using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IControlRepository
{
    Task<Control?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Control>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Control control, CancellationToken cancellationToken);
    Task UpdateAsync(Control control, CancellationToken cancellationToken);
    Task DeleteAsync(Control control, CancellationToken cancellationToken);
}
