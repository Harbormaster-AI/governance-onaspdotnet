using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Exception_
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? Exception_Id { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual string? Justification { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
public virtual RetentionSchedule? RetentionSchedule { get; set; } 
public virtual Policy? Policy { get; set; } 
public virtual Control? Control { get; set; } 
public virtual Risk? Risk { get; set; } 
 public virtual ExceptionType? ExceptionType { get; set; } 
 public virtual ExceptionStatus? Status { get; set; } 

    public static Exception_ FromRequest(Exception_Request request) {
        return new Exception_ {
            Id = request.Id,
            Title = request.Title,
            Justification = request.Justification,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            ExceptionType = request.ExceptionType,
            Status = request.Status,
        };
    }
}
