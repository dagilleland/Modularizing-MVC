using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.Lib
{
    public class DataController
    {
        public List<Author> ListAuthors()
        {
            using (var context = new AAContext())
            {
                return context.Authors.ToList();
            }
        }
    }

    [Table("Authors", Schema = "AA")]
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AAContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
    }
}
