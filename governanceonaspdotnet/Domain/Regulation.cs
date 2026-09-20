using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Regulation
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RegulationId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Citation { get; set; } 
 public virtual string? Jurisdiction { get; set; } 
 public virtual URL? PublicationUrl { get; set; } 
public virtual ICollection<Obligation> Obligations { get; set; } = new List<Obligation>();
public virtual ICollection<ComplianceProgram> CompliancePrograms { get; set; } = new List<ComplianceProgram>();

    public static Regulation FromRequest(RegulationRequest request) {
        return new Regulation {
            Id = request.Id,
            Name = request.Name,
            Citation = request.Citation,
            Jurisdiction = request.Jurisdiction,
            PublicationUrl = request.PublicationUrl,
        };
    }
}
