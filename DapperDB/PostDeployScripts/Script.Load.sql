
--DELETE FROM [dbo].[Recipe];

--DELETE FROM [dbo].[Tags];
--IF not exists (select 1 from dbo.[Recipe])
--begin
	
	--INSERT [dbo].[Recipe] ([Id], [Name], [Calorie], [Image], [Ingredient]) VALUES (N'd7dbdf4e-d5bf-4bda-85fd-0f429fb1535a', N' Gluten-Free Diet', 2345, N'https://post.healthline.com/wp-content/uploads/2020/09/gluten-free-diet-thumb-1.jpg', N'A gluten-free diet excludes any foods that contain gluten, which is a protein found in wheat and several other grains. It means eating only whole foods that don''t contain gluten, such as fruits, vegetables, meat and eggs, as well as processed gluten-free foods like gluten-free bread or pasta.')

	--INSERT [dbo].[Recipe] ([Id], [Name], [Calorie], [Image], [Ingredient]) VALUES (N'58e91f86-de37-439f-9847-61b736312b76', N'Best Easy Dinner', 648, N'https://images.themodernproper.com/billowy-turkey/production/posts/2020/Instant-Pot-Chicken-Marinara-with-Polenta-12.jpg?w=667&auto=compress%2Cformat&fit=crop&dm=1611333160&s=b0ae2c57fbb2d6dd599b4a4b509b3719', N'What should I make for dinner tonight that''s EASY? What are some good, healthy dinners? Could I just pay someone to come up with an easy dinner idea for me? We’re food bloggers—we literally cook dinner')

	--INSERT [dbo].[Tags] ([TagId], [Tag], [RecipeId]) VALUES (N'dce5248c-6ef1-4af2-8916-c81183a9a94f', N' Gluten Free', N'd7dbdf4e-d5bf-4bda-85fd-0f429fb1535a')

	--INSERT [dbo].[Tags] ([TagId], [Tag], [RecipeId]) VALUES (N'ab908caf-529e-446d-9776-fcea824042aa', N'Keto Sweets', N'd7dbdf4e-d5bf-4bda-85fd-0f429fb1535a')

	--INSERT [dbo].[Tags] ([TagId], [Tag], [RecipeId]) VALUES (N'2f180c48-06e5-4598-8137-cf7cb38b6989', N'Suger Free', N'd7dbdf4e-d5bf-4bda-85fd-0f429fb1535a')

	--INSERT [dbo].[Tags] ([TagId], [Tag], [RecipeId]) VALUES (N'3b803c4c-cc2c-493a-bc15-26ff40a6fa96', N'Fast Dinner', N'58e91f86-de37-439f-9847-61b736312b76')
--end
