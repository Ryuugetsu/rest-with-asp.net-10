CREATE TABLE dbo.person (
  [Id] bigint PRIMARY KEY NOT NULL IDENTITY,
  [FirstName] varchar(80) NOT NULL,
  [LastName] varchar(80) NOT NULL,
  [Address] varchar(100) NOT NULL,
  [Gender] varchar(6) NOT NULL,
)