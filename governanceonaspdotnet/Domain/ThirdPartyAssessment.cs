
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class ThirdPartyAssessment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ThirdpartyassessmentId { get; set; } 
 public virtual DateOnly? AssessmentDate { get; set; } 
 public virtual string? Assessor { get; set; } 
public virtual ThirdParty? ThirdParty { get; set; } 
public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
 public virtual AssessmentType? AssessmentType { get; set; } 
 public virtual AssessmentResult? Result { get; set; } 

    public static ThirdPartyAssessment FromRequest(ThirdPartyAssessmentRequest request) {
        return new ThirdPartyAssessment {
            Id = request.Id,
            AssessmentDate = request.AssessmentDate,
            Assessor = request.Assessor,
            AssessmentType = request.AssessmentType,
            Result = request.Result,
        };
    }
}
