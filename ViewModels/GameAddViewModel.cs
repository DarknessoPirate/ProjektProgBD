using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektProgBD.Repositories;
using ProjektProgBD.Models;
using System.Windows;
using System.Windows.Input;


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
        private string _gameName;
        private string _gamePrice;
        private Game _selectedGame;

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

        public Game SelectedGame
        {
            get { return _selectedGame; }
            set
            {
                _selectedGame = value;
                onPropertyChanged(nameof(SelectedGame));
            }
        }

        public string GameName
        {
            get { return _gameName; }
            set
            {
                _gameName = value;
                onPropertyChanged(nameof(GameName));
            }
        }
        public string GamePrice
        {
            get { return _gamePrice; }
            set
            {
                _gamePrice = value;
                onPropertyChanged(nameof(GamePrice));
            }
        }

        #endregion

        #region commands

        private ICommand addGameCommand;

        public ICommand AddGameCommand 
        {
            get
            {
                if (addGameCommand == null)
                    addGameCommand = new RelayCommand(
                            parameter => AddGame(),
                            predicate => GameName != null && GameName != "" &&
                                         GamePrice != null && GamePrice != ""      
                        );
                return addGameCommand;
            }

        }
        private ICommand modifyGameCommand;
        public ICommand ModifyGameCommand
        {
            get
            {
                if (modifyGameCommand == null)
                    modifyGameCommand = new RelayCommand(
                        parameter => ModifyGame(),
                        predicate => SelectedGame != null && GameName != null && GameName != "" && 
                                     GamePrice != null && GamePrice != ""
                        );
                return modifyGameCommand;
            }
        }

        private ICommand deleteGameCommand;
        public ICommand DeleteGameCommand
        {
            get
            {
                if (deleteGameCommand == null)
                    deleteGameCommand = new RelayCommand(
                        parameter => DeleteGame(),
                        predicate => SelectedGame != null
                        );
                return deleteGameCommand;
            }
        }


        #endregion

        #region functions
        private void AddGame()
        {
            var decimalPrice = new Decimal();
            if (Decimal.TryParse(GamePrice, out decimalPrice))
            {
                var newGame = new Game
                {
                    Name = GameName,
                    Price = decimalPrice
                };

                if (RepositoryGame.AddGameToDb(newGame))
                {
                    MessageBox.Show("Game Added to DB!");
                    Games.Add(newGame);
                    GameName = string.Empty;
                    GamePrice = string.Empty;
                }
                else
                {
                    MessageBox.Show("Failed to add game to DB");
                }
            }
            else
            {
                MessageBox.Show("Incorrect Price Format");
            }
        }

        private void DeleteGame()
        {
            if (RepositoryGame.DeleteGameFromDb(SelectedGame.Id))
            {
                Games.Remove(SelectedGame);
                MessageBox.Show("Removed Game from DB!");
            }
            else
            {
                MessageBox.Show("Failed to remove game from DB");
            }
        }

        private void ModifyGame()
        {
            var decimalPrice = new Decimal();
            if (Decimal.TryParse(GamePrice, out decimalPrice))
            {
                
                if (RepositoryGame.ModifyGameInDb(SelectedGame.Id, GameName, decimalPrice))
                {
                    int indexOfSelectedGame = Games.IndexOf(SelectedGame);
                    var newGame = new Game
                    {
                        Name = GameName,
                        Price = decimalPrice
                    };
                    Games[indexOfSelectedGame] = newGame;
                    GameName = string.Empty;
                    GamePrice = string.Empty;
                }
                else
                {
                    MessageBox.Show("Failed to modify game in DB");
                }
            }
            else
            {
                MessageBox.Show("Incorrect price format");
            }
        }


        #endregion





    }



}
