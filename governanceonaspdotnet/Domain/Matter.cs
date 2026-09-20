using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Matter
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MatterId { get; set; } 
 public virtual string? MatterName { get; set; } 
 public virtual string? LeadCounsel { get; set; } 
public virtual ICollection<LegalHold> LegalHolds { get; set; } = new List<LegalHold>();
public virtual Organization? Organization { get; set; } 
public virtual ICollection<DataBreach> DataBreaches { get; set; } = new List<DataBreach>();
public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
 public virtual MatterType? MatterType { get; set; } 
 public virtual MatterStatus? Status { get; set; } 

    public static Matter FromRequest(MatterRequest request) {
        return new Matter {
            Id = request.Id,
            MatterName = request.MatterName,
            LeadCounsel = request.LeadCounsel,
            MatterType = request.MatterType,
            Status = request.Status,
        };
    }
}
