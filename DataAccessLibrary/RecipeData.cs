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

    public Task<bool> InsertRecipe(RecipeModel model)
    {
        const string sql = @"insert into dbo.Recipe (Id,Name, Calorie, Image, Ingredient)
                                values(@Id,@Name, @Calorie, @Image, @Ingredient);";
        return Task.FromResult(_dataAccess.SaveData(sql, model));
       
    }

    public async Task<RecipeModel?> GetRecipe(Guid id)
    {
        return await 
            _dataAccess.LoadSingleRecipe("dbo.Recipe_GetRow"
                , new { id });
       
    }

    public Task DeleteTag(Guid Id)
    {
       
        _dataAccess.DeleteData(@"delete from dbo.Tags where TagId = @Id", new {Id});
        return Task.CompletedTask;
    }
}