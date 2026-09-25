
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class AuditWorkpaper
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AuditworkpaperId { get; set; } 
 public virtual string? WorkpaperRef { get; set; } 
 public virtual string? Subject { get; set; } 
 public virtual URL? WorkpaperUrl { get; set; } 
public virtual AuditEngagement? Engagement { get; set; } 
public virtual ICollection<Evidence> Evidence { get; set; } = new List<Evidence>();
public virtual ICollection<AuditFinding> Findings { get; set; } = new List<AuditFinding>();

    public static AuditWorkpaper FromRequest(AuditWorkpaperRequest request) {
        return new AuditWorkpaper {
            Id = request.Id,
            WorkpaperRef = request.WorkpaperRef,
            Subject = request.Subject,
            WorkpaperUrl = request.WorkpaperUrl,
        };
    }
}
