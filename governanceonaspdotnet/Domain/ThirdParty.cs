using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class ThirdParty
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ThirdpartyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Country { get; set; } 
 public virtual EmailAddress? ContactEmail { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<DataProcessingActivity> ProcessingActivities { get; set; } = new List<DataProcessingActivity>();
public virtual ICollection<ThirdPartyAssessment> Assessments { get; set; } = new List<ThirdPartyAssessment>();
public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
public virtual ICollection<Obligation> Obligations { get; set; } = new List<Obligation>();
public virtual ICollection<DataBreach> DataBreaches { get; set; } = new List<DataBreach>();
 public virtual ThirdPartyType? ThirdPartyType { get; set; } 
 public virtual VendorCriticality? Criticality { get; set; } 

    public static ThirdParty FromRequest(ThirdPartyRequest request) {
        return new ThirdParty {
            Id = request.Id,
            Name = request.Name,
            Country = request.Country,
            ContactEmail = request.ContactEmail,
            ThirdPartyType = request.ThirdPartyType,
            Criticality = request.Criticality,
        };
    }
}
