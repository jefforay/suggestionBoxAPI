using System.ComponentModel.DataAnnotations;

namespace SuggestionAPI.Domain;

public class Suggestion
{
    public Suggestion()
    {
    }

    public Suggestion(int id, DateTime dateTimeStart, DateTime dateTimeEnd, string category, string description, string title, int userId, string userName, string eventType)
    {
        Id = id;
        DateTimeStart = dateTimeStart;
        DateTimeEnd = dateTimeEnd;
        Category = category;
        Description = description;
        Title = title;
        UserId = userId;
        UserName = userName;
        EventType = eventType;
    }

    [Display(Name = "id")]
    public int Id { get; set; }

    [Display(Name = "date time")]
    [Required(ErrorMessage = "An date time is required")]
    [DataType(DataType.DateTime)]
    [Range(typeof(DateTime), "01/01/2024", "01/01/2099", ErrorMessage = "Valid dates for the Property {0} between {1} and {2}")]
    public DateTime? DateTimeStart { get; set; }

    [Display(Name = "date time")]
    [Required(ErrorMessage = "An date time is required")]
    [DataType(DataType.DateTime)]
    [Range(typeof(DateTime), "01/01/2024", "01/01/2099", ErrorMessage = "Valid dates for the Property {0} between {1} and {2}")]
    public DateTime? DateTimeEnd { get; set; }

    [Display(Name = "Category")]
    [StringLength(100, MinimumLength = 3)]
    [Required(ErrorMessage = "An category is required")]
    [DataType(DataType.Text)]
    public string? Category { get; set; }

    [Display(Name = "Description")]
    [StringLength(100, MinimumLength = 3)]
    [DataType(DataType.Text)]
    public string? Description { get; set; }

    [Display(Name = "Title")]
    [StringLength(100, MinimumLength = 3)]
    [DataType(DataType.Text)]
    public string? Title { get; set; }

    [Display(Name = "UserId")]
    public int? UserId { get; set; }

    [Display(Name = "UserName")]
    [StringLength(100, MinimumLength = 3)]
    [DataType(DataType.Text)]
    public string? UserName { get; set; }

    [Display(Name = "Event type")]
    [StringLength(100, MinimumLength = 3)]
    [DataType(DataType.Text)]
    public string? EventType { get; set; }
   
}
