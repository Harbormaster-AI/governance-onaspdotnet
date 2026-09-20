using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class GovernanceBody
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? GovernancebodyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual URL? CharterUrl { get; set; } 
 public virtual string? Chair { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<RoleAssignment> RoleAssignments { get; set; } = new List<RoleAssignment>();
public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
 public virtual GovernanceBodyType? BodyType { get; set; } 

    public static GovernanceBody FromRequest(GovernanceBodyRequest request) {
        return new GovernanceBody {
            Id = request.Id,
            Name = request.Name,
            CharterUrl = request.CharterUrl,
            Chair = request.Chair,
            BodyType = request.BodyType,
        };
    }
}
