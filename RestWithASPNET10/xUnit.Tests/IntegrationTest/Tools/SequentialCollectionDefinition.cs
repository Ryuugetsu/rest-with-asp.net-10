using Xunit;

namespace xUnit.Tests.IntegrationTest.Tools
{
    [CollectionDefinition("Sequential Tests", DisableParallelization = true)]
    public class SequentialCollectionDefinition : ICollectionFixture<SqlServerFixture>{}
}