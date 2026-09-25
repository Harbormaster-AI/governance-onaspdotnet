using governanceonaspdotnet.Domain;
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Persistence;

public interface IDataBreachRepository
{
    Task<DataBreach?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<DataBreach>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(DataBreach dataBreach, CancellationToken cancellationToken);
    Task UpdateAsync(DataBreach dataBreach, CancellationToken cancellationToken);
    Task DeleteAsync(DataBreach dataBreach, CancellationToken cancellationToken);

    Task AddToProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProcessingActivitiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDataCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDataCategoriesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToThirdPartiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromThirdPartiesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
