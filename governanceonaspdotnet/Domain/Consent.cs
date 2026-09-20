using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Consent
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ConsentId { get; set; } 
 public virtual string? SubjectIdentifier { get; set; } 
 public virtual DateOnly? CaptureDate { get; set; } 
 public virtual DateOnly? ExpiryDate { get; set; } 
public virtual ICollection<DataProcessingActivity> ProcessingActivities { get; set; } = new List<DataProcessingActivity>();
public virtual PrivacyNotice? PrivacyNotice { get; set; } 
 public virtual ConsentType? ConsentType { get; set; } 
 public virtual ConsentStatus? Status { get; set; } 

    public static Consent FromRequest(ConsentRequest request) {
        return new Consent {
            Id = request.Id,
            SubjectIdentifier = request.SubjectIdentifier,
            CaptureDate = request.CaptureDate,
            ExpiryDate = request.ExpiryDate,
            ConsentType = request.ConsentType,
            Status = request.Status,
        };
    }
}
