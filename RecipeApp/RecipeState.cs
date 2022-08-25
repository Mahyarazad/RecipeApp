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
    public List<RecipeModel> RecipeList { get; set; }
    
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

    public async Task RemoveDialog()
    {
        ShowDialogCreate = false;
        NewRecipe = new CreateRecipe();
    }

    public void ConfirmRecipe()
    {

        var recipe = new RecipeModel
        {
            Id = NewRecipe.Id,
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
                RecipeId = recipe.Id,
                NewTag = tag.NewTag
            });

            if (tag.NewTag)
            {
                var target = RecipeList.First(x => x.Id == NewRecipe.Id);
                target.TagList.Add(tag);
                RecipeList.First(x => x.Id == NewRecipe.Id).TagList = target.TagList;
            }
        }


        ShowDialogCreate = false;
        if (NewRecipe.Id==Guid.Empty)
        {
            if (_recipeData.InsertRecipe(recipe).Result)
            {
                RecipeList.Add(recipe);
                _toastService.ShowSuccess("Your recipe added successfully");
            }
            else
            {
                _toastService.ShowError("Duplicate Record!, Please chose another name");
            }
        }
        else
        {
            if (_recipeData.UpdateRecipe(recipe).Result)
            {
                _toastService.ShowSuccess("Your recipe updated successfully");
                
            }
            else
            {
                _toastService.ShowError("Duplicate Record!, Another record with this name exists");
            }
        }
        
        NewRecipe = new CreateRecipe();
    }

    public void DeleteRecipe(Guid id, List<Guid>? tagIdList)
    {

        _recipeData.DeleteRecipe(id, tagIdList);
        RecipeList.Remove(RecipeList.FirstOrDefault(x => x.Id == id));
    }

    public void RemoveTag(string value)
    {
        var target = NewRecipe.TagList!.FirstOrDefault(x => x.Tag == value);
        NewRecipe.TagList!.Remove(target!);
    }
}