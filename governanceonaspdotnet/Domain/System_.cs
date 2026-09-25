
using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class System_
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? System_Id { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? OwnerDepartment { get; set; } 
public virtual ICollection<DataProcessingActivity> ProcessingActivities { get; set; } = new List<DataProcessingActivity>();
public virtual ICollection<RecordsRepository> RecordsRepositories { get; set; } = new List<RecordsRepository>();
 public virtual SystemType? SystemType { get; set; } 

    public static System_ FromRequest(System_Request request) {
        return new System_ {
            Id = request.Id,
            Name = request.Name,
            OwnerDepartment = request.OwnerDepartment,
            SystemType = request.SystemType,
        };
    }
}
