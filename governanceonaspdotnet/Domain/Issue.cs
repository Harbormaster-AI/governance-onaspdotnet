using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Issue
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? IssueId { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual DateOnly? OpenedDate { get; set; } 
 public virtual DateOnly? ClosedDate { get; set; } 
public virtual Risk? Risk { get; set; } 
public virtual AuditFinding? Finding { get; set; } 
public virtual ICollection<CorrectiveAction> CorrectiveActions { get; set; } = new List<CorrectiveAction>();
public virtual Control? Control { get; set; } 
 public virtual IssueType? IssueType { get; set; } 
 public virtual Priority? Priority { get; set; } 
 public virtual IssueStatus? Status { get; set; } 

    public static Issue FromRequest(IssueRequest request) {
        return new Issue {
            Id = request.Id,
            Title = request.Title,
            OpenedDate = request.OpenedDate,
            ClosedDate = request.ClosedDate,
            IssueType = request.IssueType,
            Priority = request.Priority,
            Status = request.Status,
        };
    }
}
