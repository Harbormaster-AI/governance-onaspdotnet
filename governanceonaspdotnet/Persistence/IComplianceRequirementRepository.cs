using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IComplianceRequirementRepository
{
    Task<ComplianceRequirement?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ComplianceRequirement>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken);
    Task UpdateAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken);
    Task DeleteAsync(ComplianceRequirement complianceRequirement, CancellationToken cancellationToken);
}
