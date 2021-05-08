using System.Data;
using Moq;

namespace MultimapBenchmark
{
    public class MapperBenchmark : BenchmarkBase
    {
        protected override IDbConnection CreateMockConnection(int columns, int rows)
        {
            var commandMock = new Mock<IDbCommand>();
            commandMock.SetupProperty(x => x.CommandText);
            commandMock.Setup(x => x.ExecuteReader(It.IsAny<CommandBehavior>()))
                .Returns(() => new MockReader(columns, rows));
            var command = commandMock.Object;

            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(x => x.State).Returns(ConnectionState.Open);
            connectionMock.Setup(x => x.CreateCommand()).Returns(command);

            return connectionMock.Object;
        }
    }
}
