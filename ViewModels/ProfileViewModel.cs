using ProjektProgBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgBD.ViewModels
{
    public class ProfileViewModel : ViewModelBase
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


        public ProfileViewModel(User user)
        {
            CurrUser = user;
        }
    }
}
