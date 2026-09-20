using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class DataBreach
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DatabreachId { get; set; } 
 public virtual DateOnly? IncidentDate { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual int? RecordsAffected { get; set; } 
 public virtual bool? NotificationRequired { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<DataProcessingActivity> ProcessingActivities { get; set; } = new List<DataProcessingActivity>();
public virtual ICollection<DataCategory> DataCategories { get; set; } = new List<DataCategory>();
public virtual ICollection<ThirdParty> ThirdParties { get; set; } = new List<ThirdParty>();
public virtual Matter? Matter { get; set; } 
 public virtual BreachSeverity? Severity { get; set; } 
 public virtual IncidentStatus? Status { get; set; } 

    public static DataBreach FromRequest(DataBreachRequest request) {
        return new DataBreach {
            Id = request.Id,
            IncidentDate = request.IncidentDate,
            Description = request.Description,
            RecordsAffected = request.RecordsAffected,
            NotificationRequired = request.NotificationRequired,
            Severity = request.Severity,
            Status = request.Status,
        };
    }
}
