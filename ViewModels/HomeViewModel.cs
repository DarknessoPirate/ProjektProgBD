

using ProjektProgBD.Models;

namespace ProjektProgBD.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private User _currentUser;
        public User CurrentUser {
            get { return _currentUser; }
            set 
            {
                _currentUser = value;
                onPropertyChanged(nameof(CurrentUser)); 
            }
        }


        public HomeViewModel(User user)
        {
            CurrentUser = user;
        }
    }
}
