using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface ISqlDataAccess
{
    Task<RecipeModel> LoadSingleRecipe<TU>(string storedProcedure, TU parameter);
    Task<List<RecipeModel>> LoadRecipe(string sql);
    bool SaveData(string sql, RecipeModel parameter);
    bool UpdateData(string sql, RecipeModel parameter);
    void DeleteData<TU>(string sql, TU parameter);
}