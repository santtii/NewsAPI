using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NewsAPI.SharedKernel;

namespace NewsAPI.Core.Entities;

public class CategoryEntity : EntityBase
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual ICollection<NewsEntity> News { get; } = new Collection<NewsEntity>();
}
