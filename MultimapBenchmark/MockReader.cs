using System;
using System.Collections;
using System.Data.Common;

namespace MultimapBenchmark
{
    public sealed class MockReader : DbDataReader
    {
        private int _columns;
        private int _rows;
        private long _value;

        public MockReader(int columns, int rows)
        {
            _columns = columns;
            _rows = rows;
        }

        public override object this[int ordinal] => GetInt64(ordinal);

        public override object this[string name] => name == "Id" ? _value : throw new IndexOutOfRangeException();

        public override int Depth => 0;

        public override int FieldCount => _columns;

        public override bool HasRows => _rows > 0;

        public override bool IsClosed => false;

        public override int RecordsAffected => 0;

        public override bool GetBoolean(int ordinal) => Convert.ToBoolean(GetInt64(ordinal));

        public override byte GetByte(int ordinal) => Convert.ToByte(GetInt64(ordinal));

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override char GetChar(int ordinal) => Convert.ToChar(GetInt64(ordinal));

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override string GetDataTypeName(int ordinal) =>
            (uint)ordinal < (uint)_columns ? "INT" : throw new IndexOutOfRangeException();

        public override DateTime GetDateTime(int ordinal) => Convert.ToDateTime(GetInt64(ordinal));

        public override decimal GetDecimal(int ordinal) => Convert.ToDecimal(GetInt64(ordinal));

        public override double GetDouble(int ordinal) => Convert.ToDouble(GetInt64(ordinal));

        public override IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override Type GetFieldType(int ordinal) =>
            (uint)ordinal < (uint)_columns ? typeof(long) : throw new IndexOutOfRangeException();

        public override float GetFloat(int ordinal) => Convert.ToSingle(GetInt64(ordinal));

        public override Guid GetGuid(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override short GetInt16(int ordinal) => Convert.ToInt16(GetInt64(ordinal));

        public override int GetInt32(int ordinal) => Convert.ToInt32(GetInt64(ordinal));

        public override long GetInt64(int ordinal) => (uint)ordinal < (uint)_columns ? _value : throw new IndexOutOfRangeException();

        public override string GetName(int ordinal) => (uint)ordinal < (uint)_columns ? "Id" : throw new IndexOutOfRangeException();

        public override int GetOrdinal(string name) => name == "Id" ? 0 : throw new IndexOutOfRangeException();

        public override string GetString(int ordinal) => Convert.ToString(GetInt64(ordinal));

        public override object GetValue(int ordinal) => GetInt64(ordinal);

        public override int GetValues(object[] values)
        {
            var count = Math.Min(_columns, values.Length);
            for (var i = 0; i < count; i++) values[i] = _value;
            return count;
        }

        public override bool IsDBNull(int ordinal) => (uint)ordinal < (uint)_columns ? false : throw new IndexOutOfRangeException();

        public override bool NextResult()
        {
            _rows = 0;
            return false;
        }

        public override bool Read()
        {
            if (_value >= _rows) return false;

            _value++;
            return true;
        }
    }
}
