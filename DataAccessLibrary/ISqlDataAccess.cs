using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface ISqlDataAccess
{
    Task<RecipeModel> LoadSingleRecipe<U>(string storedProcedure, U parameter);
    Task<List<RecipeModel>> LoadRecipe(string sql);
    void SaveData(string sql, RecipeModel parameter);
}