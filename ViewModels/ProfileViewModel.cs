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

namespace ProjektProgBD.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {

        public ProfileViewModel(User user)
        {
            _currUser = user;
            _currUser.Games = RepositoryGame.GetUserGamesFromDb(_currUser.Id);
        }

        #region properties
        private User _currUser;
        private Game _selectedGame;
        #endregion

        #region accessors
        public User CurrUser
        {
            get { return _currUser; }
            set
            {
                _currUser = value;
                onPropertyChanged(nameof(CurrUser));
            }
        }

        public Game SelectedGame
        {
            get { return _selectedGame; }
            set
            {
                _selectedGame = value; onPropertyChanged(nameof(SelectedGame));
            }
        }

        #endregion
        #region commands
        
        #endregion

        #region functions
       
        #endregion

    }
}
