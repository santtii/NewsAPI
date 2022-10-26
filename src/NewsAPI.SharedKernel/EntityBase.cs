using System.Text.Json.Serialization;

namespace NewsAPI.SharedKernel;

public abstract class EntityBase    // this can be modified to EntityBase<TId> to support multiple key types (e.g. Guid)
{
    [JsonIgnore]
    public DateTime Created { get; set; } = DateTime.Now;
    [JsonIgnore]
    public DateTime? Updated { get; set; } = DateTime.Now;
}
