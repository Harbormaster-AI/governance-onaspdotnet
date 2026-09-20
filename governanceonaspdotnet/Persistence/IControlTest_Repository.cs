using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IControlTest_Repository
{
    Task<ControlTest_?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ControlTest_>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ControlTest_ controlTest_, CancellationToken cancellationToken);
    Task UpdateAsync(ControlTest_ controlTest_, CancellationToken cancellationToken);
    Task DeleteAsync(ControlTest_ controlTest_, CancellationToken cancellationToken);
}
