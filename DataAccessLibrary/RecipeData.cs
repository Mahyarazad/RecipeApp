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

    public Task<bool> UpdateRecipe(RecipeModel model)
    {
        const string sql = @"update dbo.Recipe set Name=@Name, Calorie=@Calorie, Image=@Image, Ingredient=@Ingredient where Id=@Id;";
        
        return Task.FromResult(_dataAccess.UpdateData(sql, model));
    }

    public async Task<RecipeModel?> GetRecipe(Guid id)
    {
        return await 
            _dataAccess.LoadSingleRecipe("dbo.Recipe_GetRow"
                , new { id });
       
    }

    public Task DeleteTag(Guid id)
    {
        _dataAccess.DeleteData(@"delete from dbo.Tags where TagId = @Id", new {id});
        return Task.CompletedTask;
    }

    public Task DeleteRecipe(Guid id, List<Guid> tagIdList )
    {
        foreach (var tagId in tagIdList)
        {
            _dataAccess.DeleteData(@"delete from dbo.Tags where TagId = @tagId", new { tagId });
        }
        _dataAccess.DeleteData(@"delete from dbo.Recipe where Id = @Id", new { id });
        return Task.CompletedTask;
    }
}