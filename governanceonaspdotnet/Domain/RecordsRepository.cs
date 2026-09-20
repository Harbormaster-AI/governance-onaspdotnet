using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class RecordsRepository
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RecordsrepositoryId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Location { get; set; } 
 public virtual string? OwnerDepartment { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<Record_> Records { get; set; } = new List<Record_>();
public virtual ICollection<System_> Systems { get; set; } = new List<System_>();
public virtual ICollection<RetentionSchedule> RetentionSchedules { get; set; } = new List<RetentionSchedule>();
public virtual ICollection<LegalHold> LegalHolds { get; set; } = new List<LegalHold>();
 public virtual RepositoryType? RepositoryType { get; set; } 

    public static RecordsRepository FromRequest(RecordsRepositoryRequest request) {
        return new RecordsRepository {
            Id = request.Id,
            Name = request.Name,
            Location = request.Location,
            OwnerDepartment = request.OwnerDepartment,
            RepositoryType = request.RepositoryType,
        };
    }
}
