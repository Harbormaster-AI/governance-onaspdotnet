using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class Record_
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? Record_Id { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual DateOnly? CreationDate { get; set; } 
public virtual RecordsRepository? Repository { get; set; } 
public virtual RetentionSchedule? RetentionSchedule { get; set; } 
public virtual ICollection<DataProcessingActivity> ProcessingActivities { get; set; } = new List<DataProcessingActivity>();
public virtual ICollection<DataCategory> DataCategories { get; set; } = new List<DataCategory>();
public virtual ICollection<LegalHold> LegalHolds { get; set; } = new List<LegalHold>();
public virtual ICollection<DataSubjectRequest> DataSubjectRequests { get; set; } = new List<DataSubjectRequest>();
 public virtual RecordType? RecordType { get; set; } 
 public virtual DataClassificationLevel? Classification { get; set; } 
 public virtual RecordStatus? Status { get; set; } 

    public static Record_ FromRequest(Record_Request request) {
        return new Record_ {
            Id = request.Id,
            Title = request.Title,
            CreationDate = request.CreationDate,
            RecordType = request.RecordType,
            Classification = request.Classification,
            Status = request.Status,
        };
    }
}
