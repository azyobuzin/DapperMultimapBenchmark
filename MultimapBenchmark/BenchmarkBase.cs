extern alias DapperBaseline;
using System.Collections.Generic;
using System.Data;
using BenchmarkDotNet.Attributes;

namespace MultimapBenchmark
{
    public abstract class BenchmarkBase
    {
        [Params(100)]
        public int Rows { get; set; }

        private IDbConnection _connection1;
        private IDbConnection _connection2;
        private IDbConnection _connection3;
        private IDbConnection _connection5;
        private IDbConnection _connection7;

        protected abstract IDbConnection CreateMockConnection(int columns, int rows);

        [GlobalSetup]
        public void Setup()
        {
            _connection1 = CreateMockConnection(1, Rows);
            _connection2 = CreateMockConnection(2, Rows);
            _connection3 = CreateMockConnection(3, Rows);
            _connection5 = CreateMockConnection(5, Rows);
            _connection7 = CreateMockConnection(7, Rows);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _connection1?.Dispose();
            _connection2?.Dispose();
            _connection3?.Dispose();
            _connection5?.Dispose();
            _connection7?.Dispose();
        }

        [Benchmark]
        public List<long> QueryLong()
        {
            var results = DapperBaseline::Dapper.SqlMapper.Query<long>(_connection1, "SELECT Id FROM TestTable");
            return DapperBaseline::Dapper.SqlMapper.AsList(results);
        }

        [Benchmark]
        public List<(long, long)> BaselineMultiMap2()
        {
            var results = DapperBaseline::Dapper.SqlMapper.Query(_connection2, "SELECT Id, Id FROM TestTable", (long a, long b) => (a, b));
            return DapperBaseline::Dapper.SqlMapper.AsList(results);
        }

        [Benchmark]
        public List<(long, long, long)> BaselineMultiMap3()
        {
            var results = DapperBaseline::Dapper.SqlMapper.Query(_connection3, "SELECT Id, Id, Id FROM TestTable", (long a, long b, long c) => (a, b, c));
            return DapperBaseline::Dapper.SqlMapper.AsList(results);
        }

        [Benchmark]
        public List<(long, long, long, long, long)> BaselineMultiMap5()
        {
            var results = DapperBaseline::Dapper.SqlMapper.Query(_connection5, "SELECT Id, Id, Id, Id, Id FROM TestTable", (long a, long b, long c, long d, long e) => (a, b, c, d, e));
            return DapperBaseline::Dapper.SqlMapper.AsList(results);
        }

        [Benchmark]
        public List<(long, long, long, long, long, long, long)> BaselineMultiMap7()
        {
            var results = DapperBaseline::Dapper.SqlMapper.Query(_connection7, "SELECT Id, Id, Id, Id, Id, Id, Id FROM TestTable", (long a, long b, long c, long d, long e, long f, long g) => (a, b, c, d, e, f, g));
            return DapperBaseline::Dapper.SqlMapper.AsList(results);
        }

        [Benchmark]
        public List<(long, long)> FixedMultiMap2()
        {
            var results = Dapper.SqlMapper.Query(_connection2, "SELECT Id, Id FROM TestTable", (long a, long b) => (a, b));
            return Dapper.SqlMapper.AsList(results);
        }

        [Benchmark]
        public List<(long, long, long)> FixedMultiMap3()
        {
            var results = Dapper.SqlMapper.Query(_connection3, "SELECT Id, Id, Id FROM TestTable", (long a, long b, long c) => (a, b, c));
            return Dapper.SqlMapper.AsList(results);
        }

        [Benchmark]
        public List<(long, long, long, long, long)> FixedMultiMap5()
        {
            var results = Dapper.SqlMapper.Query(_connection5, "SELECT Id, Id, Id, Id, Id FROM TestTable", (long a, long b, long c, long d, long e) => (a, b, c, d, e));
            return Dapper.SqlMapper.AsList(results);
        }

        [Benchmark]
        public List<(long, long, long, long, long, long, long)> FixedMultiMap7()
        {
            var results = Dapper.SqlMapper.Query(_connection7, "SELECT Id, Id, Id, Id, Id, Id, Id FROM TestTable", (long a, long b, long c, long d, long e, long f, long g) => (a, b, c, d, e, f, g));
            return Dapper.SqlMapper.AsList(results);
        }
    }
}
