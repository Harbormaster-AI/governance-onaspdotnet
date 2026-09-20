using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Obligation
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ObligationId { get; set; } 
 public virtual string? ReferenceNumber { get; set; } 
 public virtual string? DescriptionText { get; set; } 
public virtual Regulation? Regulation { get; set; } 
public virtual ICollection<Control> Controls { get; set; } = new List<Control>();
public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
 public virtual ObligationType? ObligationType { get; set; } 
 public virtual ControlFrequency? ReviewFrequency { get; set; } 

    public static Obligation FromRequest(ObligationRequest request) {
        return new Obligation {
            Id = request.Id,
            ReferenceNumber = request.ReferenceNumber,
            DescriptionText = request.DescriptionText,
            ObligationType = request.ObligationType,
            ReviewFrequency = request.ReviewFrequency,
        };
    }
}
