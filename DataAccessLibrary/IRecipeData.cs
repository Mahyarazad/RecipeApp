using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface IRecipeData
{
    Task<List<RecipeModel>> GetRecipeList();
    Task InsertRecipe(RecipeModel model);
    Task<RecipeModel?> GetRecipe(Guid id);
}