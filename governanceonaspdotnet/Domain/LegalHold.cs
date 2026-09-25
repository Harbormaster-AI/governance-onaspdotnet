
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class LegalHold
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LegalholdId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Reason { get; set; } 
 public virtual DateOnly? IssuedDate { get; set; } 
 public virtual DateOnly? ReleaseDate { get; set; } 
public virtual ICollection<RecordsRepository> Repositories { get; set; } = new List<RecordsRepository>();
public virtual ICollection<Record_> Records { get; set; } = new List<Record_>();
public virtual Matter? Matter { get; set; } 
 public virtual LegalHoldStatus? HoldStatus { get; set; } 

    public static LegalHold FromRequest(LegalHoldRequest request) {
        return new LegalHold {
            Id = request.Id,
            Name = request.Name,
            Reason = request.Reason,
            IssuedDate = request.IssuedDate,
            ReleaseDate = request.ReleaseDate,
            HoldStatus = request.HoldStatus,
        };
    }
}
