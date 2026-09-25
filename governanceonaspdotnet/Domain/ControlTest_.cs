
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class ControlTest_
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? Controltest_Id { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual DateOnly? TestPeriodStart { get; set; } 
 public virtual DateOnly? TestPeriodEnd { get; set; } 
 public virtual int? SampleSize { get; set; } 
public virtual Control? Control { get; set; } 
public virtual ICollection<Evidence> Evidence { get; set; } = new List<Evidence>();
public virtual AuditEngagement? Engagement { get; set; } 
 public virtual TestType? TestType { get; set; } 
 public virtual ControlEffectiveness? Effectiveness { get; set; } 
 public virtual TestStatus? Status { get; set; } 

    public static ControlTest_ FromRequest(ControlTest_Request request) {
        return new ControlTest_ {
            Id = request.Id,
            Name = request.Name,
            TestPeriodStart = request.TestPeriodStart,
            TestPeriodEnd = request.TestPeriodEnd,
            SampleSize = request.SampleSize,
            TestType = request.TestType,
            Effectiveness = request.Effectiveness,
            Status = request.Status,
        };
    }
}
