using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EE.Lib
{
    public class DataController
    {
        public List<Earmark> ListEarmarks()
        {
            using (var context = new EEContext())
            {
                return context.Earmarks.ToList();
            }
        }
    }

    [Table("Earmarks", Schema = "EE")]
    public class Earmark
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }

    public class EEContext : DbContext
    {
        public DbSet<Earmark> Earmarks { get; set; }
    }
}
