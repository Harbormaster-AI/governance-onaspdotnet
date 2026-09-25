
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Control
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ControlId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Objective { get; set; } 
 public virtual string? OwnerDepartment { get; set; } 
public virtual Policy? Policy { get; set; } 
public virtual ICollection<ControlTest_> ControlTests { get; set; } = new List<ControlTest_>();
public virtual ICollection<Evidence> Evidence { get; set; } = new List<Evidence>();
public virtual ICollection<Risk> Risks { get; set; } = new List<Risk>();
public virtual ICollection<Obligation> Obligations { get; set; } = new List<Obligation>();
public virtual ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
 public virtual ControlType? ControlType { get; set; } 
 public virtual ControlFrequency? Frequency { get; set; } 
 public virtual ControlStatus? Status { get; set; } 

    public static Control FromRequest(ControlRequest request) {
        return new Control {
            Id = request.Id,
            Name = request.Name,
            Objective = request.Objective,
            OwnerDepartment = request.OwnerDepartment,
            ControlType = request.ControlType,
            Frequency = request.Frequency,
            Status = request.Status,
        };
    }
}
