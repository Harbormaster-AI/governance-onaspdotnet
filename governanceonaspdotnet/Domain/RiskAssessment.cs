
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class RiskAssessment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RiskassessmentId { get; set; } 
 public virtual DateOnly? AssessmentDate { get; set; } 
 public virtual string? Assessor { get; set; } 
 public virtual string? Summary { get; set; } 
public virtual Risk? Risk { get; set; } 
 public virtual AssessmentType? AssessmentType { get; set; } 

    public static RiskAssessment FromRequest(RiskAssessmentRequest request) {
        return new RiskAssessment {
            Id = request.Id,
            AssessmentDate = request.AssessmentDate,
            Assessor = request.Assessor,
            Summary = request.Summary,
            AssessmentType = request.AssessmentType,
        };
    }
}
