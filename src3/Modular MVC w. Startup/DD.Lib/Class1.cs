using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.Lib
{
    public class DataController
    {
        public List<DataCenter> ListDataCenters()
        {
            using (var context = new DDContext())
            {
                return context.DataCenters.ToList();
            }
        }
    }

    [Table("DataCenters", Schema = "DD")]
    public class DataCenter
    {
        public int ID { get; set; }
        public string Address { get; set; }
    }

    public class DDContext : DbContext
    {
        public DbSet<DataCenter> DataCenters { get; set; }
    }
}
