using ProjektProgBD.Models;

namespace ProjektProgBD.ViewModels
{
    public class ShopViewModel : ViewModelBase
    {
        private User _currUser;
        public User CurrUser
        {
            get { return _currUser; }
            set
            {
                _currUser = value;
                onPropertyChanged(nameof(CurrUser));
            }
        }


        public ShopViewModel(User user)
        {
            CurrUser = user;
        }
    }
}
