using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ProjektProgBD.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "decimal(9, 2)")] // 9 total digits, 2 decimal places
        public decimal Money { get; set; }
        public ObservableCollection<Game> Games { get; set; } = new ObservableCollection<Game>();
        public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();

        public string GetHashPassword()
        {
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(Password);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", string.Empty);

                return hash;
            }
        }

    }
}
