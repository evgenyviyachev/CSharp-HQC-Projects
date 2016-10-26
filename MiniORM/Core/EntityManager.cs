namespace MiniORM.Core
{
    using Attributes;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class EntityManager : IDBContext
    {
        private SqlConnection connection;
        private string connectionString;
        private bool isCodeFirst;

        public EntityManager(string connectionString, bool isCodeFirst)
        {
            this.connectionString = connectionString;
            this.isCodeFirst = isCodeFirst;
        }

        public int DeleteFrom<T>()
        {
            return this.DeleteFrom<T>("");
        }

        public int DeleteFrom<T>(string where)
        {
            int result = 0;

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                string deleteCommandString = $"DELETE FROM {this.GetTableName(typeof(T))} {where}";
                SqlCommand deleteCommand = new SqlCommand(deleteCommandString, this.connection);
                
                try
                {
                    result = deleteCommand.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw new ArgumentException("The 'WHERE' clause is not correct!");
                }
            }

            return result;
        }

        public IEnumerable<T> FindAll<T>()
        {
            return this.FindAll<T>("");
        }

        public IEnumerable<T> FindAll<T>(string where)
        {
            string findAllCommandString = $"SELECT * FROM {this.GetTableName(typeof(T))} {where}";

            List<T> allEntities = new List<T>();

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand findAllCommand = new SqlCommand(findAllCommandString, this.connection);
                SqlDataReader reader = null;

                try
                {
                    reader = findAllCommand.ExecuteReader();
                }
                catch (SqlException)
                {
                    throw new ArgumentException("The 'WHERE' clause is not correct!");
                }                

                if (!reader.HasRows)
                {
                    throw new InvalidOperationException("No such entities were found!");
                }

                while (reader.Read())
                {
                    allEntities.Add(CreateEntity<T>(reader));
                }
            }

            return allEntities;
        }

        public T FindByID<T>(int id)
        {
            T result = default(T);
            string findCommandString = $"SELECT * FROM {this.GetTableName(typeof(T))} " +
                                       $"WHERE Id = @Id";

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand findCommand = new SqlCommand(findCommandString, this.connection);
                findCommand.Parameters.AddWithValue("Id", id);
                var reader = findCommand.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new InvalidOperationException("No entity with this id was found!");
                }
                reader.Read();

                result = CreateEntity<T>(reader);
            }

            return result;
        }

        public T FindFirst<T>()
        {
            return this.FindFirst<T>("");
        }

        public T FindFirst<T>(string where)
        {
            T result = default(T);
            string findFirstCommandString = $"SELECT * FROM {this.GetTableName(typeof(T))} {where}";

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand findFirstCommand = new SqlCommand(findFirstCommandString, this.connection);
                SqlDataReader reader = null;

                try
                {
                    reader = findFirstCommand.ExecuteReader();
                }
                catch (SqlException)
                {
                    throw new ArgumentException("The 'WHERE' clause is not correct!");
                }

                if (!reader.HasRows)
                {
                    throw new InvalidOperationException("No such entity was found!");
                }
                reader.Read();

                result = CreateEntity<T>(reader);
            }

            return result;
        }

        private T CreateEntity<T>(SqlDataReader reader)
        {
            object[] originalValues = new object[reader.FieldCount];
            reader.GetValues(originalValues);

            object[] values = new object[originalValues.Length - 1];
            Array.Copy(originalValues, 1, values, 0, values.Length);
            Type[] types = new Type[values.Length];

            for (int i = 0; i < types.Length; i++)
            {
                types[i] = values[i].GetType();
            }

            T entity = (T)typeof(T).GetConstructor(types).Invoke(values);

            entity.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(f => f.IsDefined(typeof(IdAttribute)))
                .SetValue(entity, (int)originalValues[0]);

            return entity;
        }

        public bool Persist(object entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("Object can not be null!");
            }

            if (!CheckIfTableExists(entity) && this.isCodeFirst)
            {
                this.CreateTable(entity);
            }

            FieldInfo[] columns = entity.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.IsDefined(typeof(ColumnAttribute))).ToArray();

            FieldInfo idInfo = this.GetId(entity.GetType());

            int id = (int)idInfo.GetValue(entity);

            if (id <= 0)
            {
                return this.Insert(entity, idInfo);
            }

            return this.Update(entity, idInfo);
        }

        private bool Update(object entity, FieldInfo idInfo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"UPDATE {this.GetTableName(entity.GetType())} SET ");

            FieldInfo[] columns = entity.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.IsDefined(typeof(ColumnAttribute))).ToArray();

            for (int i = 0; i < columns.Length; i++)
            {
                builder.Append($"{this.GetColumnName(columns[i])} = '{columns[i].GetValue(entity)}', ");
            }

            builder = builder.Remove(builder.Length - 2, 2);

            int id = (int)idInfo.GetValue(entity);
            builder.Append($" WHERE Id = {id}");

            int result;

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(builder.ToString(), this.connection);
                result = command.ExecuteNonQuery();
            }

            return result > 0;
        }

        private bool Insert(object entity, FieldInfo idInfo)
        {
            StringBuilder columnNamesBuilder = new StringBuilder();
            StringBuilder columnValuesBuilder = new StringBuilder();
            StringBuilder commandBuilder = new StringBuilder();

            commandBuilder.Append($"INSERT INTO {this.GetTableName(entity.GetType())} (");

            FieldInfo[] columns = entity.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.IsDefined(typeof(ColumnAttribute))).ToArray();

            for (int i = 0; i < columns.Length; i++)
            {
                columnNamesBuilder.Append($"{this.GetColumnName(columns[i])}, ");
                columnValuesBuilder.Append($"'{columns[i].GetValue(entity)}', ");
            }

            columnNamesBuilder = columnNamesBuilder.Remove(columnNamesBuilder.Length - 2, 2);
            columnValuesBuilder = columnValuesBuilder.Remove(columnValuesBuilder.Length - 2, 2);

            commandBuilder.Append(columnNamesBuilder)
                          .Append(") VALUES (")
                          .Append(columnValuesBuilder)
                          .Append(")");

            int result;

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(commandBuilder.ToString(), this.connection);
                result = command.ExecuteNonQuery();

                string selectId = $"SELECT MAX(Id) FROM {this.GetTableName(entity.GetType())}";
                SqlCommand query = new SqlCommand(selectId, this.connection);
                int id = (int)query.ExecuteScalar();
                idInfo.SetValue(entity, id);
            }

            return result > 0;
        }

        private void CreateTable(object entity)
        {
            StringBuilder createTable = new StringBuilder();
            createTable.Append($"CREATE TABLE {this.GetTableName(entity.GetType())} (");
            createTable.Append("Id INT PRIMARY KEY IDENTITY, ");

            FieldInfo[] columns = entity.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.IsDefined(typeof(ColumnAttribute))).ToArray();

            for (int i = 0; i < columns.Length; i++)
            {
                createTable.Append($"{this.GetColumnName(columns[i])} {this.GetDBType(columns[i])}, ");
            }

            createTable = createTable.Remove(createTable.Length - 2, 2);
            createTable.Append(")");

            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                string commandString = createTable.ToString();
                SqlCommand command = new SqlCommand(commandString, connection);
                command.ExecuteNonQuery();
            }
        }

        private bool CheckIfTableExists(object entity)
        {
            Type type = entity.GetType();

            string query =
                $"SELECT COUNT(name) " +
                $"FROM sys.sysobjects " +
                $"WHERE [Name] = '{this.GetTableName(type)}' AND [xtype] = 'U'";

            int numberOfTables = 0;
            using (this.connection = new SqlConnection(this.connectionString))
            {
                this.connection.Open();
                SqlCommand command = new SqlCommand(query, this.connection);
                numberOfTables = (int)command.ExecuteScalar();
            }

            return numberOfTables > 0;
        }

        private string GetDBType(FieldInfo fieldInfo)
        {
            string type = fieldInfo.FieldType.Name;

            switch (type)
            {
                case "Int32":
                    return "INT";
                case "String":
                    return "VARCHAR(MAX)";
                case "DateTime":
                    return "DATETIME";
                case "Boolean":
                    return "BIT";
                default:
                    throw new ArgumentException("No such type in framework");
            }
        }

        private FieldInfo GetId(Type entity)
        {
            if (entity == null)
            {
                throw new NullReferenceException("Object can not be null!");
            }

            FieldInfo idInfo = entity.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault(f => f.IsDefined(typeof(IdAttribute)));

            if (idInfo == null)
            {
                throw new ArgumentNullException("Id can not be null!");
            }

            return idInfo;
        }

        private string GetTableName(Type entityType)
        {
            if (entityType == null)
            {
                throw new NullReferenceException("Can not find tablename for null!");
            }

            if (!entityType.IsDefined(typeof(EntityAttribute)))
            {
                throw new ArgumentNullException("Entity not in framework!");
            }

            string tableName = entityType.GetCustomAttribute<EntityAttribute>().TableName;

            if (tableName == null)
            {
                throw new ArgumentNullException("Table name not defined!");
            }

            return tableName;
        }

        private string GetColumnName(FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("Field not in table!");
            }

            if (!field.IsDefined(typeof(ColumnAttribute)))
            {
                return field.Name;
            }

            string columnName = field.GetCustomAttribute<ColumnAttribute>().Name;

            if (columnName == null)
            {
                throw new ArgumentNullException("Column Name can not be null!");
            }

            return columnName;
        }
    }
}
