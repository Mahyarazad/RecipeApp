using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DataAccessLibrary.Models;

public class RecipeModel
{
    public Guid Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; }
    public long Calorie { get; set; }
    [Required, MaxLength(1000)]
    public string Ingredient { get; set; }
    public List<Tags> TagList { get; set; } = new List<Tags>();
    public string? Image { get; set; }
}   

public class Tags
{
    public Guid TagId { get; set; }
    public Guid RecipeId { get; set; }
    [Required, MaxLength(30)]
    public string Tag { get; set; }
    
}

