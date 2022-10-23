using NewsAPI.Core.Entities;

namespace NewsAPI.Core.Models;

public class NewsModel
{
    public int NewsId { get; set; }
    public int? CategoryId { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public bool Visible { get; set; } = false;

    public static explicit operator NewsEntity(NewsModel model)
    {
        return new NewsEntity
        {
            Id = model.NewsId,
            CategoryId = model.CategoryId,
            Image = model.Image,
            Title = model.Title,
            Body = model.Body,
            Date = model.Date,
            Visible = model.Visible
        };
    }
}
