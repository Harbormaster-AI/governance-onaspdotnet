
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Procedure
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ProcedureId { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual string? VersionLabel { get; set; } 
public virtual Policy? Policy { get; set; } 
public virtual ICollection<Control> Controls { get; set; } = new List<Control>();
 public virtual DocumentStatus? Status { get; set; } 

    public static Procedure FromRequest(ProcedureRequest request) {
        return new Procedure {
            Id = request.Id,
            Title = request.Title,
            VersionLabel = request.VersionLabel,
            Status = request.Status,
        };
    }
}
