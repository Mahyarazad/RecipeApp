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

    public bool ShowDialogCreate { get; private set; } = false;
    public CreateRecipe NewRecipe { get; private set; } = null!;
    public List<RecipeModel> RecipeList { get; set; } = null!;
    
    public void ShowCreateDialog(Guid Id)
    {
        ShowDialogCreate = true;
        
        if (Id == Guid.Empty)
        {
            NewRecipe = new CreateRecipe();
        }
        else
        {
            var query = _recipeData.GetRecipe(Id).Result;
            NewRecipe = new CreateRecipe
            {
                Id = query.Id,
                Name = query.Name,
                Calorie = query.Calorie,
                Ingredient = query.Ingredient,
                Image = query.Image,
                TagList = query.TagList!
            };
        }

    }

    public void RemoveDialog()
    {
        ShowDialogCreate = false;
        NewRecipe = new CreateRecipe();
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

        ShowDialogCreate = false;
       
        if (_recipeData.InsertRecipe(recipe).Result)
        {
            RecipeList.Add(recipe);
            _toastService.ShowSuccess("Your recipe added successfully");
        }
        else
        {
            _toastService.ShowError("Duplicate Record!, Please chose another name");
        }
        
        NewRecipe = new CreateRecipe();
    }

    public void RemoveTag(string value)
    {
        var target = NewRecipe.TagList!.FirstOrDefault(x => x.Tag == value);
        NewRecipe.TagList!.Remove(target!);
    }

   
}