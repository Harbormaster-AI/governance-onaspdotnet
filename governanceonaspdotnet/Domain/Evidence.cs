
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Evidence
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? EvidenceId { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual URL? LocationUrl { get; set; } 
 public virtual DateOnly? ReceivedDate { get; set; } 
public virtual ControlTest_? ControlTest { get; set; } 
public virtual Control? Control { get; set; } 
public virtual Obligation? Obligation { get; set; } 
public virtual AuditWorkpaper? Workpaper { get; set; } 
 public virtual EvidenceType? EvidenceType { get; set; } 

    public static Evidence FromRequest(EvidenceRequest request) {
        return new Evidence {
            Id = request.Id,
            Title = request.Title,
            LocationUrl = request.LocationUrl,
            ReceivedDate = request.ReceivedDate,
            EvidenceType = request.EvidenceType,
        };
    }
}
