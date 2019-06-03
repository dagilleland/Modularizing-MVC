using JsonFlatFileDataStore;
using MvcApplicationComponent;
using Owin;
using ProjName_HostApp.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProjName_HostApp.Backend
{
    public static class DatabaseManager
    {
        public static void BackupDatabase()
        { }

        public static void RegisterHostApp(string mapPath, MvcActionLink homePage)
        {
            using (var store = new DataStore(Path.Combine(mapPath, "AppInstallLog.json")))
            {
                var installations = store.GetCollection<AppRegistration>();
                var hostGuidAttribute = installations.GetType().Assembly.GetCustomAttributes(true).OfType<System.Runtime.InteropServices.GuidAttribute>().FirstOrDefault();
                Guid hostGuid = hostGuidAttribute == null?Guid.Empty:Guid.Parse(hostGuidAttribute.Value);
                var assemblyName = installations.GetType().Assembly.FullName;
                AppRegistration host = new AppRegistration()
                {
                    AppHomePage = homePage,
                    FriendlyName = "Host MVC Application",
                    UserInterface = new UIAssembly
                    {
                        AssemblyGuid = hostGuid,
                        FullyQualifiedAssemblyName = assemblyName
                    }
                };
                installations.InsertOne(host);
            }
        }
        public static IDocumentCollection<AppRegistration> ReadInstallationLog(string mapPath)
        {
            using (var store = new DataStore(Path.Combine(mapPath, "AppInstallLog.json")))
            {
                var installations = store.GetCollection<AppRegistration>();
                return installations;
            }
        }
        public static bool CheckDatabaseConnection()
        {
            string connection, database;
            using (var context = new MyDbContext())
            {
                connection = context.Database.Connection.ConnectionString;
                database = context.Database.Connection.Database;
            }
            using (var conn = new SqlConnection(connection))
            {
                var sql = $"EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=N'{database}')";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        var obj = cmd.ExecuteScalar();
                        bool exists;
                        bool.TryParse(obj.ToString(), out exists);
                        return exists;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
        public static void PublishDacPacs(string mapPath)
        {

        }
        public static List<DacPacFileInfo> ListDacPacs(string mapPath)
        {
            var files = Directory.GetFiles(mapPath, "*.dacpac");
            var results = new List<DacPacFileInfo>();
            foreach (var item in files)
            {
                using (var dacpac = Microsoft.SqlServer.Dac.DacPackage.Load(Path.Combine(mapPath, item)))
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
    public class DacPacFileInfo : DacPacInfo
    {
        public string FilePath { get; set; }
    }
}