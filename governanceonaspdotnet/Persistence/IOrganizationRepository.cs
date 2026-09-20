using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IOrganizationRepository
{
    Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Organization>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Organization organization, CancellationToken cancellationToken);
    Task UpdateAsync(Organization organization, CancellationToken cancellationToken);
    Task DeleteAsync(Organization organization, CancellationToken cancellationToken);
}
