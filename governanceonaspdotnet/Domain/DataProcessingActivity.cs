using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class DataProcessingActivity
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DataprocessingactivityId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Purpose { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<DataCategory> DataCategories { get; set; } = new List<DataCategory>();
public virtual ICollection<System_> Systems { get; set; } = new List<System_>();
public virtual ICollection<Record_> Records { get; set; } = new List<Record_>();
public virtual ICollection<PrivacyNotice> PrivacyNotices { get; set; } = new List<PrivacyNotice>();
public virtual ICollection<ThirdParty> ThirdParties { get; set; } = new List<ThirdParty>();
public virtual ICollection<Consent> Consents { get; set; } = new List<Consent>();
public virtual ICollection<DataBreach> DataBreaches { get; set; } = new List<DataBreach>();
public virtual ICollection<DataSubjectRequest> DataSubjectRequests { get; set; } = new List<DataSubjectRequest>();
 public virtual LawfulBasis? LawfulBasis { get; set; } 

    public static DataProcessingActivity FromRequest(DataProcessingActivityRequest request) {
        return new DataProcessingActivity {
            Id = request.Id,
            Name = request.Name,
            Purpose = request.Purpose,
            StartDate = request.StartDate,
            LawfulBasis = request.LawfulBasis,
        };
    }
}
