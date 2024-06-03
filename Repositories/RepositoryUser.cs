using Microsoft.Extensions.DependencyInjection;
using ProjektProgBD.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProjektProgBD.Repositories
{
    public static class RepositoryUser
    {
        public static bool AddUserToDb(User user)
        {
            bool state = false;

            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();

            if (user != null)
            {
                db.Users.Add(user);
                state = true;
                db.SaveChanges();
            }
            return state;
        }

        public static bool DeleteUserFromDb(int id)
        {
            bool state = false;

            using var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();

            var userToRemove = db.Users.SingleOrDefault(u => u.Id == id);
            if (userToRemove != null)
            {
                db.Users.Remove(userToRemove);
                state = true;
                db.SaveChanges();
            }
            return state;
        }

        public static ObservableCollection<User> GetAllUsers()
        {
            var list = new ObservableCollection<User>();
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();

            var users = db.Users.ToList();
            foreach (var user in users)
            {
                list.Add(user);
            }
            return list;
        }
    }
}