using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IAttestationRepository
{
    Task<Attestation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Attestation>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Attestation attestation, CancellationToken cancellationToken);
    Task UpdateAsync(Attestation attestation, CancellationToken cancellationToken);
    Task DeleteAsync(Attestation attestation, CancellationToken cancellationToken);


}
