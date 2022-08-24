using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface ISqlDataAccess
{
    Task<RecipeModel> LoadSingleRecipe<U>(string storedProcedure, U parameter);
    Task<List<RecipeModel>> LoadRecipe(string sql);
    bool SaveData(string sql, RecipeModel parameter);
    void DeleteData<U>(string sql, U parameter);
}