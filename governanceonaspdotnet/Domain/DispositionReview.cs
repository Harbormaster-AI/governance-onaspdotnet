
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class DispositionReview
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DispositionreviewId { get; set; } 
 public virtual DateOnly? ReviewDate { get; set; } 
 public virtual string? Reviewer { get; set; } 
 public virtual string? Notes { get; set; } 
public virtual Record_? Record { get; set; } 
public virtual RetentionSchedule? RetentionSchedule { get; set; } 
 public virtual DispositionOutcome? Outcome { get; set; } 

    public static DispositionReview FromRequest(DispositionReviewRequest request) {
        return new DispositionReview {
            Id = request.Id,
            ReviewDate = request.ReviewDate,
            Reviewer = request.Reviewer,
            Notes = request.Notes,
            Outcome = request.Outcome,
        };
    }
}
