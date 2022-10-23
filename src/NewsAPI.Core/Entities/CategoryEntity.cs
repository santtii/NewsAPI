using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using NewsAPI.SharedKernel;

namespace NewsAPI.Core.Entities;

public class CategoryEntity : EntityBase
{
    [Key]
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;

    public virtual ICollection<NewsEntity> News { get; set; } = new Collection<NewsEntity>();
}
