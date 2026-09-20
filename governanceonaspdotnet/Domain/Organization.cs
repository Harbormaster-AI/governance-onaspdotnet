using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Organization
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? OrganizationId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? Jurisdiction { get; set; } 
 public virtual string? IndustrySector { get; set; } 
public virtual ICollection<GovernanceBody> GovernanceBodies { get; set; } = new List<GovernanceBody>();
public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
public virtual ICollection<Risk> Risks { get; set; } = new List<Risk>();
public virtual ICollection<ThirdParty> ThirdParties { get; set; } = new List<ThirdParty>();
public virtual ICollection<RecordsRepository> RecordsRepositories { get; set; } = new List<RecordsRepository>();
public virtual ICollection<DataProcessingActivity> DataProcessingActivities { get; set; } = new List<DataProcessingActivity>();
public virtual ICollection<ComplianceProgram> CompliancePrograms { get; set; } = new List<ComplianceProgram>();
public virtual ICollection<AuditProgram> AuditPrograms { get; set; } = new List<AuditProgram>();
public virtual ICollection<BusinessUnit> BusinessUnits { get; set; } = new List<BusinessUnit>();
public virtual ICollection<Matter> Matters { get; set; } = new List<Matter>();
public virtual ICollection<DataBreach> DataBreaches { get; set; } = new List<DataBreach>();

    public static Organization FromRequest(OrganizationRequest request) {
        return new Organization {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            Jurisdiction = request.Jurisdiction,
            IndustrySector = request.IndustrySector,
        };
    }
}
