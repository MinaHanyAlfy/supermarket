using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
namespace SuperMarket
{
    class DbHelper
    {
        private readonly DbProviderFactory _factory;
        string connectionString;
        //Constructor
        public DbHelper(string providerName, string ConnectionString)
        {
            DbProviderFactory providerFactory;
            if (providerName == "MySql")
                providerFactory = getFactory("MySql");
            else if (providerName == "SQLite")
                providerFactory = getFactory("SQLite");
            else
            {
                providerFactory = DbProviderFactories.GetFactory(providerName);
            }
            _factory = providerFactory;
            connectionString = ConnectionString;
        }
        //Get Factory for not install providers, for now:MySql and SQLite
        private DbProviderFactory getFactory(string providerName)
        {
            DbProviderFactory f = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Description");
            dt.Columns.Add("InvariantName");
            dt.Columns.Add("AssemblyQualifiedName");
            switch (providerName)
            {
                case "MySql":
                    dt.Rows.Add("Mysql something",
                           "mysql more",
                           "mysqlClient",
                           "MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data");
                    f = DbProviderFactories.GetFactory(dt.Rows[0]);
                    break;
                case "SQLite":
                    dt.Rows.Add("SQLite Data Provider",
                    ".Net Framework Data Provider for SQLite",
                    "System.Data.SQLite",
                    "System.Data.SQLite.SQLiteFactory, System.Data.SQLite");
                    f = DbProviderFactories.GetFactory(dt.Rows[0]);
                    break;
            }
            return f;
        }
        //Create Command through factory, SQL statment and Paramters
        private DbCommand GetCommand(string query, object[] dctParams = null)
        {
            DbCommand command;
            DbConnection connection = _factory.CreateConnection();
            connection.ConnectionString = connectionString;
            command = _factory.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = query;
            command.Parameters.Clear();
            if (dctParams != null)
            {
                foreach (object kvp in dctParams)
                {
                    DbParameter parameter = _factory.CreateParameter();
                    parameter.Value = kvp;
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }
        //Get datatable
        public DataTable GetDataTable(string query, object[] dctParams = null)
        {
            DataTable table = new DataTable();
            DbCommand command = GetCommand(query, dctParams);
            DbDataAdapter adapter = _factory.CreateDataAdapter();
            adapter.SelectCommand = command;

            adapter.Fill(table);
            return table;
        }
        //DataSet
        public DataSet GetDataSet(string query, object[] dctParams = null)
        {
            DataSet ds = new DataSet();
            DbCommand command = GetCommand(query, dctParams);
            DbDataAdapter adapter = _factory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(ds);
            return ds;
        }

        //Update by SQL statment and DataTable
        public void Update(string sql, DataTable table)
        {
            StringBuilder statement = new StringBuilder();
            statement.Append(sql);
            DbCommand selectCommand = GetCommand(statement.ToString());
            DbDataAdapter adapter = _factory.CreateDataAdapter();
            adapter.SelectCommand = selectCommand;
            adapter.AcceptChangesDuringUpdate = true;
            DbCommandBuilder builder = _factory.CreateCommandBuilder();
            builder.ConflictOption = ConflictOption.OverwriteChanges;
            builder.DataAdapter = adapter;
            adapter.Update(table);
        }

        public int ExecuteNonQuery(string query, object[] dctParams = null)
        {
            int affectedRows = 0;
            DbCommand command = GetCommand(query, dctParams);
            DbConnection connection = command.Connection;
            connection.Open();

            affectedRows = command.ExecuteNonQuery();
            connection.Close();
            return affectedRows;
        }


        public DbDataReader ExecuteReader(string query, object[] dctParams = null)
        {


            DbCommand command = GetCommand(query, dctParams);
            DbConnection connection = command.Connection;
            connection.Open();

            DbDataReader dr = command.ExecuteReader();

            connection.Close();
            return dr;
        }

        public int GetNumberOfRows(string tableName)
        {
            int countRows = 0;

            DataTable dt = new DataTable();
            dt = GetDataTable("SELECT * FROM " + tableName);
            countRows = dt.Rows.Count;
            return countRows;
        }
        public List<string> GetColumnNames(string tableName)
        {
            DataTable dt = new DataTable();
            List<string> colName = new List<string>();
            dt = GetDataTable("select *  from " + tableName);
            foreach (DataColumn col in dt.Columns)
                colName.Add(col.ColumnName);
            return colName;
        }
        public object ExecuteScalar(string query, object[] dctParams = null)
        {
            DbCommand cmd = GetCommand(query, dctParams);
            DbConnection conn = cmd.Connection;
            conn.Open();
            object cell = null;
            if (cmd.ExecuteScalar() != null)
                cell = cmd.ExecuteScalar();
            conn.Close();
            return cell;
        }
    }
}
