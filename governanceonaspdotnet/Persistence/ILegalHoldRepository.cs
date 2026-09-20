using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface ILegalHoldRepository
{
    Task<LegalHold?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<LegalHold>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(LegalHold legalHold, CancellationToken cancellationToken);
    Task UpdateAsync(LegalHold legalHold, CancellationToken cancellationToken);
    Task DeleteAsync(LegalHold legalHold, CancellationToken cancellationToken);
}
