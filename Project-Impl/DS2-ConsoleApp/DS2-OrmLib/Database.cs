﻿using System.Data;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace DS2OrmLib
{
    /// <summary>
    /// Represents a MS SQL Database
    /// </summary>
    public class Database
    {
        private SqlConnection Connection { get; set; }
        private SqlTransaction SqlTransaction { get; set; }
        public string Language { get; set; }

        public string ConnectionString { get; private set; }

        public Database(string connectionString)
        {
            Connection = new SqlConnection();
            Language = "en";
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Connect
        /// </summary>
        public bool Connect(string conString)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.ConnectionString = conString;
                Connection.Open();
            }
            return true;

        }

        /// <summary>
        /// Connect
        /// </summary>
        public bool Connect()
        {
            bool ret = true;
            if (Connection.State != ConnectionState.Open)
            {
                // connection string is stored in file App.config or Web.config
                ret = Connect(ConnectionString);
            }
            return ret;
        }

        /// <summary>
        /// Close
        /// </summary>
        public void Close()
        {
            Connection.Close();
        }

        /// <summary>
        /// Begin a transaction.
        /// </summary>
        public void BeginTransaction()
        {
            SqlTransaction = Connection.BeginTransaction(IsolationLevel.Serializable);
        }

        /// <summary>
        /// End a transaction.
        /// </summary>
        public void EndTransaction()
        {
            SqlTransaction.Commit();
        }

        /// <summary>
        /// If a transaction is failed call it.
        /// </summary>
        public void Rollback()
        {
            SqlTransaction.Rollback();
        }

        /// <summary>
        /// Select encapulated in the command.
        /// </summary>
        public SqlDataReader Select(SqlCommand command)
        {
            SqlDataReader sqlReader = command.ExecuteReader();
            return sqlReader;
        }

        /// <summary>
        /// Insert a record encapulated in the command.
        /// </summary>
        public int ExecuteNonQuery(SqlCommand command)
        {
            int rowNumber = 0;
            rowNumber = command.ExecuteNonQuery();
            return rowNumber;
        }

        /// <summary>
        /// Insert a record encapulated in the command, usable when identity value is retrieved.
        /// </summary>
        public T ExecuteScalar<T>(SqlCommand command)
        {
            return (T)command.ExecuteScalar();
        }

        /// <summary>
        /// Create command
        /// </summary>
        public SqlCommand CreateCommand(string strCommand)
        {
            SqlCommand command = new SqlCommand(strCommand, Connection);

            if (SqlTransaction != null)
            {
                command.Transaction = SqlTransaction;
            }
            return command;
        }

        public static Database Connect(Database pDb)
        {
            Database db;
            db = pDb ?? throw new Exception("pDb is null");
            return db;
        }

        public static void Close(Database pDb, Database db)
        {
            if (pDb == null)
            {
                db.Close();
            }
        }

    }
}
