using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Attestation
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AttestationId { get; set; } 
 public virtual string? Statement { get; set; } 
 public virtual string? Attestor { get; set; } 
 public virtual DateOnly? DateSigned { get; set; } 
public virtual Control? Control { get; set; } 
public virtual Policy? Policy { get; set; } 
public virtual ComplianceProgram? ComplianceProgram { get; set; } 
 public virtual AttestationResult? Result { get; set; } 

    public static Attestation FromRequest(AttestationRequest request) {
        return new Attestation {
            Id = request.Id,
            Statement = request.Statement,
            Attestor = request.Attestor,
            DateSigned = request.DateSigned,
            Result = request.Result,
        };
    }
}
