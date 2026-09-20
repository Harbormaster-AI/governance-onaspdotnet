using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IContractRepository
{
    Task<Contract?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Contract>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Contract contract, CancellationToken cancellationToken);
    Task UpdateAsync(Contract contract, CancellationToken cancellationToken);
    Task DeleteAsync(Contract contract, CancellationToken cancellationToken);
}
