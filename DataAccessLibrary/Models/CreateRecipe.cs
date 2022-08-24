using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models;

public class CreateRecipe
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    public long? Calorie { get; set; } = null;
    [Required(ErrorMessage = "Ingredient is required")]
    public string? Ingredient { get; set; }
    public List<Tags>? TagList { get; set; } = new List<Tags>();
    public string? Image { get; set; }
}

