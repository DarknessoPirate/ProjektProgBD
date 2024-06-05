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
using System.Windows.Documents;
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

        public static bool ModifyGameInDb(int idToModify, string newName, decimal newPrice)
        {
            bool state = false;
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();

            var rowsAffected = db.Games
                .Where(r => r.Id == idToModify)
                .ExecuteUpdate(review => review
                    .SetProperty(r => r.Name, r => newName)
                    .SetProperty(r => r.Price, r => newPrice));

            if (rowsAffected > 0)
                state = true;


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

            //////////// Tymczasowy hasz set, bo są duplikaty w bazie /////////////////////////////
            var games = db.Users.Where(u => u.Id == userId).SelectMany(u => u.Games).ToHashSet().ToList();

            
            foreach (var game in games)
            {
                list.Add(game);
            }
            return list;
            
        }

        public static ObservableCollection<Game> GetAllGamesFromDb()
        {
            var list = new ObservableCollection<Game>();
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            var games = db.Games.ToList();

            foreach (var game in games)
            {
                list.Add(game);
            }
            return list;
        }

    }
}
