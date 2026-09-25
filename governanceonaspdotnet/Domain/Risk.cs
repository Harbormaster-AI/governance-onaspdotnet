
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Risk
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RiskId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual int? InherentRiskScore { get; set; } 
 public virtual int? ResidualRiskScore { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<Control> Controls { get; set; } = new List<Control>();
public virtual ICollection<RiskAssessment> Assessments { get; set; } = new List<RiskAssessment>();
public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
public virtual ICollection<AuditFinding> Findings { get; set; } = new List<AuditFinding>();
 public virtual RiskCategory? Category { get; set; } 
 public virtual RiskImpact? Impact { get; set; } 
 public virtual RiskLikelihood? Likelihood { get; set; } 
 public virtual RiskStatus? Status { get; set; } 

    public static Risk FromRequest(RiskRequest request) {
        return new Risk {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            InherentRiskScore = request.InherentRiskScore,
            ResidualRiskScore = request.ResidualRiskScore,
            Category = request.Category,
            Impact = request.Impact,
            Likelihood = request.Likelihood,
            Status = request.Status,
        };
    }
}
