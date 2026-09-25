
using governanceonaspdotnet.Contracts;
using governanceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace governanceonaspdotnet.Persistence;

public class AttestationRepository : IAttestationRepository
{
    private readonly ApplicationDbContext _db;

    public AttestationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Attestation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Attestations
            .Include(x => x.Control)
            .Include(x => x.Policy)
            .Include(x => x.ComplianceProgram)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Attestation>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Attestations
            .AsNoTracking()
            .Include(x => x.Control)
            .Include(x => x.Policy)
            .Include(x => x.ComplianceProgram)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Attestation attestation, CancellationToken cancellationToken)
    {
        _db.Attestations.Add(attestation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Attestation attestation, CancellationToken cancellationToken)
    {
        _db.Attestations.Update(attestation);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Attestation attestation, CancellationToken cancellationToken)
    {
        _db.Attestations.Remove(attestation);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
