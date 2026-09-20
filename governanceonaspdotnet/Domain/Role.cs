using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Role
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RoleId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Responsibility { get; set; } 
public virtual ICollection<RoleAssignment> Assignments { get; set; } = new List<RoleAssignment>();

    public static Role FromRequest(RoleRequest request) {
        return new Role {
            Id = request.Id,
            Name = request.Name,
            Responsibility = request.Responsibility,
        };
    }
}
