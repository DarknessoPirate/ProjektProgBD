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
            _currUser = RepositoryUser.GetUserFromDb(user);
            _currUser.Games = RepositoryGame.GetUserGamesFromDb(_currUser.Id);
            _ratings = [1, 2, 3, 4, 5];
        }

        #region properties
        private User _currUser;
        private Game _selectedGame;
        private string _reviewContent;
        private int[] _ratings;
        private int _selectedRating;
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
                _selectedGame = value;
                onPropertyChanged(nameof(SelectedGame));
            }
        }

        public string ReviewContent
        {
            get { return _reviewContent; }
            set
            {
                _reviewContent = value;
                onPropertyChanged(nameof(ReviewContent));
            }
        }

        public int[] Ratings
        {
            get { return _ratings; }
            set
            {
                _ratings = value;
                onPropertyChanged(nameof(Ratings));
            }
        }

        public int SelectedRating
        {
            get { return _selectedRating; }
            set
            {
                _selectedRating = value;
                onPropertyChanged(nameof(SelectedRating));
            }
        }

        #endregion
        #region commands
        private ICommand submitReviewCommand;
        public ICommand SubmitReviewCommand
        {
            get
            {
                if (submitReviewCommand == null)
                    submitReviewCommand = new RelayCommand(
                            parameter => AddReview(),
                            predicate => ReviewContent != "" &&
                                         SelectedGame != null &&
                                         SelectedRating >= 1 && SelectedRating <= 5
                        );
                return submitReviewCommand;
            }
        }
        #endregion

        #region functions
        private void AddReview()
        {
            if (!string.IsNullOrEmpty(ReviewContent))
            {
                var newReview = new Review
                {
                    Content = ReviewContent,
                    Score = SelectedRating,
                    UserId = CurrUser.Id,
                    GameId = SelectedGame.Id
                };

                if(RepositoryReview.AddReviewToDb(newReview))
                {
                    MessageBox.Show("Review Added");
                    ReviewContent = string.Empty;
                }
            }

        }

        #endregion

    }
}
