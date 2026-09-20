using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IAuditProgramRepository
{
    Task<AuditProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AuditProgram>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(AuditProgram auditProgram, CancellationToken cancellationToken);
    Task UpdateAsync(AuditProgram auditProgram, CancellationToken cancellationToken);
    Task DeleteAsync(AuditProgram auditProgram, CancellationToken cancellationToken);
}
