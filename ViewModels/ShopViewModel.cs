using ProjektProgBD.Models;

namespace ProjektProgBD.ViewModels
{
    public class ShopViewModel : ViewModelBase
    {
        private User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                onPropertyChanged(nameof(CurrentUser));
            }
        }


        public ShopViewModel(User user)
        {
            CurrentUser = user;
        }
    }
}
