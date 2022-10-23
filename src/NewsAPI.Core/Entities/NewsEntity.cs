using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NewsAPI.SharedKernel;

namespace NewsAPI.Core.Entities;

public class NewsEntity : EntityBase
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("CategoryEntity")]
    public int? CategoryId { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public bool Visible { get; set; } = false;

    public virtual CategoryEntity? Category { get; set; }
}
