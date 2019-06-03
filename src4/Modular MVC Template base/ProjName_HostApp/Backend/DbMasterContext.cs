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

        public readonly DatabaseConnection DatabaseConnection;
        //=> new DatabaseConnection { ConnectionStringName = ConnectionStringName, DataSource = Database.Connection.DataSource, InitialCatalog = Database.Connection.Database, ServerVersion = Database.Connection.ServerVersion };
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
        public string Backup()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<string> ListBackups()
        {
            throw new NotImplementedException();
        }
        public void Restore(string backupName)
        {
            throw new NotImplementedException();
        }

    }
}