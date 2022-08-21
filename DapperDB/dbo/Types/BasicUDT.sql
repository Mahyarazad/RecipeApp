CREATE TYPE [dbo].[BasicUDT] AS TABLE 
(
	TagId uniqueidentifier,
	Tag NVARCHAR(30),
	RecipeID uniqueidentifier
)