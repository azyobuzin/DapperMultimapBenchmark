using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.Data.Sqlite;

namespace MultimapBenchmark
{
    public class SqliteBenchmark : BenchmarkBase
    {
        protected override IDbConnection CreateMockConnection(int columns, int rows)
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var sb = new StringBuilder("CREATE TABLE TestTable (Id INTEGER);\n");

            for (var i = 1; i <= rows; i++)
            {
                sb.AppendFormat(CultureInfo.InvariantCulture, "INSERT INTO TestTable (Id) VALUES ({0});\n", i);
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sb.ToString();
                command.ExecuteNonQuery();
            }

            return connection;
        }
    }
}
