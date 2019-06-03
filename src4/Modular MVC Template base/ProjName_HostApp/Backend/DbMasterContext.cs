using Microsoft.SqlServer.Dac;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;

namespace ProjName_HostApp.Backend
{
    public class DacPacManager
    {
        private readonly string FolderPath;
        public DacPacManager(string folderPath)
        {
            FolderPath = folderPath;
        }
        public List<string> GetDeploymentPlan(string connectionString, string databaseName, string dacPacName)
        {
            List<string> MessageList = new List<string>();

            var dacOptions = new DacDeployOptions();
            dacOptions.BlockOnPossibleDataLoss = false;

            var dacServiceInstance = new DacServices(connectionString);
            dacServiceInstance.ProgressChanged +=
              new EventHandler<DacProgressEventArgs>((s, e) =>
                            MessageList.Add(e.Message));
            dacServiceInstance.Message +=
              new EventHandler<DacMessageEventArgs>((s, e) =>
                            MessageList.Add(e.Message.Message));

            try
            {
                string report, script, diagnostics;
                using (DacPackage dacpac = DacPackage.Load(Path.Combine(FolderPath, dacPacName)))
                {
                    report = dacServiceInstance.GenerateDeployReport(dacpac, databaseName,
                                            options: dacOptions);
                    script =dacServiceInstance.GenerateDeployScript(dacpac, databaseName,
                                            options: dacOptions);
                }
                diagnostics = string.Join(Environment.NewLine, MessageList);
                MessageList.Clear();
                MessageList.Add(report);
                MessageList.Add(script);
                MessageList.Add(diagnostics);
            }
            catch (Exception ex)
            {
                MessageList.Add(ex.Message);
            }
            return MessageList;
        }
        public List<string> ProcessDacPac(string connectionString, string databaseName, string dacPacName)
        {
            List<string> MessageList = new List<string>();

            var dacOptions = new DacDeployOptions();
            dacOptions.BlockOnPossibleDataLoss = false;
            //dacOptions.BackupDatabaseBeforeChanges = true;
            dacOptions.VerifyDeployment = true;

            var dacServiceInstance = new DacServices(connectionString);
            dacServiceInstance.ProgressChanged +=
              new EventHandler<DacProgressEventArgs>((s, e) =>
                            MessageList.Add(e.Message));
            dacServiceInstance.Message +=
              new EventHandler<DacMessageEventArgs>((s, e) =>
                            MessageList.Add(e.Message.Message));

            try
            {
                using (DacPackage dacpac = DacPackage.Load(Path.Combine(FolderPath, dacPacName)))
                {
                    dacServiceInstance.Deploy(dacpac, databaseName,
                                            upgradeExisting: true,
                                            options: dacOptions);
                }

            }
            catch (Exception ex)
            {
                MessageList.Add(ex.Message);
            }
            return MessageList;
        }
        public List<DacPacFileInfo> ListDacPacs()
        {
            var files = Directory.GetFiles(FolderPath, "*.dacpac");
            var results = new List<DacPacFileInfo>();
            foreach (var item in files)
            {
                using (var dacpac = Microsoft.SqlServer.Dac.DacPackage.Load(Path.Combine(FolderPath, item)))
                {
                    results.Add(new DacPacFileInfo
                    {
                        FilePath = item,
                        Name = dacpac.Name,
                        Description = dacpac.Description,
                        Version = dacpac.Version
                    });
                }
            }
            return results;
        }
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

            string offline = $@"ALTER DATABASE [{dbname}] SET SINGLE_USER 
With ROLLBACK IMMEDIATE";
            db.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, offline);

            string sqlCommand = $@"RESTORE DATABASE [{dbname}] FROM  DISK = N'C:\Backup\{dbname}{suffix}.bak' WITH REPLACE";
            db.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, sqlCommand);

            string online = $@"ALTER DATABASE [{dbname}] SET MULTI_USER";
            db.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, online);
        }

    }
}