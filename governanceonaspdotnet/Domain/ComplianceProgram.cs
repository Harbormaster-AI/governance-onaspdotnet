using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class ComplianceProgram
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ComplianceprogramId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Framework { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<ComplianceRequirement> Requirements { get; set; } = new List<ComplianceRequirement>();
public virtual ICollection<Control> Controls { get; set; } = new List<Control>();
public virtual ICollection<Attestation> Attestations { get; set; } = new List<Attestation>();
public virtual ICollection<Regulation> Regulations { get; set; } = new List<Regulation>();
 public virtual ComplianceStatus? Status { get; set; } 

    public static ComplianceProgram FromRequest(ComplianceProgramRequest request) {
        return new ComplianceProgram {
            Id = request.Id,
            Name = request.Name,
            Framework = request.Framework,
            Status = request.Status,
        };
    }
}
