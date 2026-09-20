using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class PrivacyNotice
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PrivacynoticeId { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual string? Audience { get; set; } 
 public virtual string? VersionLabel { get; set; } 
 public virtual DateOnly? PublicationDate { get; set; } 
 public virtual URL? PublicationUrl { get; set; } 
public virtual ICollection<DataProcessingActivity> ProcessingActivities { get; set; } = new List<DataProcessingActivity>();
public virtual Organization? Organization { get; set; } 
public virtual ICollection<Consent> Consents { get; set; } = new List<Consent>();
 public virtual DocumentStatus? Status { get; set; } 

    public static PrivacyNotice FromRequest(PrivacyNoticeRequest request) {
        return new PrivacyNotice {
            Id = request.Id,
            Title = request.Title,
            Audience = request.Audience,
            VersionLabel = request.VersionLabel,
            PublicationDate = request.PublicationDate,
            PublicationUrl = request.PublicationUrl,
            Status = request.Status,
        };
    }
}
