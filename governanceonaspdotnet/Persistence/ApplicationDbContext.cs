using Microsoft.EntityFrameworkCore;

using governanceonaspdotnet.Domain;

namespace governanceonaspdotnet.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<Organization> Organizations => Set<Organization>();
public DbSet<GovernanceBody> GovernanceBodys => Set<GovernanceBody>();
public DbSet<Person> Persons => Set<Person>();
public DbSet<Role> Roles => Set<Role>();
public DbSet<RoleAssignment> RoleAssignments => Set<RoleAssignment>();
public DbSet<Policy> Policys => Set<Policy>();
public DbSet<Procedure> Procedures => Set<Procedure>();
public DbSet<Regulation> Regulations => Set<Regulation>();
public DbSet<Obligation> Obligations => Set<Obligation>();
public DbSet<Control> Controls => Set<Control>();
public DbSet<ControlTest_> ControlTest_s => Set<ControlTest_>();
public DbSet<Evidence> Evidences => Set<Evidence>();
public DbSet<Risk> Risks => Set<Risk>();
public DbSet<RiskAssessment> RiskAssessments => Set<RiskAssessment>();
public DbSet<ComplianceProgram> CompliancePrograms => Set<ComplianceProgram>();
public DbSet<ComplianceRequirement> ComplianceRequirements => Set<ComplianceRequirement>();
public DbSet<Attestation> Attestations => Set<Attestation>();
public DbSet<AuditProgram> AuditPrograms => Set<AuditProgram>();
public DbSet<AuditEngagement> AuditEngagements => Set<AuditEngagement>();
public DbSet<AuditWorkpaper> AuditWorkpapers => Set<AuditWorkpaper>();
public DbSet<AuditFinding> AuditFindings => Set<AuditFinding>();
public DbSet<CorrectiveAction> CorrectiveActions => Set<CorrectiveAction>();
public DbSet<Issue> Issues => Set<Issue>();
public DbSet<BusinessUnit> BusinessUnits => Set<BusinessUnit>();
public DbSet<DataProcessingActivity> DataProcessingActivitys => Set<DataProcessingActivity>();
public DbSet<DataCategory> DataCategorys => Set<DataCategory>();
public DbSet<System_> System_s => Set<System_>();
public DbSet<PrivacyNotice> PrivacyNotices => Set<PrivacyNotice>();
public DbSet<DataSubjectRequest> DataSubjectRequests => Set<DataSubjectRequest>();
public DbSet<RecordsRepository> RecordsRepositorys => Set<RecordsRepository>();
public DbSet<Record_> Record_s => Set<Record_>();
public DbSet<RetentionSchedule> RetentionSchedules => Set<RetentionSchedule>();
public DbSet<DispositionReview> DispositionReviews => Set<DispositionReview>();
public DbSet<LegalHold> LegalHolds => Set<LegalHold>();
public DbSet<Matter> Matters => Set<Matter>();
public DbSet<ThirdParty> ThirdPartys => Set<ThirdParty>();
public DbSet<ThirdPartyAssessment> ThirdPartyAssessments => Set<ThirdPartyAssessment>();
public DbSet<Contract> Contracts => Set<Contract>();
public DbSet<Exception_> Exception_s => Set<Exception_>();
public DbSet<Consent> Consents => Set<Consent>();
public DbSet<DataBreach> DataBreachs => Set<DataBreach>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Organization has one or more GovernanceBodies of type GovernanceBody
        modelBuilder.Entity<GovernanceBody>()
            .HasOne<Organization>()
            .WithMany(parent => parent.GovernanceBodies)
            .HasForeignKey("GovernanceBodiesId");

        // Organization has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<Organization>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("PoliciesId");

        // Organization has one or more Risks of type Risk
        modelBuilder.Entity<Risk>()
            .HasOne<Organization>()
            .WithMany(parent => parent.Risks)
            .HasForeignKey("RisksId");

        // Organization has one or more ThirdParties of type ThirdParty
        modelBuilder.Entity<ThirdParty>()
            .HasOne<Organization>()
            .WithMany(parent => parent.ThirdParties)
            .HasForeignKey("ThirdPartiesId");

        // Organization has one or more RecordsRepositories of type RecordsRepository
        modelBuilder.Entity<RecordsRepository>()
            .HasOne<Organization>()
            .WithMany(parent => parent.RecordsRepositories)
            .HasForeignKey("RecordsRepositoriesId");

        // Organization has one or more DataProcessingActivities of type DataProcessingActivity
        modelBuilder.Entity<DataProcessingActivity>()
            .HasOne<Organization>()
            .WithMany(parent => parent.DataProcessingActivities)
            .HasForeignKey("DataProcessingActivitiesId");

        // Organization has one or more CompliancePrograms of type ComplianceProgram
        modelBuilder.Entity<ComplianceProgram>()
            .HasOne<Organization>()
            .WithMany(parent => parent.CompliancePrograms)
            .HasForeignKey("ComplianceProgramsId");

        // Organization has one or more AuditPrograms of type AuditProgram
        modelBuilder.Entity<AuditProgram>()
            .HasOne<Organization>()
            .WithMany(parent => parent.AuditPrograms)
            .HasForeignKey("AuditProgramsId");

        // Organization has one or more BusinessUnits of type BusinessUnit
        modelBuilder.Entity<BusinessUnit>()
            .HasOne<Organization>()
            .WithMany(parent => parent.BusinessUnits)
            .HasForeignKey("BusinessUnitsId");

        // Organization has one or more Matters of type Matter
        modelBuilder.Entity<Matter>()
            .HasOne<Organization>()
            .WithMany(parent => parent.Matters)
            .HasForeignKey("MattersId");

        // Organization has one or more DataBreaches of type DataBreach
        modelBuilder.Entity<DataBreach>()
            .HasOne<Organization>()
            .WithMany(parent => parent.DataBreaches)
            .HasForeignKey("DataBreachesId");

        // GovernanceBody has one Organization of type Organization
        modelBuilder.Entity<GovernanceBody>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // GovernanceBody has one or more RoleAssignments of type RoleAssignment
        modelBuilder.Entity<RoleAssignment>()
            .HasOne<GovernanceBody>()
            .WithMany(parent => parent.RoleAssignments)
            .HasForeignKey("RoleAssignmentsId");

        // GovernanceBody has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<GovernanceBody>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("PoliciesId");


        // Person has one or more RoleAssignments of type RoleAssignment
        modelBuilder.Entity<RoleAssignment>()
            .HasOne<Person>()
            .WithMany(parent => parent.RoleAssignments)
            .HasForeignKey("RoleAssignmentsId");

        // Person has one or more OwnedPolicies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<Person>()
            .WithMany(parent => parent.OwnedPolicies)
            .HasForeignKey("OwnedPoliciesId");

        // Person has one or more CorrectiveActions of type CorrectiveAction
        modelBuilder.Entity<CorrectiveAction>()
            .HasOne<Person>()
            .WithMany(parent => parent.CorrectiveActions)
            .HasForeignKey("CorrectiveActionsId");


        // Role has one or more Assignments of type RoleAssignment
        modelBuilder.Entity<RoleAssignment>()
            .HasOne<Role>()
            .WithMany(parent => parent.Assignments)
            .HasForeignKey("AssignmentsId");

        // RoleAssignment has one Person of type Person
        modelBuilder.Entity<RoleAssignment>()
            .HasOne(x => x.Person)
            .WithMany()
            .HasForeignKey("PersonId");

        // RoleAssignment has one Role of type Role
        modelBuilder.Entity<RoleAssignment>()
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey("RoleId");

        // RoleAssignment has one GovernanceBody of type GovernanceBody
        modelBuilder.Entity<RoleAssignment>()
            .HasOne(x => x.GovernanceBody)
            .WithMany()
            .HasForeignKey("GovernanceBodyId");

        // RoleAssignment has one Organization of type Organization
        modelBuilder.Entity<RoleAssignment>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // Policy has one Organization of type Organization
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // Policy has one or more Owners of type Person
        modelBuilder.Entity<Person>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Owners)
            .HasForeignKey("OwnersId");

        // Policy has one or more RelatedRequirements of type ComplianceRequirement
        modelBuilder.Entity<ComplianceRequirement>()
            .HasOne<Policy>()
            .WithMany(parent => parent.RelatedRequirements)
            .HasForeignKey("RelatedRequirementsId");

        // Policy has one or more Controls of type Control
        modelBuilder.Entity<Control>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Controls)
            .HasForeignKey("ControlsId");

        // Policy has one or more Procedures of type Procedure
        modelBuilder.Entity<Procedure>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Procedures)
            .HasForeignKey("ProceduresId");

        // Policy has one or more Exceptions of type Exception_
        modelBuilder.Entity<Exception_>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Exceptions)
            .HasForeignKey("ExceptionsId");

        // Policy has one or more Attestations of type Attestation
        modelBuilder.Entity<Attestation>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Attestations)
            .HasForeignKey("AttestationsId");

        // Procedure has one Policy of type Policy
        modelBuilder.Entity<Procedure>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");


        // Procedure has one or more Controls of type Control
        modelBuilder.Entity<Control>()
            .HasOne<Procedure>()
            .WithMany(parent => parent.Controls)
            .HasForeignKey("ControlsId");


        // Regulation has one or more Obligations of type Obligation
        modelBuilder.Entity<Obligation>()
            .HasOne<Regulation>()
            .WithMany(parent => parent.Obligations)
            .HasForeignKey("ObligationsId");

        // Regulation has one or more CompliancePrograms of type ComplianceProgram
        modelBuilder.Entity<ComplianceProgram>()
            .HasOne<Regulation>()
            .WithMany(parent => parent.CompliancePrograms)
            .HasForeignKey("ComplianceProgramsId");

        // Obligation has one Regulation of type Regulation
        modelBuilder.Entity<Obligation>()
            .HasOne(x => x.Regulation)
            .WithMany()
            .HasForeignKey("RegulationId");


        // Obligation has one or more Controls of type Control
        modelBuilder.Entity<Control>()
            .HasOne<Obligation>()
            .WithMany(parent => parent.Controls)
            .HasForeignKey("ControlsId");

        // Obligation has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<Obligation>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("PoliciesId");

        // Obligation has one or more Contracts of type Contract
        modelBuilder.Entity<Contract>()
            .HasOne<Obligation>()
            .WithMany(parent => parent.Contracts)
            .HasForeignKey("ContractsId");

        // Control has one Policy of type Policy
        modelBuilder.Entity<Control>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");


        // Control has one or more ControlTests of type ControlTest_
        modelBuilder.Entity<ControlTest_>()
            .HasOne<Control>()
            .WithMany(parent => parent.ControlTests)
            .HasForeignKey("ControlTestsId");

        // Control has one or more Evidence of type Evidence
        modelBuilder.Entity<Evidence>()
            .HasOne<Control>()
            .WithMany(parent => parent.Evidence)
            .HasForeignKey("EvidenceId");

        // Control has one or more Risks of type Risk
        modelBuilder.Entity<Risk>()
            .HasOne<Control>()
            .WithMany(parent => parent.Risks)
            .HasForeignKey("RisksId");

        // Control has one or more Obligations of type Obligation
        modelBuilder.Entity<Obligation>()
            .HasOne<Control>()
            .WithMany(parent => parent.Obligations)
            .HasForeignKey("ObligationsId");

        // Control has one or more Procedures of type Procedure
        modelBuilder.Entity<Procedure>()
            .HasOne<Control>()
            .WithMany(parent => parent.Procedures)
            .HasForeignKey("ProceduresId");

        // Control has one or more Issues of type Issue
        modelBuilder.Entity<Issue>()
            .HasOne<Control>()
            .WithMany(parent => parent.Issues)
            .HasForeignKey("IssuesId");

        // ControlTest_ has one Control of type Control
        modelBuilder.Entity<ControlTest_>()
            .HasOne(x => x.Control)
            .WithMany()
            .HasForeignKey("ControlId");

        // ControlTest_ has one Engagement of type AuditEngagement
        modelBuilder.Entity<ControlTest_>()
            .HasOne(x => x.Engagement)
            .WithMany()
            .HasForeignKey("EngagementId");


        // ControlTest_ has one or more Evidence of type Evidence
        modelBuilder.Entity<Evidence>()
            .HasOne<ControlTest_>()
            .WithMany(parent => parent.Evidence)
            .HasForeignKey("EvidenceId");

        // Evidence has one ControlTest of type ControlTest_
        modelBuilder.Entity<Evidence>()
            .HasOne(x => x.ControlTest)
            .WithMany()
            .HasForeignKey("ControlTestId");

        // Evidence has one Control of type Control
        modelBuilder.Entity<Evidence>()
            .HasOne(x => x.Control)
            .WithMany()
            .HasForeignKey("ControlId");

        // Evidence has one Obligation of type Obligation
        modelBuilder.Entity<Evidence>()
            .HasOne(x => x.Obligation)
            .WithMany()
            .HasForeignKey("ObligationId");

        // Evidence has one Workpaper of type AuditWorkpaper
        modelBuilder.Entity<Evidence>()
            .HasOne(x => x.Workpaper)
            .WithMany()
            .HasForeignKey("WorkpaperId");


        // Risk has one Organization of type Organization
        modelBuilder.Entity<Risk>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // Risk has one or more Controls of type Control
        modelBuilder.Entity<Control>()
            .HasOne<Risk>()
            .WithMany(parent => parent.Controls)
            .HasForeignKey("ControlsId");

        // Risk has one or more Assessments of type RiskAssessment
        modelBuilder.Entity<RiskAssessment>()
            .HasOne<Risk>()
            .WithMany(parent => parent.Assessments)
            .HasForeignKey("AssessmentsId");

        // Risk has one or more Issues of type Issue
        modelBuilder.Entity<Issue>()
            .HasOne<Risk>()
            .WithMany(parent => parent.Issues)
            .HasForeignKey("IssuesId");

        // Risk has one or more Findings of type AuditFinding
        modelBuilder.Entity<AuditFinding>()
            .HasOne<Risk>()
            .WithMany(parent => parent.Findings)
            .HasForeignKey("FindingsId");

        // RiskAssessment has one Risk of type Risk
        modelBuilder.Entity<RiskAssessment>()
            .HasOne(x => x.Risk)
            .WithMany()
            .HasForeignKey("RiskId");


        // ComplianceProgram has one Organization of type Organization
        modelBuilder.Entity<ComplianceProgram>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // ComplianceProgram has one or more Requirements of type ComplianceRequirement
        modelBuilder.Entity<ComplianceRequirement>()
            .HasOne<ComplianceProgram>()
            .WithMany(parent => parent.Requirements)
            .HasForeignKey("RequirementsId");

        // ComplianceProgram has one or more Controls of type Control
        modelBuilder.Entity<Control>()
            .HasOne<ComplianceProgram>()
            .WithMany(parent => parent.Controls)
            .HasForeignKey("ControlsId");

        // ComplianceProgram has one or more Attestations of type Attestation
        modelBuilder.Entity<Attestation>()
            .HasOne<ComplianceProgram>()
            .WithMany(parent => parent.Attestations)
            .HasForeignKey("AttestationsId");

        // ComplianceProgram has one or more Regulations of type Regulation
        modelBuilder.Entity<Regulation>()
            .HasOne<ComplianceProgram>()
            .WithMany(parent => parent.Regulations)
            .HasForeignKey("RegulationsId");

        // ComplianceRequirement has one ComplianceProgram of type ComplianceProgram
        modelBuilder.Entity<ComplianceRequirement>()
            .HasOne(x => x.ComplianceProgram)
            .WithMany()
            .HasForeignKey("ComplianceProgramId");


        // ComplianceRequirement has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<ComplianceRequirement>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("PoliciesId");

        // ComplianceRequirement has one or more Controls of type Control
        modelBuilder.Entity<Control>()
            .HasOne<ComplianceRequirement>()
            .WithMany(parent => parent.Controls)
            .HasForeignKey("ControlsId");

        // ComplianceRequirement has one or more Obligations of type Obligation
        modelBuilder.Entity<Obligation>()
            .HasOne<ComplianceRequirement>()
            .WithMany(parent => parent.Obligations)
            .HasForeignKey("ObligationsId");

        // Attestation has one Control of type Control
        modelBuilder.Entity<Attestation>()
            .HasOne(x => x.Control)
            .WithMany()
            .HasForeignKey("ControlId");

        // Attestation has one Policy of type Policy
        modelBuilder.Entity<Attestation>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");

        // Attestation has one ComplianceProgram of type ComplianceProgram
        modelBuilder.Entity<Attestation>()
            .HasOne(x => x.ComplianceProgram)
            .WithMany()
            .HasForeignKey("ComplianceProgramId");


        // AuditProgram has one Organization of type Organization
        modelBuilder.Entity<AuditProgram>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // AuditProgram has one or more Engagements of type AuditEngagement
        modelBuilder.Entity<AuditEngagement>()
            .HasOne<AuditProgram>()
            .WithMany(parent => parent.Engagements)
            .HasForeignKey("EngagementsId");

        // AuditEngagement has one AuditProgram of type AuditProgram
        modelBuilder.Entity<AuditEngagement>()
            .HasOne(x => x.AuditProgram)
            .WithMany()
            .HasForeignKey("AuditProgramId");


        // AuditEngagement has one or more BusinessUnits of type BusinessUnit
        modelBuilder.Entity<BusinessUnit>()
            .HasOne<AuditEngagement>()
            .WithMany(parent => parent.BusinessUnits)
            .HasForeignKey("BusinessUnitsId");

        // AuditEngagement has one or more ControlTests of type ControlTest_
        modelBuilder.Entity<ControlTest_>()
            .HasOne<AuditEngagement>()
            .WithMany(parent => parent.ControlTests)
            .HasForeignKey("ControlTestsId");

        // AuditEngagement has one or more Workpapers of type AuditWorkpaper
        modelBuilder.Entity<AuditWorkpaper>()
            .HasOne<AuditEngagement>()
            .WithMany(parent => parent.Workpapers)
            .HasForeignKey("WorkpapersId");

        // AuditEngagement has one or more Findings of type AuditFinding
        modelBuilder.Entity<AuditFinding>()
            .HasOne<AuditEngagement>()
            .WithMany(parent => parent.Findings)
            .HasForeignKey("FindingsId");

        // AuditWorkpaper has one Engagement of type AuditEngagement
        modelBuilder.Entity<AuditWorkpaper>()
            .HasOne(x => x.Engagement)
            .WithMany()
            .HasForeignKey("EngagementId");


        // AuditWorkpaper has one or more Evidence of type Evidence
        modelBuilder.Entity<Evidence>()
            .HasOne<AuditWorkpaper>()
            .WithMany(parent => parent.Evidence)
            .HasForeignKey("EvidenceId");

        // AuditWorkpaper has one or more Findings of type AuditFinding
        modelBuilder.Entity<AuditFinding>()
            .HasOne<AuditWorkpaper>()
            .WithMany(parent => parent.Findings)
            .HasForeignKey("FindingsId");

        // AuditFinding has one Engagement of type AuditEngagement
        modelBuilder.Entity<AuditFinding>()
            .HasOne(x => x.Engagement)
            .WithMany()
            .HasForeignKey("EngagementId");

        // AuditFinding has one Workpaper of type AuditWorkpaper
        modelBuilder.Entity<AuditFinding>()
            .HasOne(x => x.Workpaper)
            .WithMany()
            .HasForeignKey("WorkpaperId");


        // AuditFinding has one or more CorrectiveActions of type CorrectiveAction
        modelBuilder.Entity<CorrectiveAction>()
            .HasOne<AuditFinding>()
            .WithMany(parent => parent.CorrectiveActions)
            .HasForeignKey("CorrectiveActionsId");

        // AuditFinding has one or more RelatedRisks of type Risk
        modelBuilder.Entity<Risk>()
            .HasOne<AuditFinding>()
            .WithMany(parent => parent.RelatedRisks)
            .HasForeignKey("RelatedRisksId");

        // AuditFinding has one or more RelatedControls of type Control
        modelBuilder.Entity<Control>()
            .HasOne<AuditFinding>()
            .WithMany(parent => parent.RelatedControls)
            .HasForeignKey("RelatedControlsId");

        // AuditFinding has one or more Issues of type Issue
        modelBuilder.Entity<Issue>()
            .HasOne<AuditFinding>()
            .WithMany(parent => parent.Issues)
            .HasForeignKey("IssuesId");

        // CorrectiveAction has one Finding of type AuditFinding
        modelBuilder.Entity<CorrectiveAction>()
            .HasOne(x => x.Finding)
            .WithMany()
            .HasForeignKey("FindingId");

        // CorrectiveAction has one Issue of type Issue
        modelBuilder.Entity<CorrectiveAction>()
            .HasOne(x => x.Issue)
            .WithMany()
            .HasForeignKey("IssueId");


        // Issue has one Risk of type Risk
        modelBuilder.Entity<Issue>()
            .HasOne(x => x.Risk)
            .WithMany()
            .HasForeignKey("RiskId");

        // Issue has one Finding of type AuditFinding
        modelBuilder.Entity<Issue>()
            .HasOne(x => x.Finding)
            .WithMany()
            .HasForeignKey("FindingId");

        // Issue has one Control of type Control
        modelBuilder.Entity<Issue>()
            .HasOne(x => x.Control)
            .WithMany()
            .HasForeignKey("ControlId");


        // Issue has one or more CorrectiveActions of type CorrectiveAction
        modelBuilder.Entity<CorrectiveAction>()
            .HasOne<Issue>()
            .WithMany(parent => parent.CorrectiveActions)
            .HasForeignKey("CorrectiveActionsId");

        // BusinessUnit has one Organization of type Organization
        modelBuilder.Entity<BusinessUnit>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // BusinessUnit has one or more Audits of type AuditEngagement
        modelBuilder.Entity<AuditEngagement>()
            .HasOne<BusinessUnit>()
            .WithMany(parent => parent.Audits)
            .HasForeignKey("AuditsId");

        // DataProcessingActivity has one Organization of type Organization
        modelBuilder.Entity<DataProcessingActivity>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // DataProcessingActivity has one or more DataCategories of type DataCategory
        modelBuilder.Entity<DataCategory>()
            .HasOne<DataProcessingActivity>()
            .WithMany(parent => parent.DataCategories)
            .HasForeignKey("DataCategoriesId");

        // DataProcessingActivity has one or more Systems of type System_
        modelBuilder.Entity<System_>()
            .HasOne<DataProcessingActivity>()
            .WithMany(parent => parent.Systems)
            .HasForeignKey("SystemsId");

        // DataProcessingActivity has one or more Records of type Record_
        modelBuilder.Entity<Record_>()
            .HasOne<DataProcessingActivity>()
            .WithMany(parent => parent.Records)
            .HasForeignKey("RecordsId");

        // DataProcessingActivity has one or more PrivacyNotices of type PrivacyNotice
        modelBuilder.Entity<PrivacyNotice>()
            .HasOne<DataProcessingActivity>()
            .WithMany(parent => parent.PrivacyNotices)
            .HasForeignKey("PrivacyNoticesId");

        // DataProcessingActivity has one or more ThirdParties of type ThirdParty
        modelBuilder.Entity<ThirdParty>()
            .HasOne<DataProcessingActivity>()
            .WithMany(parent => parent.ThirdParties)
            .HasForeignKey("ThirdPartiesId");

        // DataProcessingActivity has one or more Consents of type Consent
        modelBuilder.Entity<Consent>()
            .HasOne<DataProcessingActivity>()
            .WithMany(parent => parent.Consents)
            .HasForeignKey("ConsentsId");

        // DataProcessingActivity has one or more DataBreaches of type DataBreach
        modelBuilder.Entity<DataBreach>()
            .HasOne<DataProcessingActivity>()
            .WithMany(parent => parent.DataBreaches)
            .HasForeignKey("DataBreachesId");

        // DataProcessingActivity has one or more DataSubjectRequests of type DataSubjectRequest
        modelBuilder.Entity<DataSubjectRequest>()
            .HasOne<DataProcessingActivity>()
            .WithMany(parent => parent.DataSubjectRequests)
            .HasForeignKey("DataSubjectRequestsId");


        // DataCategory has one or more ProcessingActivities of type DataProcessingActivity
        modelBuilder.Entity<DataProcessingActivity>()
            .HasOne<DataCategory>()
            .WithMany(parent => parent.ProcessingActivities)
            .HasForeignKey("ProcessingActivitiesId");

        // DataCategory has one or more Records of type Record_
        modelBuilder.Entity<Record_>()
            .HasOne<DataCategory>()
            .WithMany(parent => parent.Records)
            .HasForeignKey("RecordsId");

        // DataCategory has one or more DataBreaches of type DataBreach
        modelBuilder.Entity<DataBreach>()
            .HasOne<DataCategory>()
            .WithMany(parent => parent.DataBreaches)
            .HasForeignKey("DataBreachesId");


        // System_ has one or more ProcessingActivities of type DataProcessingActivity
        modelBuilder.Entity<DataProcessingActivity>()
            .HasOne<System_>()
            .WithMany(parent => parent.ProcessingActivities)
            .HasForeignKey("ProcessingActivitiesId");

        // System_ has one or more RecordsRepositories of type RecordsRepository
        modelBuilder.Entity<RecordsRepository>()
            .HasOne<System_>()
            .WithMany(parent => parent.RecordsRepositories)
            .HasForeignKey("RecordsRepositoriesId");

        // PrivacyNotice has one Organization of type Organization
        modelBuilder.Entity<PrivacyNotice>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // PrivacyNotice has one or more ProcessingActivities of type DataProcessingActivity
        modelBuilder.Entity<DataProcessingActivity>()
            .HasOne<PrivacyNotice>()
            .WithMany(parent => parent.ProcessingActivities)
            .HasForeignKey("ProcessingActivitiesId");

        // PrivacyNotice has one or more Consents of type Consent
        modelBuilder.Entity<Consent>()
            .HasOne<PrivacyNotice>()
            .WithMany(parent => parent.Consents)
            .HasForeignKey("ConsentsId");

        // DataSubjectRequest has one Organization of type Organization
        modelBuilder.Entity<DataSubjectRequest>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // DataSubjectRequest has one or more ProcessingActivities of type DataProcessingActivity
        modelBuilder.Entity<DataProcessingActivity>()
            .HasOne<DataSubjectRequest>()
            .WithMany(parent => parent.ProcessingActivities)
            .HasForeignKey("ProcessingActivitiesId");

        // DataSubjectRequest has one or more Records of type Record_
        modelBuilder.Entity<Record_>()
            .HasOne<DataSubjectRequest>()
            .WithMany(parent => parent.Records)
            .HasForeignKey("RecordsId");

        // RecordsRepository has one Organization of type Organization
        modelBuilder.Entity<RecordsRepository>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // RecordsRepository has one or more Records of type Record_
        modelBuilder.Entity<Record_>()
            .HasOne<RecordsRepository>()
            .WithMany(parent => parent.Records)
            .HasForeignKey("RecordsId");

        // RecordsRepository has one or more Systems of type System_
        modelBuilder.Entity<System_>()
            .HasOne<RecordsRepository>()
            .WithMany(parent => parent.Systems)
            .HasForeignKey("SystemsId");

        // RecordsRepository has one or more RetentionSchedules of type RetentionSchedule
        modelBuilder.Entity<RetentionSchedule>()
            .HasOne<RecordsRepository>()
            .WithMany(parent => parent.RetentionSchedules)
            .HasForeignKey("RetentionSchedulesId");

        // RecordsRepository has one or more LegalHolds of type LegalHold
        modelBuilder.Entity<LegalHold>()
            .HasOne<RecordsRepository>()
            .WithMany(parent => parent.LegalHolds)
            .HasForeignKey("LegalHoldsId");

        // Record_ has one Repository of type RecordsRepository
        modelBuilder.Entity<Record_>()
            .HasOne(x => x.Repository)
            .WithMany()
            .HasForeignKey("RepositoryId");

        // Record_ has one RetentionSchedule of type RetentionSchedule
        modelBuilder.Entity<Record_>()
            .HasOne(x => x.RetentionSchedule)
            .WithMany()
            .HasForeignKey("RetentionScheduleId");


        // Record_ has one or more ProcessingActivities of type DataProcessingActivity
        modelBuilder.Entity<DataProcessingActivity>()
            .HasOne<Record_>()
            .WithMany(parent => parent.ProcessingActivities)
            .HasForeignKey("ProcessingActivitiesId");

        // Record_ has one or more DataCategories of type DataCategory
        modelBuilder.Entity<DataCategory>()
            .HasOne<Record_>()
            .WithMany(parent => parent.DataCategories)
            .HasForeignKey("DataCategoriesId");

        // Record_ has one or more LegalHolds of type LegalHold
        modelBuilder.Entity<LegalHold>()
            .HasOne<Record_>()
            .WithMany(parent => parent.LegalHolds)
            .HasForeignKey("LegalHoldsId");

        // Record_ has one or more DataSubjectRequests of type DataSubjectRequest
        modelBuilder.Entity<DataSubjectRequest>()
            .HasOne<Record_>()
            .WithMany(parent => parent.DataSubjectRequests)
            .HasForeignKey("DataSubjectRequestsId");


        // RetentionSchedule has one or more Repositories of type RecordsRepository
        modelBuilder.Entity<RecordsRepository>()
            .HasOne<RetentionSchedule>()
            .WithMany(parent => parent.Repositories)
            .HasForeignKey("RepositoriesId");

        // RetentionSchedule has one or more Records of type Record_
        modelBuilder.Entity<Record_>()
            .HasOne<RetentionSchedule>()
            .WithMany(parent => parent.Records)
            .HasForeignKey("RecordsId");

        // RetentionSchedule has one or more Exceptions of type Exception_
        modelBuilder.Entity<Exception_>()
            .HasOne<RetentionSchedule>()
            .WithMany(parent => parent.Exceptions)
            .HasForeignKey("ExceptionsId");

        // RetentionSchedule has one or more DispositionReviews of type DispositionReview
        modelBuilder.Entity<DispositionReview>()
            .HasOne<RetentionSchedule>()
            .WithMany(parent => parent.DispositionReviews)
            .HasForeignKey("DispositionReviewsId");

        // DispositionReview has one Record of type Record_
        modelBuilder.Entity<DispositionReview>()
            .HasOne(x => x.Record)
            .WithMany()
            .HasForeignKey("RecordId");

        // DispositionReview has one RetentionSchedule of type RetentionSchedule
        modelBuilder.Entity<DispositionReview>()
            .HasOne(x => x.RetentionSchedule)
            .WithMany()
            .HasForeignKey("RetentionScheduleId");


        // LegalHold has one Matter of type Matter
        modelBuilder.Entity<LegalHold>()
            .HasOne(x => x.Matter)
            .WithMany()
            .HasForeignKey("MatterId");


        // LegalHold has one or more Repositories of type RecordsRepository
        modelBuilder.Entity<RecordsRepository>()
            .HasOne<LegalHold>()
            .WithMany(parent => parent.Repositories)
            .HasForeignKey("RepositoriesId");

        // LegalHold has one or more Records of type Record_
        modelBuilder.Entity<Record_>()
            .HasOne<LegalHold>()
            .WithMany(parent => parent.Records)
            .HasForeignKey("RecordsId");

        // Matter has one Organization of type Organization
        modelBuilder.Entity<Matter>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // Matter has one or more LegalHolds of type LegalHold
        modelBuilder.Entity<LegalHold>()
            .HasOne<Matter>()
            .WithMany(parent => parent.LegalHolds)
            .HasForeignKey("LegalHoldsId");

        // Matter has one or more DataBreaches of type DataBreach
        modelBuilder.Entity<DataBreach>()
            .HasOne<Matter>()
            .WithMany(parent => parent.DataBreaches)
            .HasForeignKey("DataBreachesId");

        // Matter has one or more Contracts of type Contract
        modelBuilder.Entity<Contract>()
            .HasOne<Matter>()
            .WithMany(parent => parent.Contracts)
            .HasForeignKey("ContractsId");

        // ThirdParty has one Organization of type Organization
        modelBuilder.Entity<ThirdParty>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");


        // ThirdParty has one or more ProcessingActivities of type DataProcessingActivity
        modelBuilder.Entity<DataProcessingActivity>()
            .HasOne<ThirdParty>()
            .WithMany(parent => parent.ProcessingActivities)
            .HasForeignKey("ProcessingActivitiesId");

        // ThirdParty has one or more Assessments of type ThirdPartyAssessment
        modelBuilder.Entity<ThirdPartyAssessment>()
            .HasOne<ThirdParty>()
            .WithMany(parent => parent.Assessments)
            .HasForeignKey("AssessmentsId");

        // ThirdParty has one or more Contracts of type Contract
        modelBuilder.Entity<Contract>()
            .HasOne<ThirdParty>()
            .WithMany(parent => parent.Contracts)
            .HasForeignKey("ContractsId");

        // ThirdParty has one or more Obligations of type Obligation
        modelBuilder.Entity<Obligation>()
            .HasOne<ThirdParty>()
            .WithMany(parent => parent.Obligations)
            .HasForeignKey("ObligationsId");

        // ThirdParty has one or more DataBreaches of type DataBreach
        modelBuilder.Entity<DataBreach>()
            .HasOne<ThirdParty>()
            .WithMany(parent => parent.DataBreaches)
            .HasForeignKey("DataBreachesId");

        // ThirdPartyAssessment has one ThirdParty of type ThirdParty
        modelBuilder.Entity<ThirdPartyAssessment>()
            .HasOne(x => x.ThirdParty)
            .WithMany()
            .HasForeignKey("ThirdPartyId");


        // ThirdPartyAssessment has one or more Issues of type Issue
        modelBuilder.Entity<Issue>()
            .HasOne<ThirdPartyAssessment>()
            .WithMany(parent => parent.Issues)
            .HasForeignKey("IssuesId");

        // Contract has one ThirdParty of type ThirdParty
        modelBuilder.Entity<Contract>()
            .HasOne(x => x.ThirdParty)
            .WithMany()
            .HasForeignKey("ThirdPartyId");

        // Contract has one Matter of type Matter
        modelBuilder.Entity<Contract>()
            .HasOne(x => x.Matter)
            .WithMany()
            .HasForeignKey("MatterId");


        // Contract has one or more Obligations of type Obligation
        modelBuilder.Entity<Obligation>()
            .HasOne<Contract>()
            .WithMany(parent => parent.Obligations)
            .HasForeignKey("ObligationsId");

        // Contract has one or more DataProcessingActivities of type DataProcessingActivity
        modelBuilder.Entity<DataProcessingActivity>()
            .HasOne<Contract>()
            .WithMany(parent => parent.DataProcessingActivities)
            .HasForeignKey("DataProcessingActivitiesId");

        // Exception_ has one RetentionSchedule of type RetentionSchedule
        modelBuilder.Entity<Exception_>()
            .HasOne(x => x.RetentionSchedule)
            .WithMany()
            .HasForeignKey("RetentionScheduleId");

        // Exception_ has one Policy of type Policy
        modelBuilder.Entity<Exception_>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");

        // Exception_ has one Control of type Control
        modelBuilder.Entity<Exception_>()
            .HasOne(x => x.Control)
            .WithMany()
            .HasForeignKey("ControlId");

        // Exception_ has one Risk of type Risk
        modelBuilder.Entity<Exception_>()
            .HasOne(x => x.Risk)
            .WithMany()
            .HasForeignKey("RiskId");


        // Consent has one PrivacyNotice of type PrivacyNotice
        modelBuilder.Entity<Consent>()
            .HasOne(x => x.PrivacyNotice)
            .WithMany()
            .HasForeignKey("PrivacyNoticeId");


        // Consent has one or more ProcessingActivities of type DataProcessingActivity
        modelBuilder.Entity<DataProcessingActivity>()
            .HasOne<Consent>()
            .WithMany(parent => parent.ProcessingActivities)
            .HasForeignKey("ProcessingActivitiesId");

        // DataBreach has one Organization of type Organization
        modelBuilder.Entity<DataBreach>()
            .HasOne(x => x.Organization)
            .WithMany()
            .HasForeignKey("OrganizationId");

        // DataBreach has one Matter of type Matter
        modelBuilder.Entity<DataBreach>()
            .HasOne(x => x.Matter)
            .WithMany()
            .HasForeignKey("MatterId");


        // DataBreach has one or more ProcessingActivities of type DataProcessingActivity
        modelBuilder.Entity<DataProcessingActivity>()
            .HasOne<DataBreach>()
            .WithMany(parent => parent.ProcessingActivities)
            .HasForeignKey("ProcessingActivitiesId");

        // DataBreach has one or more DataCategories of type DataCategory
        modelBuilder.Entity<DataCategory>()
            .HasOne<DataBreach>()
            .WithMany(parent => parent.DataCategories)
            .HasForeignKey("DataCategoriesId");

        // DataBreach has one or more ThirdParties of type ThirdParty
        modelBuilder.Entity<ThirdParty>()
            .HasOne<DataBreach>()
            .WithMany(parent => parent.ThirdParties)
            .HasForeignKey("ThirdPartiesId");

    }
}
