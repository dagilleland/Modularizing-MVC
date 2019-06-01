using ProjName_HostApp.Backend.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjName_HostApp.Backend
{
    public static class DatabaseManager
    {

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
    public class DacPacFileInfo
    {
        public string FilePath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Version Version { get; set; }

    }
}