using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class DataSubjectRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DatasubjectrequestId { get; set; } 
 public virtual DateOnly? ReceivedDate { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual string? RequesterCountry { get; set; } 
public virtual Organization? Organization { get; set; } 
public virtual ICollection<DataProcessingActivity> ProcessingActivities { get; set; } = new List<DataProcessingActivity>();
public virtual ICollection<Record_> Records { get; set; } = new List<Record_>();
 public virtual DataSubjectRequestType? RequestType { get; set; } 
 public virtual RequestStatus? Status { get; set; } 

    public static DataSubjectRequest FromRequest(DataSubjectRequestRequest request) {
        return new DataSubjectRequest {
            Id = request.Id,
            ReceivedDate = request.ReceivedDate,
            DueDate = request.DueDate,
            RequesterCountry = request.RequesterCountry,
            RequestType = request.RequestType,
            Status = request.Status,
        };
    }
}
