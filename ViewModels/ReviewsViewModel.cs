using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektProgBD.Models;
using ProjektProgBD.Repositories;
using System.Windows;
using System.Windows.Input;
namespace ProjektProgBD.ViewModels
{
    public class ReviewsViewModel : ViewModelBase
    { 
        public ReviewsViewModel(User user)
        {
            CurrentUser = user;
        }

        #region properties
        private User _currentUser;
        private Game _selectedGame;
        private string _reviewContent;
        private int[] _ratings;
        private int _selectedRating;
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
                    UserId = CurrentUser.Id,
                    GameId = SelectedGame.Id
                };

                if (RepositoryReview.AddReviewToDb(newReview))
                {
                    MessageBox.Show("Review Added");
                    ReviewContent = string.Empty;
                }
            }

        }

        #endregion
    }
}
