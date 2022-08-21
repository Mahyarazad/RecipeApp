CREATE TABLE [dbo].[Tags] (
    [TagId]    UNIQUEIDENTIFIER NOT NULL,
    [Tag]      NVARCHAR (30)    NOT NULL,
    [RecipeId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK__Tags_Recipee] PRIMARY KEY NONCLUSTERED ([TagId] ASC),
    CONSTRAINT [RecipeId] FOREIGN KEY ([RecipeId]) REFERENCES [dbo].[Recipe] ([Id])
);

