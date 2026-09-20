using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class AuditProgram
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AuditprogramId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Scope { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<AuditEngagement> Engagements { get; set; } = new List<AuditEngagement>();
 public virtual AuditCycle? Cycle { get; set; } 
 public virtual AuditStatus? Status { get; set; } 

    public static AuditProgram FromRequest(AuditProgramRequest request) {
        return new AuditProgram {
            Id = request.Id,
            Name = request.Name,
            Scope = request.Scope,
            Cycle = request.Cycle,
            Status = request.Status,
        };
    }
}
