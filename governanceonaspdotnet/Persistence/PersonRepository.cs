using governanceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _db;

    public PersonRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Persons
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Person>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Persons
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Person person, CancellationToken cancellationToken)
    {
        _db.Persons.Add(person);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Person person, CancellationToken cancellationToken)
    {
        _db.Persons.Update(person);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Person person, CancellationToken cancellationToken)
    {
        _db.Persons.Remove(person);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
