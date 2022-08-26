using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _configuration;
    public string ConnectionString { get; set; } = "Default";

    public SqlDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<RecipeModel> LoadSingleRecipe<U>(string storedProcedure, U parameter)
    {
        var connectionString = _configuration.GetConnectionString(ConnectionString);

        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            var data = 
                connection.Query<RecipeModel, Tags, RecipeModel>(storedProcedure,

                    (recipeModel, tags) =>
                    {
                        recipeModel.TagList.Add(tags);
                        return recipeModel;
                    }, splitOn: "TagId",
                    param:parameter,
                    commandType: CommandType.StoredProcedure);

            if (data.Count() == 0)
            {
                var result =
                    connection.Query<RecipeModel>(sql: @"select * from Recipe where Id = @Id", parameter);
                return result.FirstOrDefault();
            }
            else
            {
                var result = data.GroupBy(p => p.Id).Select(g =>
                {
                    var groupedRecipe = g.First();
                    groupedRecipe.TagList = g.Select(p => p.TagList.Single()).ToList();
                    return groupedRecipe;
                });

                return result.FirstOrDefault();
            }
        }
    }

    public async Task<List<RecipeModel>> LoadRecipe(string sql)
    {
        using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(ConnectionString)))
        {
            var data =
                await connection.QueryAsync<RecipeModel, Tags, RecipeModel>(sql, (recipeModel, tags) =>
                {
                    recipeModel.TagList.Add(tags);
                    return recipeModel;
                }, splitOn: "TagId");

            var result = 
                data.GroupBy(p => p.Id).Select(g =>
                    {
                        var groupedRecipe = g.First();
                        groupedRecipe.TagList = g.Select(p => p.TagList.Single()).ToList();
                        return groupedRecipe;
                    });

            return result.ToList();
        }
    }

    public bool SaveData(string sql, RecipeModel parameter)
    {
        var connectionString = _configuration.GetConnectionString(ConnectionString);
        using (IDbConnection connection = new SqlConnection(connectionString))
        {

            if (connection
                .ExecuteScalar<bool>(@"select count(1) from dbo.Recipe where Name=@name",
                    new { parameter.Name }))
            {
                return false;
            }

            //parameter.Id = new Guid();
            var output = new DataTable();

            output.Columns.Add("TagId", typeof(Guid));
            output.Columns.Add("Tag", typeof(string));
            output.Columns.Add("RecipeId", typeof(Guid));

            foreach (var tag in parameter.TagList)
            {
                output.Rows.Add(tag.TagId, tag.Tag, parameter.Id);
            }

            var p = new
            {
                tag = output.AsTableValuedParameter("BasicUDT")
            };

            var lineaffected = connection.Execute(sql, parameter);
            var affectedLines = connection.Execute("dbo.Recipe_InsertSet"
                , p, commandType: CommandType.StoredProcedure);
          

            //await connection.ExecuteAsync(sql, parameter);
            Console.WriteLine(affectedLines);
            return true;
        }
    }

    public bool UpdateData(string sql, RecipeModel parameter)
    {
        var connectionString = _configuration.GetConnectionString(ConnectionString);
        using (IDbConnection connection = new SqlConnection(connectionString))
        {

            var queryNameControl = connection.Query<Guid>(@"select Id from dbo.Recipe where Name=@name",
                new { parameter.Name }).FirstOrDefault();
            
            if (queryNameControl!=parameter.Id && queryNameControl!=Guid.Empty)
            {
                return false;
            }
            else
            {
                connection.Execute(sql, parameter);
                if (parameter.TagList.Any())
                {
                    var output = new DataTable();

                    output.Columns.Add("TagId", typeof(Guid));
                    output.Columns.Add("Tag", typeof(string));
                    output.Columns.Add("RecipeId", typeof(Guid));

                    foreach (var tag in parameter.TagList.Where(x=>x.NewTag))
                    {
                        output.Rows.Add(tag.TagId, tag.Tag, tag.RecipeId);
                    }

                    var p = new
                    {
                        tag = output.AsTableValuedParameter("BasicUDT")
                    };
                    connection.Execute("dbo.Recipe_InsertSet"
                        , p, commandType: CommandType.StoredProcedure);
                }
                
                return true;
            }
        }
    }

    public void DeleteData<U>(string sql, U parameter)
    {
        var connectionString = _configuration.GetConnectionString(ConnectionString);
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            connection.Execute(sql, parameter);
        }
    }

}