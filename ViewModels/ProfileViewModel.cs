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

namespace ProjektProgBD.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {

        public ProfileViewModel(User user)
        {
            _currentUser = RepositoryUser.GetUserFromDb(user);
            _currentUser.Games = RepositoryGame.GetUserGamesFromDb(_currentUser.Id);

        }

        #region properties
        private User _currentUser;
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
        private ICommand submitReviewCommand;
        
        #endregion

        #region functions
        

        #endregion

    }
}
