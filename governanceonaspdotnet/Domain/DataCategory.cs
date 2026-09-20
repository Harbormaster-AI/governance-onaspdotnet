using governanceonaspdotnet.Contracts;

namespace governanceonaspdotnet.Domain;

public class DataCategory
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DatacategoryId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Description { get; set; } 
public virtual ICollection<DataProcessingActivity> ProcessingActivities { get; set; } = new List<DataProcessingActivity>();
public virtual ICollection<Record_> Records { get; set; } = new List<Record_>();
public virtual ICollection<DataBreach> DataBreaches { get; set; } = new List<DataBreach>();
 public virtual DataClassificationLevel? Classification { get; set; } 

    public static DataCategory FromRequest(DataCategoryRequest request) {
        return new DataCategory {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Classification = request.Classification,
        };
    }
}
