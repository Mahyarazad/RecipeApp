CREATE TABLE [dbo].[Recipe] (
    [Id]      UNIQUEIDENTIFIER NOT NULL,
    [Name]    NVARCHAR(30)       NOT NULL,
    [Calorie] BIGINT           NULL,
    [Image]   NVARCHAR(500)      NULL,
    [Ingredient] NVARCHAR(1000) NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

