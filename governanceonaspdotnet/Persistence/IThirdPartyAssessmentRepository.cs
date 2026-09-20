using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public interface IThirdPartyAssessmentRepository
{
    Task<ThirdPartyAssessment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ThirdPartyAssessment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ThirdPartyAssessment thirdPartyAssessment, CancellationToken cancellationToken);
    Task UpdateAsync(ThirdPartyAssessment thirdPartyAssessment, CancellationToken cancellationToken);
    Task DeleteAsync(ThirdPartyAssessment thirdPartyAssessment, CancellationToken cancellationToken);
}
