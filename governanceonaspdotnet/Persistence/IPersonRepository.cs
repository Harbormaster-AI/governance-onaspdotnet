using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Person>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Person person, CancellationToken cancellationToken);
    Task UpdateAsync(Person person, CancellationToken cancellationToken);
    Task DeleteAsync(Person person, CancellationToken cancellationToken);
}
