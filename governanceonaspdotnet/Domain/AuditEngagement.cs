
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class AuditEngagement
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AuditengagementId { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
public virtual AuditProgram? AuditProgram { get; set; } 
public virtual ICollection<BusinessUnit> BusinessUnits { get; set; } = new List<BusinessUnit>();
public virtual ICollection<ControlTest_> ControlTests { get; set; } = new List<ControlTest_>();
public virtual ICollection<AuditWorkpaper> Workpapers { get; set; } = new List<AuditWorkpaper>();
public virtual ICollection<AuditFinding> Findings { get; set; } = new List<AuditFinding>();
 public virtual AuditStatus? Status { get; set; } 

    public static AuditEngagement FromRequest(AuditEngagementRequest request) {
        return new AuditEngagement {
            Id = request.Id,
            Title = request.Title,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
        };
    }
}
