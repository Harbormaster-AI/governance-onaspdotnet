using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IRegulationRepository
{
    Task<Regulation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Regulation>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Regulation regulation, CancellationToken cancellationToken);
    Task UpdateAsync(Regulation regulation, CancellationToken cancellationToken);
    Task DeleteAsync(Regulation regulation, CancellationToken cancellationToken);
}
