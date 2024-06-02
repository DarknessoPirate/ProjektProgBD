using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgBD.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Content { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Game Game { get; set; }
        public int GameId { get; set; }
    }
}
