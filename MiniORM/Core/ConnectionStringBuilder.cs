namespace MiniORM.Core
{
    using System.Data.SqlClient;

    public class ConnectionStringBuilder
    {
        private SqlConnectionStringBuilder builder;
        private string connectionString;

        public ConnectionStringBuilder(string databaseName)
        {
            this.builder = new SqlConnectionStringBuilder();
            builder["Server"] = ".\\SQLEXPRESS";            
            builder["Integrated Security"] = true;
            builder["Connect Timeout"] = 1000;
            builder["Trusted_Connection"] = true;
            builder["Initial Catalog"] = databaseName;
            this.ConnectionString = this.builder.ToString();
        }

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }

            private set
            {
                connectionString = value;
            }
        }
    }
}
