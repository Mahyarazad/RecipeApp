CREATE PROCEDURE [dbo].[Recipe_GetRow]
	@Id uniqueidentifier
AS
BEGIN
	if exists (select * from Recipe where Id = @Id)
	select * 
	from Recipe r inner join Tags t 
	on t.RecipeId = r.Id 
	where r.Id = @Id;
end