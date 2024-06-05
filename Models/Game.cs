using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgBD.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [Column(TypeName = "decimal(9, 2)")]
        public decimal Price { get; set; }
        public ObservableCollection<User> Users { get; set;} = new ObservableCollection<User>();
        public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();
    }
}
