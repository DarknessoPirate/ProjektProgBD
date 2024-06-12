

using ProjektProgBD.Models;
using System.Windows.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjektProgBD.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(User user)
        {
            CurrentUser = user;
            CurrentDate = DateTime.Now.ToString("dd.MM.yyyy");
            CurrentTime = DateTime.Now.ToString("HH:mm");

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        #region properties
        private User _currentUser;
        private string _currentDate;
        private string _currentTime;
        private DispatcherTimer _timer;
        #endregion

        #region accessors
        public User CurrentUser {
            get { return _currentUser; }
            set 
            {
                _currentUser = value;
                onPropertyChanged(nameof(CurrentUser)); 
            }
        }

        public string CurrentDate
        {
            get
            {
                return _currentDate;
            }
            set
            {
                _currentDate = value;
                onPropertyChanged(nameof(CurrentDate));
            }
        }

        public string CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                _currentTime = value;
                onPropertyChanged(nameof(CurrentTime));
            }
        }
        #endregion

        #region commands
        #endregion

        #region functions
        private void TimerTick(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToString("HH:mm");
        }
        #endregion





        
    }
}
