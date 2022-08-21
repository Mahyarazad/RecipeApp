using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public class RecipeData : IRecipeData
{
    private readonly ISqlDataAccess _dataAccess;

    public RecipeData(ISqlDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public Task<List<RecipeModel>> GetRecipeList()
    {
        const string sql = 
            "select * from Recipe r " +
                "left join Tags t on t.RecipeId = r.Id; ";

        //return  _dataAccess.LoadData<RecipeModel, dynamic>(sql, new { });
        return _dataAccess.LoadRecipe(sql);

    }

    public Task InsertRecipe(RecipeModel model)
    {
        const string sql = @"insert into dbo.Recipe (Id,Name, Calorie, Image, Ingredient)
                                values(@Id,@Name, @Calorie, @Image, @Ingredient);";
        //return  _dataAccess.SaveData(sql, model);
        _dataAccess.SaveData(sql, model);
        return Task.CompletedTask;
    }
}