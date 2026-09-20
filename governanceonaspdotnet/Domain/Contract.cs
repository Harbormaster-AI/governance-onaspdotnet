using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Contract
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ContractId { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
 public virtual DateOnly? ExpiryDate { get; set; } 
 public virtual URL? RepositoryUrl { get; set; } 
public virtual ThirdParty? ThirdParty { get; set; } 
public virtual ICollection<Obligation> Obligations { get; set; } = new List<Obligation>();
public virtual ICollection<DataProcessingActivity> DataProcessingActivities { get; set; } = new List<DataProcessingActivity>();
public virtual Matter? Matter { get; set; } 
 public virtual ContractStatus? Status { get; set; } 

    public static Contract FromRequest(ContractRequest request) {
        return new Contract {
            Id = request.Id,
            Title = request.Title,
            EffectiveDate = request.EffectiveDate,
            ExpiryDate = request.ExpiryDate,
            RepositoryUrl = request.RepositoryUrl,
            Status = request.Status,
        };
    }
}
