using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface ISqlDataAccess
{
    Task<List<T>> LoadData<T, TU>(string sql, TU parameter);
    Task<List<RecipeModel>> LoadRecipe(string sql);
    void SaveData(string sql, RecipeModel parameter);
}