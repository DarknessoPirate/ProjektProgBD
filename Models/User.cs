using System.Collections.ObjectModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ProjektProgBD.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ObservableCollection<Game> Games { get; set; }
        public ObservableCollection<Review> Reviews { get; set; }
    }
}
