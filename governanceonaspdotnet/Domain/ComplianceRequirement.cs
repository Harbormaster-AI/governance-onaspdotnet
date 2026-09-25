
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class ComplianceRequirement
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CompliancerequirementId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Source { get; set; } 
 public virtual string? Citation { get; set; } 
public virtual ComplianceProgram? ComplianceProgram { get; set; } 
public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
public virtual ICollection<Control> Controls { get; set; } = new List<Control>();
public virtual ICollection<Obligation> Obligations { get; set; } = new List<Obligation>();
 public virtual Applicability? Applicability { get; set; } 
 public virtual ComplianceStatus? Status { get; set; } 

    public static ComplianceRequirement FromRequest(ComplianceRequirementRequest request) {
        return new ComplianceRequirement {
            Id = request.Id,
            Name = request.Name,
            Source = request.Source,
            Citation = request.Citation,
            Applicability = request.Applicability,
            Status = request.Status,
        };
    }
}
