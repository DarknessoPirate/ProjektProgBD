using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektProgBD.Repositories;
using ProjektProgBD.Models;


namespace ProjektProgBD.ViewModels
{
    public class GameAddViewModel : ViewModelBase
    {
        public GameAddViewModel() 
        {
            Games = RepositoryGame.GetAllGamesFromDb();
        }
        #region properties
        private ObservableCollection<Game> _games;

        #endregion

        #region accessors
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






    }



}
