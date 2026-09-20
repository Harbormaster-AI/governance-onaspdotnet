using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IComplianceProgramRepository
{
    Task<ComplianceProgram?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ComplianceProgram>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken);
    Task UpdateAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken);
    Task DeleteAsync(ComplianceProgram complianceProgram, CancellationToken cancellationToken);
}
