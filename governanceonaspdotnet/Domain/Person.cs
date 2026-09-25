
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Person
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PersonId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual EmailAddress? Email { get; set; } 
 public virtual string? Department { get; set; } 
public virtual ICollection<RoleAssignment> RoleAssignments { get; set; } = new List<RoleAssignment>();
public virtual ICollection<Policy> OwnedPolicies { get; set; } = new List<Policy>();
public virtual ICollection<CorrectiveAction> CorrectiveActions { get; set; } = new List<CorrectiveAction>();

    public static Person FromRequest(PersonRequest request) {
        return new Person {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Department = request.Department,
        };
    }
}
