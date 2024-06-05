using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjektProgBD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace ProjektProgBD.Repositories
{
    public static class RepositoryGame
    {
        public static bool AddGameToDb(Game game)
        {
            bool state = false;

            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            
            if (game != null)
            {
                db.Games.Add(game);
                state = true;
                db.SaveChanges();
            }
            return state;
        }

        public static bool DeleteGameFromDb(int id)
        {
            bool state = false;

            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            
            var gameToRemove = db.Games.SingleOrDefault(g => g.Id == id);
            if (gameToRemove != null)
            {
                db.Games.Remove(gameToRemove);
                state = true;
                db.SaveChanges();
            }
            
            return state;
        }

        public static ObservableCollection<Game> GetUserGamesFromDb(int userId)
        {
            var list = new ObservableCollection<Game>();
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();

            var games = db.Users.Where(u => u.Id == userId).SelectMany(u => u.Games).ToHashSet().ToList();

            
            foreach (var game in games)
            {
                list.Add(game);
            }
            return list;
            
        }
    }
}
