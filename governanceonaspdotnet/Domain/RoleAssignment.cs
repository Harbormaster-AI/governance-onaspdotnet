using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class RoleAssignment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RoleassignmentId { get; set; } 
 public virtual DateOnly? EffectiveFrom { get; set; } 
 public virtual DateOnly? EffectiveTo { get; set; } 
public virtual Person? Person { get; set; } 
public virtual Role? Role { get; set; } 
public virtual GovernanceBody? GovernanceBody { get; set; } 
public virtual Organization? Organization { get; set; } 

    public static RoleAssignment FromRequest(RoleAssignmentRequest request) {
        return new RoleAssignment {
            Id = request.Id,
            EffectiveFrom = request.EffectiveFrom,
            EffectiveTo = request.EffectiveTo,
        };
    }
}
