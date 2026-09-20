using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class CorrectiveAction
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CorrectiveactionId { get; set; } 
 public virtual string? ActionTitle { get; set; } 
 public virtual string? Owner { get; set; } 
 public virtual DateOnly? TargetDate { get; set; } 
public virtual AuditFinding? Finding { get; set; } 
public virtual Issue? Issue { get; set; } 
 public virtual ActionStatus? Status { get; set; } 

    public static CorrectiveAction FromRequest(CorrectiveActionRequest request) {
        return new CorrectiveAction {
            Id = request.Id,
            ActionTitle = request.ActionTitle,
            Owner = request.Owner,
            TargetDate = request.TargetDate,
            Status = request.Status,
        };
    }
}
