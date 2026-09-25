
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class RetentionSchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RetentionscheduleId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual int? RetentionPeriodMonths { get; set; } 
public virtual ICollection<RecordsRepository> Repositories { get; set; } = new List<RecordsRepository>();
public virtual ICollection<Record_> Records { get; set; } = new List<Record_>();
public virtual ICollection<Exception_> Exceptions { get; set; } = new List<Exception_>();
public virtual ICollection<DispositionReview> DispositionReviews { get; set; } = new List<DispositionReview>();
 public virtual RetentionTrigger? RetentionTrigger { get; set; } 
 public virtual DispositionAction? DispositionAction { get; set; } 
 public virtual RetentionStatus? Status { get; set; } 

    public static RetentionSchedule FromRequest(RetentionScheduleRequest request) {
        return new RetentionSchedule {
            Id = request.Id,
            Name = request.Name,
            RetentionPeriodMonths = request.RetentionPeriodMonths,
            RetentionTrigger = request.RetentionTrigger,
            DispositionAction = request.DispositionAction,
            Status = request.Status,
        };
    }
}
