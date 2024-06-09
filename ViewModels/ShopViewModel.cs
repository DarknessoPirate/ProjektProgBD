using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjektProgBD.Models;
using ProjektProgBD.Repositories;
using ProjektProgBD.Views;
using System;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Windows.Input;

namespace ProjektProgBD.ViewModels
{
    public class ShopViewModel : ViewModelBase
    {
        public ShopViewModel(User user)
        {
            CurrentUser = user;
            CurrentMoney = CurrentUser.Money;
            Games = Games = RepositoryGame.GetAllGamesFromDb();
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
        private ICommand openGameWindowCommand;
        public ICommand OpenGameWindowCommand
        {
            get
            {
                if (openGameWindowCommand == null)
                    openGameWindowCommand = new RelayCommand(
                       parameter => OpenGameWindow(parameter),
                       predicate => true
                       );
                return openGameWindowCommand;
            }
        }

        #region functions

        public void BuyGame()
        {
        
            var db = App.ServiceProvider.GetRequiredService<ShopDbContext>();
            if (!CurrentUser.Games.Contains(SelectedGame))
            {
                db.UserGames.Add(new UserGame { UserId = CurrentUser.Id, GameId = SelectedGame.Id });
                db.SaveChanges();
                CurrentMoney -= SelectedGame.Price;
                CurrentUser.Money = CurrentMoney;
                RepositoryUser.ModifyUserInDb(CurrentUser);

            }              
        }

        private void OpenGameWindow(object parameter)
        {
            var game = parameter as Game;
            if (game != null)
            {
                var GameWindow = new GameDetailsWindow();
                GameWindow.DataContext = new GameDetailsWindowViewModel(_currentUser, game);
                GameWindow.Show();
            }
        }


        #endregion


    }





}
