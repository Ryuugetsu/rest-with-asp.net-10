CREATE TABLE [dbo].[Books] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] VARCHAR(MAX) NULL,
    [Author] VARCHAR(MAX) NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    [LaunchDate] DATETIME2(6) NOT NULL
);