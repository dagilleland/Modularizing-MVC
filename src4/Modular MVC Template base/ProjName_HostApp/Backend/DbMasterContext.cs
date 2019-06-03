using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;

namespace ProjName_HostApp.Backend
{
    public class DacPacManager
    {
        private readonly string FolderPath;

    }
    public class DatabaseConnection
    {
        public string ConnectionStringName { get; set; }
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public SqlAuthenticationMethod AuthenticationMethod { get; set; }
        public bool DatabaseExists { get; set; }
    }
    public class DbMasterContext : DbContext
    {
        private readonly string ConnectionStringName;
        public readonly DatabaseConnection DatabaseConnection;
        public DbMasterContext() : base("name=DefaultConnection")
        {
            ConnectionStringName = "DefaultConnection";
            var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            var builder = new SqlConnectionStringBuilder(connectionString);
            DatabaseConnection = new DatabaseConnection
            {
                ConnectionStringName = ConnectionStringName,
                DataSource = builder.DataSource,
                InitialCatalog = builder.InitialCatalog,
                AuthenticationMethod = builder.Authentication,
                DatabaseExists = Database.Exists(ConnectionStringName)
            };
        }

        public bool DatabaseExists => DatabaseConnection.DatabaseExists;
        public bool HasBackupRights { get; private set; }
        public bool HasRestoreRights { get; private set; }
        public void CreateDatabase()
        {
            if (DatabaseExists) throw new Exception("Database already exists");
            new DbContext(ConnectionStringName).Database.CreateIfNotExists();
        }
        public void DeleteDatabase()
        {
            if (!DatabaseExists) throw new Exception("Database does not exist");
            new DbContext(ConnectionStringName).Database.Delete();
        }
        public string Backup(string suffix)
        {
            if (suffix == null) suffix = string.Empty;
            var db = new DbContext(ConnectionStringName);
            string dbname = db.Database.Connection.Database;
            string sqlCommand = $@"BACKUP DATABASE [{dbname}] TO  DISK = N'C:\Backup\{dbname}{suffix}.bak' WITH NOFORMAT, NOINIT,  SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            //string sqlCommand = $@"BACKUP DATABASE [{dbname}] TO  DISK = N'C:\Backup\' WITH NOFORMAT, NOINIT,  NAME = N'{dbname}{suffix}.bak', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            db.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, sqlCommand);
            return dbname + suffix;
        }
        public IEnumerable<string> ListBackups()
        {
            throw new NotImplementedException();
        }
        public void Restore(string suffix)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            var builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog = "master";
            if (suffix == null) suffix = string.Empty;
            var db = new DbContext(builder.ConnectionString);
            string dbname = DatabaseConnection.InitialCatalog;
            string sqlCommand = $@"RESTORE DATABASE [{dbname}] FROM  DISK = N'C:\Backup\{dbname}{suffix}.bak'";
            db.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, sqlCommand);
        }

    }
}