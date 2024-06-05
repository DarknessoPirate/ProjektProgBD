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

            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();

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

        public static User GetUserFromDb(User user)
        {
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            if (user != null)
            {
                var resultUser =  db.Users.SingleOrDefault(u => u.Name == user.Name && u.Password == user.Password);
                if (resultUser != null)
                {
                    return resultUser;
                }
               
            }
            return null;
        }

        public static User GetUserFromDb(string userName)
        {
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            if (userName != string.Empty)
            {
                var resultUser = db.Users.SingleOrDefault(u => u.Name == userName);
                if (resultUser != null)
                {
                    return resultUser;
                }

            }
            return null;
        }

        public static bool FindUserInDB(User user)
        {
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            if(user != null)
            {
                var foundUser = db.Users.SingleOrDefault(u => u.Name == user.Name && u.Password == user.Password);
                if (foundUser != null)
                    return true;
            }
            return false;
        }

        public static bool FindUserInDB(string userName)
        {
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            if (userName != string.Empty)
            {
                var foundUser = db.Users.SingleOrDefault(u => u.Name == userName);
                if (foundUser != null)
                    return true;
            }
            return false;
        }
    }
}