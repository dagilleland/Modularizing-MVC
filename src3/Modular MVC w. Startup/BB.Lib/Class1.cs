using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Lib
{
    public class DataController
    {
        public List<Book> ListBooks()
        {
            using (var context = new BBContext())
            {
                return context.Books.ToList();
            }
        }
    }

    [Table("Books", Schema = "BB")]
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int? AuthorID { get; set; }
    }

    public class BBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}
