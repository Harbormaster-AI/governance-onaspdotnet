using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class AuditFinding
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AuditfindingId { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
public virtual AuditEngagement? Engagement { get; set; } 
public virtual AuditWorkpaper? Workpaper { get; set; } 
public virtual ICollection<CorrectiveAction> CorrectiveActions { get; set; } = new List<CorrectiveAction>();
public virtual ICollection<Risk> RelatedRisks { get; set; } = new List<Risk>();
public virtual ICollection<Control> RelatedControls { get; set; } = new List<Control>();
public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
 public virtual FindingSeverity? Severity { get; set; } 
 public virtual FindingStatus? Status { get; set; } 

    public static AuditFinding FromRequest(AuditFindingRequest request) {
        return new AuditFinding {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            Severity = request.Severity,
            Status = request.Status,
        };
    }
}
