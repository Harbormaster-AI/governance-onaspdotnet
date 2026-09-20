using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Policy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PolicyId { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual string? VersionLabel { get; set; } 
 public virtual DateOnly? ApprovalDate { get; set; } 
 public virtual DateOnly? NextReviewDate { get; set; } 
 public virtual URL? DocumentUrl { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<Person> Owners { get; set; } = new List<Person>();
public virtual ICollection<ComplianceRequirement> RelatedRequirements { get; set; } = new List<ComplianceRequirement>();
public virtual ICollection<Control> Controls { get; set; } = new List<Control>();
public virtual ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();
public virtual ICollection<Exception_> Exceptions { get; set; } = new List<Exception_>();
public virtual ICollection<Attestation> Attestations { get; set; } = new List<Attestation>();
 public virtual PolicyType? PolicyType { get; set; } 
 public virtual DocumentStatus? Status { get; set; } 

    public static Policy FromRequest(PolicyRequest request) {
        return new Policy {
            Id = request.Id,
            Title = request.Title,
            VersionLabel = request.VersionLabel,
            ApprovalDate = request.ApprovalDate,
            NextReviewDate = request.NextReviewDate,
            DocumentUrl = request.DocumentUrl,
            PolicyType = request.PolicyType,
            Status = request.Status,
        };
    }
}
