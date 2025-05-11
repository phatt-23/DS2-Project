using DS2ConsoleApp.Orm.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Helper
{
    public static class SqlAttribMapper
    {
        public static byte[] GetBinary(SqlDataReader reader, string attributeName)
        {
            var data = reader.GetSqlBinary(reader.GetOrdinal(attributeName));
            return data.Value;
        }

        public static Visibility GetVisibility(SqlDataReader reader, string attributeName)
        {
            var ord = reader.GetOrdinal(attributeName);
            var value = reader.GetString(ord);
            return value.ToVisibilityEnum();
        }

        public static T? GetNullable<T>(SqlDataReader reader, string attributeName)
            where T : struct
        {
            var ord = reader.GetOrdinal(attributeName);
            if (reader.IsDBNull(ord)) return null;

            object value = reader.GetValue(ord);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T Get<T>(SqlDataReader reader, string attributeName)
            where T : struct
        {
            var ord = reader.GetOrdinal(attributeName);
            object value = reader.GetValue(ord);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static string? GetNullableString(SqlDataReader reader, string attributeName)
        {
            var ord = reader.GetOrdinal(attributeName);
            return (reader.IsDBNull(ord)) ? null : reader.GetString(ord);
        }

        public static string GetString(SqlDataReader reader, string attributeName)
        {
            return reader.GetString(reader.GetOrdinal(attributeName));
        }
    }
}
