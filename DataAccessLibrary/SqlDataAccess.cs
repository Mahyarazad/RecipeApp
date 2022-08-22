﻿using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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


            var result = data.GroupBy(p => p.Id).Select(g =>
            {
                var groupedRecipe = g.First();
                groupedRecipe.TagList = g.Select(p => p.TagList.Single()).ToList();
                return groupedRecipe;
            });

            return result.FirstOrDefault() ?? throw new InvalidOperationException();
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

            var result = data.GroupBy(p => p.Id).Select(g =>
            {
                var groupedRecipe = g.First();
                groupedRecipe.TagList = g.Select(p => p.TagList.Single()).ToList();
                return groupedRecipe;
            });

            return result.ToList();
        }
    }

    public void SaveData(string sql, RecipeModel parameter)
    {
        var connectionString = _configuration.GetConnectionString(ConnectionString);
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            var output = new DataTable();

            output.Columns.Add("TagId", typeof(Guid));
            output.Columns.Add("Tag", typeof(string));
            output.Columns.Add("RecipeId", typeof(Guid));

            foreach (var tag in parameter.TagList)
            {
                output.Rows.Add(tag.TagId, tag.Tag, tag.RecipeId);
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
        }
    }
}