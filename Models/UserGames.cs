using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgBD.Models
{
    public class UserGame
    {
        public int Id { get; set; }
        public User User { get; set; } = null!;
        public int UserId { get; set; }

        public Game Game { get; set; } = null!;
        public int GameId { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
