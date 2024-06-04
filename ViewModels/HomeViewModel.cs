

using ProjektProgBD.Models;

namespace ProjektProgBD.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private User _currUser;
        public User CurrUser {
            get { return _currUser; }
            set 
            {
                _currUser = value;
                onPropertyChanged(nameof(CurrUser)); 
            }
        }


        public HomeViewModel(User user)
        {
            CurrUser = user;
        }
    }
}
