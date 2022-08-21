using Blazored.Toast.Services;
using DataAccessLibrary;
using DataAccessLibrary.Models;

namespace RecipeApp;

public class RecipeState
{
    private readonly IRecipeData _recipeData;
    private readonly IToastService _toastService;


    public RecipeState(IRecipeData recipeData, IToastService toastService)
    {
        _recipeData = recipeData;
        _toastService = toastService;
    }

    public bool ShowDialog { get; private set; }
    public CreateRecipe NewRecipe { get; private set; } = null!;
    public List<RecipeModel> RecipeList { get; set; } = null!;



    public void ShowCreateDialog(CreateRecipe command)
    {
        ShowDialog = true;
        NewRecipe = new CreateRecipe();
    }
    public void RemoveDialog()
    {
        ShowDialog = false;
       
    }

    public void ConfirmRecipe()
    {

        var recipe = new RecipeModel
        {
            Id = Guid.NewGuid(),
            Image = NewRecipe.Image,
            Name = NewRecipe.Name!,
            Ingredient = NewRecipe.Ingredient!,
            Calorie = (long)NewRecipe.Calorie,

        };

        foreach (var tag in NewRecipe.TagList!)
        {
            recipe.TagList.Add(new Tags
            {
                TagId = tag.TagId,
                Tag = tag.Tag,
                RecipeId = recipe.Id
            });
        }

        ShowDialog = false;
        _recipeData.InsertRecipe(recipe);
        RecipeList.Add(recipe);
        _toastService.ShowSuccess("Your recipe added successfully");
        NewRecipe = new CreateRecipe();
    }

    public void RemoveTag(string value)
    {
        var target = NewRecipe.TagList!.FirstOrDefault(x => x.Tag == value);
        NewRecipe.TagList!.Remove(target!);
     }
}