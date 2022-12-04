TRUNCATE TABLE [Test.Hangfire].[hangfire].[AggregatedCounter]
TRUNCATE TABLE [Test.Hangfire].[hangfire].[Counter]
TRUNCATE TABLE [Test.Hangfire].[hangfire].[JobParameter]
TRUNCATE TABLE [Test.Hangfire].[hangfire].[JobQueue]
TRUNCATE TABLE [Test.Hangfire].[hangfire].[List]
TRUNCATE TABLE [Test.Hangfire].[hangfire].[State]
TRUNCATE TABLE [Test.Hangfire].[hangfire].[Server]
DELETE FROM [Test.Hangfire].[hangfire].[Job]
DBCC CHECKIDENT ('[Test.Hangfire].[hangfire].[Job]', reseed, 0)
UPDATE [Test.Hangfire].[hangfire].[Hash] SET Value = 1 WHERE Field = 'LastJobId'