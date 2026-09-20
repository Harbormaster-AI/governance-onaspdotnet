using governanceonaspdotnet.Api;
using governanceonaspdotnet.Persistence;
using governanceonaspdotnet.Service;

using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();


var dbEngineEnvironment = builder.Configuration["DB_TYPE"];

string dbEngine;
string dbUserName;
string dbPassword;
string dbName;
string dbHost;
string dbPort;

if (string.IsNullOrWhiteSpace(dbEngineEnvironment))
{
    // Generation-time configuration
    dbEngine = "mysql";
    dbUserName = "postgres";
    dbPassword = "no_password";
    dbName = "testDb";
    dbHost = "localhost";
    dbPort = "5432";
}
else
{
    // Runtime environment configuration
    dbEngine = dbEngineEnvironment;
    dbUserName = builder.Configuration["DB_USER_NAME"]
        ?? throw new InvalidOperationException("DB_USER_NAME is required when DB_ENGINE is provided.");

    dbPassword = builder.Configuration["DB_PASSWORD"]
        ?? throw new InvalidOperationException("DB_PASSWORD is required when DB_ENGINE is provided.");

    dbName = builder.Configuration["DB_NAME"]
        ?? throw new InvalidOperationException("DB_NAME is required when DB_ENGINE is provided.");

    dbHost = builder.Configuration["DB_HOST"]
        ?? throw new InvalidOperationException("DB_HOST is required when DB_ENGINE is provided.");

    dbPort = builder.Configuration["DB_PORT"]
        ?? throw new InvalidOperationException("DB_PORT is required when DB_ENGINE is provided.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString =
        $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUserName};Password={dbPassword}";

    if (dbEngine.Equals("mysql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("mariadb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlite(connectionString);
    }
    else if (dbEngine.Equals("sqlserver", StringComparison.OrdinalIgnoreCase) ||
             dbEngine.Equals("azuresql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlServer(connectionString);
    }
    else if (dbEngine.Equals("oracle", StringComparison.OrdinalIgnoreCase))
    {
        options.UseOracle(connectionString);
    }
    else if (dbEngine.Equals("mongodb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMongoDB(connectionString);
    }
    else if (dbEngine.Equals("inmemory", StringComparison.OrdinalIgnoreCase))
    {
        options.UseInMemoryDatabase("governanceonaspdotnet");
    }
    else if (dbEngine.Equals("cosmosdb", StringComparison.OrdinalIgnoreCase))
    {
        var cosmosEndpoint = builder.Configuration["DB_ENDPOINT"]
            ?? throw new InvalidOperationException(
                "DB_ENDPOINT is required for Cosmos DB.");

        var cosmosKey = builder.Configuration["DB_KEY"]
            ?? throw new InvalidOperationException(
                "DB_KEY is required for Cosmos DB.");

        options.UseCosmos(
            cosmosEndpoint,
            cosmosKey,
            dbName);
    }
    else if (dbEngine.Equals("postgres", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        throw new InvalidOperationException(
            "Unsupported or missing database engine configuration.");
    }
    // etc.
});

builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IGovernanceBodyRepository, GovernanceBodyRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleAssignmentRepository, RoleAssignmentRepository>();
builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
builder.Services.AddScoped<IProcedureRepository, ProcedureRepository>();
builder.Services.AddScoped<IRegulationRepository, RegulationRepository>();
builder.Services.AddScoped<IObligationRepository, ObligationRepository>();
builder.Services.AddScoped<IControlRepository, ControlRepository>();
builder.Services.AddScoped<IControlTest_Repository, ControlTest_Repository>();
builder.Services.AddScoped<IEvidenceRepository, EvidenceRepository>();
builder.Services.AddScoped<IRiskRepository, RiskRepository>();
builder.Services.AddScoped<IRiskAssessmentRepository, RiskAssessmentRepository>();
builder.Services.AddScoped<IComplianceProgramRepository, ComplianceProgramRepository>();
builder.Services.AddScoped<IComplianceRequirementRepository, ComplianceRequirementRepository>();
builder.Services.AddScoped<IAttestationRepository, AttestationRepository>();
builder.Services.AddScoped<IAuditProgramRepository, AuditProgramRepository>();
builder.Services.AddScoped<IAuditEngagementRepository, AuditEngagementRepository>();
builder.Services.AddScoped<IAuditWorkpaperRepository, AuditWorkpaperRepository>();
builder.Services.AddScoped<IAuditFindingRepository, AuditFindingRepository>();
builder.Services.AddScoped<ICorrectiveActionRepository, CorrectiveActionRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<IBusinessUnitRepository, BusinessUnitRepository>();
builder.Services.AddScoped<IDataProcessingActivityRepository, DataProcessingActivityRepository>();
builder.Services.AddScoped<IDataCategoryRepository, DataCategoryRepository>();
builder.Services.AddScoped<ISystem_Repository, System_Repository>();
builder.Services.AddScoped<IPrivacyNoticeRepository, PrivacyNoticeRepository>();
builder.Services.AddScoped<IDataSubjectRequestRepository, DataSubjectRequestRepository>();
builder.Services.AddScoped<IRecordsRepositoryRepository, RecordsRepositoryRepository>();
builder.Services.AddScoped<IRecord_Repository, Record_Repository>();
builder.Services.AddScoped<IRetentionScheduleRepository, RetentionScheduleRepository>();
builder.Services.AddScoped<IDispositionReviewRepository, DispositionReviewRepository>();
builder.Services.AddScoped<ILegalHoldRepository, LegalHoldRepository>();
builder.Services.AddScoped<IMatterRepository, MatterRepository>();
builder.Services.AddScoped<IThirdPartyRepository, ThirdPartyRepository>();
builder.Services.AddScoped<IThirdPartyAssessmentRepository, ThirdPartyAssessmentRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IException_Repository, Exception_Repository>();
builder.Services.AddScoped<IConsentRepository, ConsentRepository>();
builder.Services.AddScoped<IDataBreachRepository, DataBreachRepository>();

builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IGovernanceBodyService, GovernanceBodyService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleAssignmentService, RoleAssignmentService>();
builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<IProcedureService, ProcedureService>();
builder.Services.AddScoped<IRegulationService, RegulationService>();
builder.Services.AddScoped<IObligationService, ObligationService>();
builder.Services.AddScoped<IControlService, ControlService>();
builder.Services.AddScoped<IControlTest_Service, ControlTest_Service>();
builder.Services.AddScoped<IEvidenceService, EvidenceService>();
builder.Services.AddScoped<IRiskService, RiskService>();
builder.Services.AddScoped<IRiskAssessmentService, RiskAssessmentService>();
builder.Services.AddScoped<IComplianceProgramService, ComplianceProgramService>();
builder.Services.AddScoped<IComplianceRequirementService, ComplianceRequirementService>();
builder.Services.AddScoped<IAttestationService, AttestationService>();
builder.Services.AddScoped<IAuditProgramService, AuditProgramService>();
builder.Services.AddScoped<IAuditEngagementService, AuditEngagementService>();
builder.Services.AddScoped<IAuditWorkpaperService, AuditWorkpaperService>();
builder.Services.AddScoped<IAuditFindingService, AuditFindingService>();
builder.Services.AddScoped<ICorrectiveActionService, CorrectiveActionService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IBusinessUnitService, BusinessUnitService>();
builder.Services.AddScoped<IDataProcessingActivityService, DataProcessingActivityService>();
builder.Services.AddScoped<IDataCategoryService, DataCategoryService>();
builder.Services.AddScoped<ISystem_Service, System_Service>();
builder.Services.AddScoped<IPrivacyNoticeService, PrivacyNoticeService>();
builder.Services.AddScoped<IDataSubjectRequestService, DataSubjectRequestService>();
builder.Services.AddScoped<IRecordsRepositoryService, RecordsRepositoryService>();
builder.Services.AddScoped<IRecord_Service, Record_Service>();
builder.Services.AddScoped<IRetentionScheduleService, RetentionScheduleService>();
builder.Services.AddScoped<IDispositionReviewService, DispositionReviewService>();
builder.Services.AddScoped<ILegalHoldService, LegalHoldService>();
builder.Services.AddScoped<IMatterService, MatterService>();
builder.Services.AddScoped<IThirdPartyService, ThirdPartyService>();
builder.Services.AddScoped<IThirdPartyAssessmentService, ThirdPartyAssessmentService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IException_Service, Exception_Service>();
builder.Services.AddScoped<IConsentService, ConsentService>();
builder.Services.AddScoped<IDataBreachService, DataBreachService>();

var app = builder.Build();

// Health endpoint
app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Testing uses a shared SQLite file; wipe it so unique indexes (e.g. email) don't fail on re-runs.
    if (app.Environment.IsEnvironment("Testing"))
    {
        db.Database.EnsureDeleted();
    }

    // EnsureCreated does not alter an existing schema. If you changed relationships locally,
    // recreate the MySQL database (docker compose down -v && docker compose up -d).
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapOrganizationEndpoints();
app.MapGovernanceBodyEndpoints();
app.MapPersonEndpoints();
app.MapRoleEndpoints();
app.MapRoleAssignmentEndpoints();
app.MapPolicyEndpoints();
app.MapProcedureEndpoints();
app.MapRegulationEndpoints();
app.MapObligationEndpoints();
app.MapControlEndpoints();
app.MapControlTest_Endpoints();
app.MapEvidenceEndpoints();
app.MapRiskEndpoints();
app.MapRiskAssessmentEndpoints();
app.MapComplianceProgramEndpoints();
app.MapComplianceRequirementEndpoints();
app.MapAttestationEndpoints();
app.MapAuditProgramEndpoints();
app.MapAuditEngagementEndpoints();
app.MapAuditWorkpaperEndpoints();
app.MapAuditFindingEndpoints();
app.MapCorrectiveActionEndpoints();
app.MapIssueEndpoints();
app.MapBusinessUnitEndpoints();
app.MapDataProcessingActivityEndpoints();
app.MapDataCategoryEndpoints();
app.MapSystem_Endpoints();
app.MapPrivacyNoticeEndpoints();
app.MapDataSubjectRequestEndpoints();
app.MapRecordsRepositoryEndpoints();
app.MapRecord_Endpoints();
app.MapRetentionScheduleEndpoints();
app.MapDispositionReviewEndpoints();
app.MapLegalHoldEndpoints();
app.MapMatterEndpoints();
app.MapThirdPartyEndpoints();
app.MapThirdPartyAssessmentEndpoints();
app.MapContractEndpoints();
app.MapException_Endpoints();
app.MapConsentEndpoints();
app.MapDataBreachEndpoints();

app.Run();

public partial class Program { }

