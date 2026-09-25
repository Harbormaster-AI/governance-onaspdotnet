
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class BusinessUnit
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BusinessunitId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Leader { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<AuditEngagement> Audits { get; set; } = new List<AuditEngagement>();

    public static BusinessUnit FromRequest(BusinessUnitRequest request) {
        return new BusinessUnit {
            Id = request.Id,
            Name = request.Name,
            Leader = request.Leader,
        };
    }
}
