using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjektProgBD.Models;
using ProjektProgBD.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Input;

namespace ProjektProgBD.ViewModels
{
    public class ShopViewModel : ViewModelBase
    {
        public ShopViewModel(User user)
        {
            CurrentUser = user;
            CurrentMoney = CurrentUser.Money;
            Games = RepositoryGame.GetAllGamesFromDb();
        }
        #region properties
        private User _currentUser;
        private decimal _currentMoney;
        private ObservableCollection<Game> _games;
        private Game _selectedGame;
        
        #endregion

        #region accessors

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                onPropertyChanged(nameof(CurrentUser));
            }
        }

        public decimal CurrentMoney
        {
            get { return _currentMoney; }
            set
            {
                _currentMoney = value;
                onPropertyChanged(nameof(CurrentMoney));
            }
        }

        public ObservableCollection<Game> Games
        {
            get { return _games; }
            set
            {
                _games = value;
                onPropertyChanged(nameof(Games));
            }
        }

        public Game SelectedGame
        {
            get { return _selectedGame; }
            set
            {
                _selectedGame = value;
                onPropertyChanged(nameof(SelectedGame));
            }
        }
        #endregion

        #region commands



        #endregion
        private ICommand buyGameCommand;
        public ICommand BuyGameCommand
        {
            get
            {
                if (buyGameCommand == null)
                    buyGameCommand = new RelayCommand(
                    parameter => BuyGame(),
                    predicate => SelectedGame != null && CurrentUser.Money >= SelectedGame.Price
                    );
                return buyGameCommand;
            }
}

#region functions

        public void BuyGame()
        {
        
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            var existingUserGame = db.UserGames
           .FirstOrDefault(ug => ug.UserId == CurrentUser.Id && ug.GameId == SelectedGame.Id);

            if (existingUserGame == null)
            {

                foreach (var ug in db.UserGames.ToList())
                {
                    db.Entry(ug).State = EntityState.Unchanged;
                }

                db.UserGames.Add(new UserGame { UserId = CurrentUser.Id, GameId = SelectedGame.Id });
                db.SaveChanges();
                CurrentMoney -= SelectedGame.Price;
                CurrentUser.Money = CurrentMoney;

                RepositoryUser.ModifyUserInDb(CurrentUser);
            }
            else
            {
                MessageBox.Show("You have this game");
            }

                         
        }


        #endregion


    }





}
