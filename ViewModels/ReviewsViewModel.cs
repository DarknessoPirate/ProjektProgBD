using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektProgBD.Models;
using ProjektProgBD.Repositories;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
namespace ProjektProgBD.ViewModels
{
    public class ReviewsViewModel : ViewModelBase
    { 
        public ReviewsViewModel(User user)
        {
            _currentUser = user;
            _reviews = RepositoryReview.GetAllReviews();
            _ratings = [1, 2, 3, 4, 5];
        }

        #region properties
        private User _currentUser;
        private Review _selectedReview;
        private ObservableCollection<Review> _reviews;
        private string _reviewContent;
        private int[] _ratings;
        private int _selectedRating;
        private string _reviewAuthor;
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

        public Review SelectedReview
        {
            get { return _selectedReview; }
            set
            {
                _selectedReview = value;
                onPropertyChanged(nameof(SelectedReview));
            }
        }

        public ObservableCollection<Review> Reviews
        {
            get { return _reviews; }
            set
            {
                _reviews = value;
                onPropertyChanged(nameof(Reviews));
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

        public string ReviewAuthor
        {
            get { return _reviewAuthor; }
            set 
            { 
                _reviewAuthor = value;
                onPropertyChanged(nameof(ReviewAuthor));
            }
        }

        #endregion

        #region commands
        private ICommand editReviewCommand;
        public ICommand EditReviewCommand
        {
            get
            {
                if (editReviewCommand == null)
                    editReviewCommand = new RelayCommand(
                        parameter => EditReview(),
                        predicate => SelectedReview != null &&
                                     SelectedRating >= 1 && SelectedRating <= 5 &&
                                     ReviewContent != ""
                        );
                return editReviewCommand;
            }
        }

        private ICommand deleteReviewCommand;
        public ICommand DeleteReviewCommand
        {
            get
            {
                if (deleteReviewCommand == null)
                    deleteReviewCommand = new RelayCommand(
                        parameter => DeleteReview(),
                        predicate => SelectedReview != null
                        );
                return deleteReviewCommand;
            }
        }
        private ICommand yourReviewsCommand;
        public ICommand YourReviewsCommand
        {
            get
            {
                if (yourReviewsCommand == null)
                    yourReviewsCommand = new RelayCommand(
                        parameter => ShowCurrentUserReviews(),
                        predicate => true
                        );
                return yourReviewsCommand;
            }
        }

        private ICommand allReviewsCommand;
        public ICommand AllReviewsCommand
        {
            get
            {
                if (allReviewsCommand == null)
                    allReviewsCommand = new RelayCommand(
                        parameter => ShowAllReviews(),
                        predicate => true
                        );
                return allReviewsCommand;
            }
        }

        private ICommand reviewAuthorReviewsCommand;
        public ICommand ReviewAuthorReviewsCommand
        {
            get
            {
                if (reviewAuthorReviewsCommand == null)
                    reviewAuthorReviewsCommand = new RelayCommand(
                        parameter => ShowAuthorFromTextBoxReviews(),
                        predicate => ReviewAuthor != ""
                        );
                return reviewAuthorReviewsCommand;
            }
        }

        #endregion

        #region functions
        private void EditReview()
        {
            if (!string.IsNullOrWhiteSpace(ReviewContent) && SelectedReview.UserId == CurrentUser.Id)
            {
                
                var newReview = new Review
                {
                    Id = SelectedReview.Id,
                    Content = ReviewContent,
                    Score = SelectedRating,
                    UserId = CurrentUser.Id,
                    GameId = SelectedReview.GameId,
                    Game = SelectedReview.Game,
                    User = SelectedReview.User,
                };
            

                if (RepositoryReview.ModifyReviewInDb(SelectedReview.Id, ReviewContent, SelectedRating))
                {                   
                    int index = Reviews.IndexOf(SelectedReview);

                    Reviews[index] = newReview;

                    ReviewContent = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("You can only edit your reviews");
            }
        }

        private void DeleteReview()
        {
            if (SelectedReview != null && SelectedReview.UserId == CurrentUser.Id)
            {
                if (RepositoryReview.DeleteReviewFromDb(SelectedReview.Id))
                {
                    Reviews.Remove(SelectedReview);
                    SelectedReview = null;
                }           
            }
            else
            {
                MessageBox.Show("You can only delete your reviews");
            }
        }

        private void ShowCurrentUserReviews()
        {
            Reviews = RepositoryReview.GetUserReviewsFromDb(CurrentUser.Id);
        }

        private void ShowAllReviews()
        {
            Reviews = RepositoryReview.GetAllReviews();
        }

        private void ShowAuthorFromTextBoxReviews()
        {
            if (RepositoryUser.FindUserInDB(ReviewAuthor))
            {
                var author = RepositoryUser.GetUserFromDb(ReviewAuthor);
                Reviews = RepositoryReview.GetUserReviewsFromDb(author.Id);
            }
            else
            {
                MessageBox.Show("User with this username does not exist");
            }
        }

        #endregion
    }
}
