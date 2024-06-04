using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjektProgBD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgBD.Repositories
{
    public static class RepositoryGame
    {
        public static bool AddGameToDb(Game game)
        {
            bool state = false;

            using (var db = App.ServiceProvider.GetRequiredService<ShopDbContext>())
            {
                if (game != null)
                {
                    db.Games.Add(game);
                    state = true;
                    db.SaveChanges();
                }
            }
            return state;
        }

        public static bool DeleteGameFromDb(int id)
        {
            bool state = false;

            using (var db = App.ServiceProvider.GetRequiredService<ShopDbContext>())
            {
                var gameToRemove = db.Games.SingleOrDefault(g => g.Id == id);
                if (gameToRemove != null)
                {
                    db.Games.Remove(gameToRemove);
                    state = true;
                    db.SaveChanges();
                }
            }
            return state;
        }

        public static ObservableCollection<Game> GetUserGamesFromDb(int userId)
        {
            var list = new ObservableCollection<Game>();
            using (var db = App.ServiceProvider.GetRequiredService<ShopDbContext>())
            {
                var games = db.Users.Where(u => u.Id == userId).SelectMany(u => u.Games).ToList();
                foreach (var game in games)
                {
                    list.Add(game);
                }
                return list;
            }

        }
    }
}
