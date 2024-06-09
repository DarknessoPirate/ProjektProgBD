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
using Microsoft.Win32;
using System.IO;


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
        private string _coverImagePath;
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

        public string CoverImagePath
        {
            get { return _coverImagePath; }
            set
            {
                _coverImagePath = value;
                onPropertyChanged(nameof(CoverImagePath));
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
                                         GamePrice != null && GamePrice != "" &&
                                         CoverImagePath != null && CoverImagePath != ""
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
                                     GamePrice != null && GamePrice != "" &&
                                    CoverImagePath != null && CoverImagePath != ""
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

        private ICommand selectCoverImageCommand;
        public ICommand SelectCoverImageCommand
        {
            get
            {
                if (selectCoverImageCommand == null)
                    selectCoverImageCommand = new RelayCommand(
                        parameter => SelectCoverImage(),
                        predicate => true
                    );
                return selectCoverImageCommand;
            }
        }

        #endregion

        #region functions

        private void SelectCoverImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                CoverImagePath = openFileDialog.FileName;
            }
        }

        private string SaveCoverImage(string originalPath)
        {
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CoverImages");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string fileName = Path.GetFileName(originalPath);
            string destinationPath = Path.Combine(directory, fileName);

            if (!File.Exists(destinationPath))
            {
                File.Copy(originalPath, destinationPath);
            }

            return Path.Combine("CoverImages", fileName);
        }

        private void AddGame()
        {
            var decimalPrice = new Decimal();
            if (Decimal.TryParse(GamePrice, out decimalPrice))
            {
                string relativePath = SaveCoverImage(CoverImagePath);

                var newGame = new Game
                {
                    Name = GameName,
                    Price = decimalPrice,
                    CoverImagePath = relativePath
                };

                if (RepositoryGame.AddGameToDb(newGame))
                {
                    MessageBox.Show("Game Added to DB!");
                    Games.Add(newGame);
                    GameName = string.Empty;
                    GamePrice = string.Empty;
                    CoverImagePath = string.Empty;
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
                string relativePath = SaveCoverImage(CoverImagePath);

                if (RepositoryGame.ModifyGameInDb(SelectedGame.Id, GameName, decimalPrice))
                {
                    int indexOfSelectedGame = Games.IndexOf(SelectedGame);
                    var newGame = new Game
                    {
                        Name = GameName,
                        Price = decimalPrice,
                        CoverImagePath = relativePath
                    };
                    Games[indexOfSelectedGame] = newGame;
                    GameName = string.Empty;
                    GamePrice = string.Empty;
                    CoverImagePath = string.Empty;
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
