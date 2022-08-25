CREATE PROCEDURE [dbo].[Recipe_InsertSet]
	@tag BasicUDT readonly
AS
BEGIN
	INSERT INTO dbo.Tags(TagId,Tag,RecipeId) 
	SELECT [TagId],[Tag],[RecipeId]
	from @tag;
end
		