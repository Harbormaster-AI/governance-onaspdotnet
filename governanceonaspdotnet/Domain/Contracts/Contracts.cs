using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Contracts;

public class IdentifierRequest
{
    public Guid Id { get; set; }
}

public class AssociationRequest
{
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
}

public class MultipleAssociationRequest
{
    public Guid ParentId { get; set; }
    public List<Guid> ChildIds { get; set; } = new();
}

public class OrganizationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? Jurisdiction { get; set; } 
 public virtual string? IndustrySector { get; set; } 
}

public class OrganizationResponse : OrganizationRequest {
    public static OrganizationResponse FromModel(Organization model) {
        return new OrganizationResponse {
            Id = model.Id,
            Name = model.Name,
            LegalName = model.LegalName,
            Jurisdiction = model.Jurisdiction,
            IndustrySector = model.IndustrySector,
        };
    }
}

public class GovernanceBodyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual URL? CharterUrl { get; set; } 
 public virtual string? Chair { get; set; } 
 public virtual GovernanceBodyType? BodyType { get; set; } 
}

public class GovernanceBodyResponse : GovernanceBodyRequest {
    public static GovernanceBodyResponse FromModel(GovernanceBody model) {
        return new GovernanceBodyResponse {
            Id = model.Id,
            Name = model.Name,
            CharterUrl = model.CharterUrl,
            Chair = model.Chair,
            BodyType = model.BodyType,
        };
    }
}

public class PersonRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual EmailAddress? Email { get; set; } 
 public virtual string? Department { get; set; } 
}

public class PersonResponse : PersonRequest {
    public static PersonResponse FromModel(Person model) {
        return new PersonResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Department = model.Department,
        };
    }
}

public class RoleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Responsibility { get; set; } 
}

public class RoleResponse : RoleRequest {
    public static RoleResponse FromModel(Role model) {
        return new RoleResponse {
            Id = model.Id,
            Name = model.Name,
            Responsibility = model.Responsibility,
        };
    }
}

public class RoleAssignmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? EffectiveFrom { get; set; } 
 public virtual DateOnly? EffectiveTo { get; set; } 
}

public class RoleAssignmentResponse : RoleAssignmentRequest {
    public static RoleAssignmentResponse FromModel(RoleAssignment model) {
        return new RoleAssignmentResponse {
            Id = model.Id,
            EffectiveFrom = model.EffectiveFrom,
            EffectiveTo = model.EffectiveTo,
        };
    }
}

public class PolicyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual string? VersionLabel { get; set; } 
 public virtual DateOnly? ApprovalDate { get; set; } 
 public virtual DateOnly? NextReviewDate { get; set; } 
 public virtual URL? DocumentUrl { get; set; } 
 public virtual PolicyType? PolicyType { get; set; } 
 public virtual DocumentStatus? Status { get; set; } 
}

public class PolicyResponse : PolicyRequest {
    public static PolicyResponse FromModel(Policy model) {
        return new PolicyResponse {
            Id = model.Id,
            Title = model.Title,
            VersionLabel = model.VersionLabel,
            ApprovalDate = model.ApprovalDate,
            NextReviewDate = model.NextReviewDate,
            DocumentUrl = model.DocumentUrl,
            PolicyType = model.PolicyType,
            Status = model.Status,
        };
    }
}

public class ProcedureRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual string? VersionLabel { get; set; } 
 public virtual DocumentStatus? Status { get; set; } 
}

public class ProcedureResponse : ProcedureRequest {
    public static ProcedureResponse FromModel(Procedure model) {
        return new ProcedureResponse {
            Id = model.Id,
            Title = model.Title,
            VersionLabel = model.VersionLabel,
            Status = model.Status,
        };
    }
}

public class RegulationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Citation { get; set; } 
 public virtual string? Jurisdiction { get; set; } 
 public virtual URL? PublicationUrl { get; set; } 
}

public class RegulationResponse : RegulationRequest {
    public static RegulationResponse FromModel(Regulation model) {
        return new RegulationResponse {
            Id = model.Id,
            Name = model.Name,
            Citation = model.Citation,
            Jurisdiction = model.Jurisdiction,
            PublicationUrl = model.PublicationUrl,
        };
    }
}

public class ObligationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ReferenceNumber { get; set; } 
 public virtual string? DescriptionText { get; set; } 
 public virtual ObligationType? ObligationType { get; set; } 
 public virtual ControlFrequency? ReviewFrequency { get; set; } 
}

public class ObligationResponse : ObligationRequest {
    public static ObligationResponse FromModel(Obligation model) {
        return new ObligationResponse {
            Id = model.Id,
            ReferenceNumber = model.ReferenceNumber,
            DescriptionText = model.DescriptionText,
            ObligationType = model.ObligationType,
            ReviewFrequency = model.ReviewFrequency,
        };
    }
}

public class ControlRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Objective { get; set; } 
 public virtual string? OwnerDepartment { get; set; } 
 public virtual ControlType? ControlType { get; set; } 
 public virtual ControlFrequency? Frequency { get; set; } 
 public virtual ControlStatus? Status { get; set; } 
}

public class ControlResponse : ControlRequest {
    public static ControlResponse FromModel(Control model) {
        return new ControlResponse {
            Id = model.Id,
            Name = model.Name,
            Objective = model.Objective,
            OwnerDepartment = model.OwnerDepartment,
            ControlType = model.ControlType,
            Frequency = model.Frequency,
            Status = model.Status,
        };
    }
}

public class ControlTest_Request {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual DateOnly? TestPeriodStart { get; set; } 
 public virtual DateOnly? TestPeriodEnd { get; set; } 
 public virtual int? SampleSize { get; set; } 
 public virtual TestType? TestType { get; set; } 
 public virtual ControlEffectiveness? Effectiveness { get; set; } 
 public virtual TestStatus? Status { get; set; } 
}

public class ControlTest_Response : ControlTest_Request {
    public static ControlTest_Response FromModel(ControlTest_ model) {
        return new ControlTest_Response {
            Id = model.Id,
            Name = model.Name,
            TestPeriodStart = model.TestPeriodStart,
            TestPeriodEnd = model.TestPeriodEnd,
            SampleSize = model.SampleSize,
            TestType = model.TestType,
            Effectiveness = model.Effectiveness,
            Status = model.Status,
        };
    }
}

public class EvidenceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual URL? LocationUrl { get; set; } 
 public virtual DateOnly? ReceivedDate { get; set; } 
 public virtual EvidenceType? EvidenceType { get; set; } 
}

public class EvidenceResponse : EvidenceRequest {
    public static EvidenceResponse FromModel(Evidence model) {
        return new EvidenceResponse {
            Id = model.Id,
            Title = model.Title,
            LocationUrl = model.LocationUrl,
            ReceivedDate = model.ReceivedDate,
            EvidenceType = model.EvidenceType,
        };
    }
}

public class RiskRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual int? InherentRiskScore { get; set; } 
 public virtual int? ResidualRiskScore { get; set; } 
 public virtual RiskCategory? Category { get; set; } 
 public virtual RiskImpact? Impact { get; set; } 
 public virtual RiskLikelihood? Likelihood { get; set; } 
 public virtual RiskStatus? Status { get; set; } 
}

public class RiskResponse : RiskRequest {
    public static RiskResponse FromModel(Risk model) {
        return new RiskResponse {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            InherentRiskScore = model.InherentRiskScore,
            ResidualRiskScore = model.ResidualRiskScore,
            Category = model.Category,
            Impact = model.Impact,
            Likelihood = model.Likelihood,
            Status = model.Status,
        };
    }
}

public class RiskAssessmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? AssessmentDate { get; set; } 
 public virtual string? Assessor { get; set; } 
 public virtual string? Summary { get; set; } 
 public virtual AssessmentType? AssessmentType { get; set; } 
}

public class RiskAssessmentResponse : RiskAssessmentRequest {
    public static RiskAssessmentResponse FromModel(RiskAssessment model) {
        return new RiskAssessmentResponse {
            Id = model.Id,
            AssessmentDate = model.AssessmentDate,
            Assessor = model.Assessor,
            Summary = model.Summary,
            AssessmentType = model.AssessmentType,
        };
    }
}

public class ComplianceProgramRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Framework { get; set; } 
 public virtual ComplianceStatus? Status { get; set; } 
}

public class ComplianceProgramResponse : ComplianceProgramRequest {
    public static ComplianceProgramResponse FromModel(ComplianceProgram model) {
        return new ComplianceProgramResponse {
            Id = model.Id,
            Name = model.Name,
            Framework = model.Framework,
            Status = model.Status,
        };
    }
}

public class ComplianceRequirementRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Source { get; set; } 
 public virtual string? Citation { get; set; } 
 public virtual Applicability? Applicability { get; set; } 
 public virtual ComplianceStatus? Status { get; set; } 
}

public class ComplianceRequirementResponse : ComplianceRequirementRequest {
    public static ComplianceRequirementResponse FromModel(ComplianceRequirement model) {
        return new ComplianceRequirementResponse {
            Id = model.Id,
            Name = model.Name,
            Source = model.Source,
            Citation = model.Citation,
            Applicability = model.Applicability,
            Status = model.Status,
        };
    }
}

public class AttestationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Statement { get; set; } 
 public virtual string? Attestor { get; set; } 
 public virtual DateOnly? DateSigned { get; set; } 
 public virtual AttestationResult? Result { get; set; } 
}

public class AttestationResponse : AttestationRequest {
    public static AttestationResponse FromModel(Attestation model) {
        return new AttestationResponse {
            Id = model.Id,
            Statement = model.Statement,
            Attestor = model.Attestor,
            DateSigned = model.DateSigned,
            Result = model.Result,
        };
    }
}

public class AuditProgramRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Scope { get; set; } 
 public virtual AuditCycle? Cycle { get; set; } 
 public virtual AuditStatus? Status { get; set; } 
}

public class AuditProgramResponse : AuditProgramRequest {
    public static AuditProgramResponse FromModel(AuditProgram model) {
        return new AuditProgramResponse {
            Id = model.Id,
            Name = model.Name,
            Scope = model.Scope,
            Cycle = model.Cycle,
            Status = model.Status,
        };
    }
}

public class AuditEngagementRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual AuditStatus? Status { get; set; } 
}

public class AuditEngagementResponse : AuditEngagementRequest {
    public static AuditEngagementResponse FromModel(AuditEngagement model) {
        return new AuditEngagementResponse {
            Id = model.Id,
            Title = model.Title,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Status = model.Status,
        };
    }
}

public class AuditWorkpaperRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? WorkpaperRef { get; set; } 
 public virtual string? Subject { get; set; } 
 public virtual URL? WorkpaperUrl { get; set; } 
}

public class AuditWorkpaperResponse : AuditWorkpaperRequest {
    public static AuditWorkpaperResponse FromModel(AuditWorkpaper model) {
        return new AuditWorkpaperResponse {
            Id = model.Id,
            WorkpaperRef = model.WorkpaperRef,
            Subject = model.Subject,
            WorkpaperUrl = model.WorkpaperUrl,
        };
    }
}

public class AuditFindingRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual FindingSeverity? Severity { get; set; } 
 public virtual FindingStatus? Status { get; set; } 
}

public class AuditFindingResponse : AuditFindingRequest {
    public static AuditFindingResponse FromModel(AuditFinding model) {
        return new AuditFindingResponse {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            DueDate = model.DueDate,
            Severity = model.Severity,
            Status = model.Status,
        };
    }
}

public class CorrectiveActionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ActionTitle { get; set; } 
 public virtual string? Owner { get; set; } 
 public virtual DateOnly? TargetDate { get; set; } 
 public virtual ActionStatus? Status { get; set; } 
}

public class CorrectiveActionResponse : CorrectiveActionRequest {
    public static CorrectiveActionResponse FromModel(CorrectiveAction model) {
        return new CorrectiveActionResponse {
            Id = model.Id,
            ActionTitle = model.ActionTitle,
            Owner = model.Owner,
            TargetDate = model.TargetDate,
            Status = model.Status,
        };
    }
}

public class IssueRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual DateOnly? OpenedDate { get; set; } 
 public virtual DateOnly? ClosedDate { get; set; } 
 public virtual IssueType? IssueType { get; set; } 
 public virtual Priority? Priority { get; set; } 
 public virtual IssueStatus? Status { get; set; } 
}

public class IssueResponse : IssueRequest {
    public static IssueResponse FromModel(Issue model) {
        return new IssueResponse {
            Id = model.Id,
            Title = model.Title,
            OpenedDate = model.OpenedDate,
            ClosedDate = model.ClosedDate,
            IssueType = model.IssueType,
            Priority = model.Priority,
            Status = model.Status,
        };
    }
}

public class BusinessUnitRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Leader { get; set; } 
}

public class BusinessUnitResponse : BusinessUnitRequest {
    public static BusinessUnitResponse FromModel(BusinessUnit model) {
        return new BusinessUnitResponse {
            Id = model.Id,
            Name = model.Name,
            Leader = model.Leader,
        };
    }
}

public class DataProcessingActivityRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Purpose { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual LawfulBasis? LawfulBasis { get; set; } 
}

public class DataProcessingActivityResponse : DataProcessingActivityRequest {
    public static DataProcessingActivityResponse FromModel(DataProcessingActivity model) {
        return new DataProcessingActivityResponse {
            Id = model.Id,
            Name = model.Name,
            Purpose = model.Purpose,
            StartDate = model.StartDate,
            LawfulBasis = model.LawfulBasis,
        };
    }
}

public class DataCategoryRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual DataClassificationLevel? Classification { get; set; } 
}

public class DataCategoryResponse : DataCategoryRequest {
    public static DataCategoryResponse FromModel(DataCategory model) {
        return new DataCategoryResponse {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Classification = model.Classification,
        };
    }
}

public class System_Request {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? OwnerDepartment { get; set; } 
 public virtual SystemType? SystemType { get; set; } 
}

public class System_Response : System_Request {
    public static System_Response FromModel(System_ model) {
        return new System_Response {
            Id = model.Id,
            Name = model.Name,
            OwnerDepartment = model.OwnerDepartment,
            SystemType = model.SystemType,
        };
    }
}

public class PrivacyNoticeRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual string? Audience { get; set; } 
 public virtual string? VersionLabel { get; set; } 
 public virtual DateOnly? PublicationDate { get; set; } 
 public virtual URL? PublicationUrl { get; set; } 
 public virtual DocumentStatus? Status { get; set; } 
}

public class PrivacyNoticeResponse : PrivacyNoticeRequest {
    public static PrivacyNoticeResponse FromModel(PrivacyNotice model) {
        return new PrivacyNoticeResponse {
            Id = model.Id,
            Title = model.Title,
            Audience = model.Audience,
            VersionLabel = model.VersionLabel,
            PublicationDate = model.PublicationDate,
            PublicationUrl = model.PublicationUrl,
            Status = model.Status,
        };
    }
}

public class DataSubjectRequestRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? ReceivedDate { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual string? RequesterCountry { get; set; } 
 public virtual DataSubjectRequestType? RequestType { get; set; } 
 public virtual RequestStatus? Status { get; set; } 
}

public class DataSubjectRequestResponse : DataSubjectRequestRequest {
    public static DataSubjectRequestResponse FromModel(DataSubjectRequest model) {
        return new DataSubjectRequestResponse {
            Id = model.Id,
            ReceivedDate = model.ReceivedDate,
            DueDate = model.DueDate,
            RequesterCountry = model.RequesterCountry,
            RequestType = model.RequestType,
            Status = model.Status,
        };
    }
}

public class RecordsRepositoryRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Location { get; set; } 
 public virtual string? OwnerDepartment { get; set; } 
 public virtual RepositoryType? RepositoryType { get; set; } 
}

public class RecordsRepositoryResponse : RecordsRepositoryRequest {
    public static RecordsRepositoryResponse FromModel(RecordsRepository model) {
        return new RecordsRepositoryResponse {
            Id = model.Id,
            Name = model.Name,
            Location = model.Location,
            OwnerDepartment = model.OwnerDepartment,
            RepositoryType = model.RepositoryType,
        };
    }
}

public class Record_Request {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual DateOnly? CreationDate { get; set; } 
 public virtual RecordType? RecordType { get; set; } 
 public virtual DataClassificationLevel? Classification { get; set; } 
 public virtual RecordStatus? Status { get; set; } 
}

public class Record_Response : Record_Request {
    public static Record_Response FromModel(Record_ model) {
        return new Record_Response {
            Id = model.Id,
            Title = model.Title,
            CreationDate = model.CreationDate,
            RecordType = model.RecordType,
            Classification = model.Classification,
            Status = model.Status,
        };
    }
}

public class RetentionScheduleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual int? RetentionPeriodMonths { get; set; } 
 public virtual RetentionTrigger? RetentionTrigger { get; set; } 
 public virtual DispositionAction? DispositionAction { get; set; } 
 public virtual RetentionStatus? Status { get; set; } 
}

public class RetentionScheduleResponse : RetentionScheduleRequest {
    public static RetentionScheduleResponse FromModel(RetentionSchedule model) {
        return new RetentionScheduleResponse {
            Id = model.Id,
            Name = model.Name,
            RetentionPeriodMonths = model.RetentionPeriodMonths,
            RetentionTrigger = model.RetentionTrigger,
            DispositionAction = model.DispositionAction,
            Status = model.Status,
        };
    }
}

public class DispositionReviewRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? ReviewDate { get; set; } 
 public virtual string? Reviewer { get; set; } 
 public virtual string? Notes { get; set; } 
 public virtual DispositionOutcome? Outcome { get; set; } 
}

public class DispositionReviewResponse : DispositionReviewRequest {
    public static DispositionReviewResponse FromModel(DispositionReview model) {
        return new DispositionReviewResponse {
            Id = model.Id,
            ReviewDate = model.ReviewDate,
            Reviewer = model.Reviewer,
            Notes = model.Notes,
            Outcome = model.Outcome,
        };
    }
}

public class LegalHoldRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Reason { get; set; } 
 public virtual DateOnly? IssuedDate { get; set; } 
 public virtual DateOnly? ReleaseDate { get; set; } 
 public virtual LegalHoldStatus? HoldStatus { get; set; } 
}

public class LegalHoldResponse : LegalHoldRequest {
    public static LegalHoldResponse FromModel(LegalHold model) {
        return new LegalHoldResponse {
            Id = model.Id,
            Name = model.Name,
            Reason = model.Reason,
            IssuedDate = model.IssuedDate,
            ReleaseDate = model.ReleaseDate,
            HoldStatus = model.HoldStatus,
        };
    }
}

public class MatterRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? MatterName { get; set; } 
 public virtual string? LeadCounsel { get; set; } 
 public virtual MatterType? MatterType { get; set; } 
 public virtual MatterStatus? Status { get; set; } 
}

public class MatterResponse : MatterRequest {
    public static MatterResponse FromModel(Matter model) {
        return new MatterResponse {
            Id = model.Id,
            MatterName = model.MatterName,
            LeadCounsel = model.LeadCounsel,
            MatterType = model.MatterType,
            Status = model.Status,
        };
    }
}

public class ThirdPartyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Country { get; set; } 
 public virtual EmailAddress? ContactEmail { get; set; } 
 public virtual ThirdPartyType? ThirdPartyType { get; set; } 
 public virtual VendorCriticality? Criticality { get; set; } 
}

public class ThirdPartyResponse : ThirdPartyRequest {
    public static ThirdPartyResponse FromModel(ThirdParty model) {
        return new ThirdPartyResponse {
            Id = model.Id,
            Name = model.Name,
            Country = model.Country,
            ContactEmail = model.ContactEmail,
            ThirdPartyType = model.ThirdPartyType,
            Criticality = model.Criticality,
        };
    }
}

public class ThirdPartyAssessmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? AssessmentDate { get; set; } 
 public virtual string? Assessor { get; set; } 
 public virtual AssessmentType? AssessmentType { get; set; } 
 public virtual AssessmentResult? Result { get; set; } 
}

public class ThirdPartyAssessmentResponse : ThirdPartyAssessmentRequest {
    public static ThirdPartyAssessmentResponse FromModel(ThirdPartyAssessment model) {
        return new ThirdPartyAssessmentResponse {
            Id = model.Id,
            AssessmentDate = model.AssessmentDate,
            Assessor = model.Assessor,
            AssessmentType = model.AssessmentType,
            Result = model.Result,
        };
    }
}

public class ContractRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
 public virtual DateOnly? ExpiryDate { get; set; } 
 public virtual URL? RepositoryUrl { get; set; } 
 public virtual ContractStatus? Status { get; set; } 
}

public class ContractResponse : ContractRequest {
    public static ContractResponse FromModel(Contract model) {
        return new ContractResponse {
            Id = model.Id,
            Title = model.Title,
            EffectiveDate = model.EffectiveDate,
            ExpiryDate = model.ExpiryDate,
            RepositoryUrl = model.RepositoryUrl,
            Status = model.Status,
        };
    }
}

public class Exception_Request {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Title { get; set; } 
 public virtual string? Justification { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual ExceptionType? ExceptionType { get; set; } 
 public virtual ExceptionStatus? Status { get; set; } 
}

public class Exception_Response : Exception_Request {
    public static Exception_Response FromModel(Exception_ model) {
        return new Exception_Response {
            Id = model.Id,
            Title = model.Title,
            Justification = model.Justification,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            ExceptionType = model.ExceptionType,
            Status = model.Status,
        };
    }
}

public class ConsentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? SubjectIdentifier { get; set; } 
 public virtual DateOnly? CaptureDate { get; set; } 
 public virtual DateOnly? ExpiryDate { get; set; } 
 public virtual ConsentType? ConsentType { get; set; } 
 public virtual ConsentStatus? Status { get; set; } 
}

public class ConsentResponse : ConsentRequest {
    public static ConsentResponse FromModel(Consent model) {
        return new ConsentResponse {
            Id = model.Id,
            SubjectIdentifier = model.SubjectIdentifier,
            CaptureDate = model.CaptureDate,
            ExpiryDate = model.ExpiryDate,
            ConsentType = model.ConsentType,
            Status = model.Status,
        };
    }
}

public class DataBreachRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? IncidentDate { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual int? RecordsAffected { get; set; } 
 public virtual bool? NotificationRequired { get; set; } 
 public virtual BreachSeverity? Severity { get; set; } 
 public virtual IncidentStatus? Status { get; set; } 
}

public class DataBreachResponse : DataBreachRequest {
    public static DataBreachResponse FromModel(DataBreach model) {
        return new DataBreachResponse {
            Id = model.Id,
            IncidentDate = model.IncidentDate,
            Description = model.Description,
            RecordsAffected = model.RecordsAffected,
            NotificationRequired = model.NotificationRequired,
            Severity = model.Severity,
            Status = model.Status,
        };
    }
}

