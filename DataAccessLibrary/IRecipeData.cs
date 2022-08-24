using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface IRecipeData
{
    Task<List<RecipeModel>> GetRecipeList();
    Task<bool> InsertRecipe(RecipeModel model);
    Task<RecipeModel?> GetRecipe(Guid id);
    Task DeleteTag(Guid id);
}