using ProjektProgBD.Models;
using ProjektProgBD.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Input;
using System.Windows;
using ProjektProgBD.Views;

namespace ProjektProgBD.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {

        public ProfileViewModel(User user)
        {
            _currentUser = user;
            _games = RepositoryGame.GetUserGamesFromDb(_currentUser.Id);
        }

        #region properties
        private User _currentUser;
        private Game _selectedGame;
        private ObservableCollection<Game> _games;
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

        public Game SelectedGame
        {
            get { return _selectedGame; }
            set
            {
                _selectedGame = value;
                onPropertyChanged(nameof(SelectedGame));
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

       

        #endregion
        #region commands
      
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

        private ICommand refreshGamesCommand;
        public ICommand RefreshGamesCommand
        {
            get
            {
                if (refreshGamesCommand == null)
                    refreshGamesCommand = new RelayCommand(
                       parameter => RefreshGames(),
                       predicate => true
                       );
                return refreshGamesCommand;
            }
        }

        #endregion

        #region functions

        private void OpenGameWindow(object parameter)
        {
            var game = parameter as Game;
            if (game != null)
            {
                var GameWindow = new GameDetailsWindow();
                GameWindow.DataContext = new GameDetailsWindowViewModel(CurrentUser, game);
                GameWindow.Show();
            }
        }

        private void RefreshGames()
        {
            Games = RepositoryGame.GetUserGamesFromDb(CurrentUser.Id);
        }

        #endregion

    }
}
