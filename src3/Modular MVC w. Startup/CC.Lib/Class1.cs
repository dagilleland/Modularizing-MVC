using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Lib
{
    public class DataContext
    {
        public List<Card> ListCards()
        {
            using (var context = new CCContext())
            {
                return context.Cards.ToList();
            }
        }
    }

    [Table("Cards", Schema = "CC")]
    public class Card
    {
        public int ID { get; set; }
        public string CardFront { get; set; }
        public string InsideText { get; set; }
        public Guid AltId { get; set; } = Guid.NewGuid();
        public int? AuthorID { get; set; }
    }

    public class CCContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
    }
}
