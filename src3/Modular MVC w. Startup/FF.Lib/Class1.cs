using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Lib
{
    public class DataController
    {
        public List<Fridge> ListFridges()
        {
            using (var context = new FFContext())
            {
                return context.Fridges.ToList();
            }
        }
    }
    [Table("Fridges", Schema = "FF")]
    public class Fridge
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
    internal class FFContext : DbContext
    {
        public DbSet<Fridge> Fridges { get; set; }
    }
}
