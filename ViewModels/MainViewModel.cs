using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjektProgBD.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel() 
        {

        }

        #region private properties
        private string _userName;
        private string _userPassword;

        #endregion
        #region accessors
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                onPropertyChanged(nameof(UserName));
            }
        }

        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                onPropertyChanged(nameof(UserPassword));
            }
        }
        #endregion

        #region commands
        private ICommand logInCommand;
        public ICommand LogInCommand
        {
            get
            {
                if (logInCommand == null)
                    logInCommand = new RelayCommand(
                        parameter => LogIn(parameter), 
                        predicate => true
                        );
                
                return logInCommand;
            }
        }
        #endregion

        #region functions
        //
        // hasla nie przechowujemy w zmiennych dlatego jest returnowane tylko gdy jest potrzebne bo siedzi zaszyfrowane w passwordboxie
        //
        private string GetPassword(object parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                return passwordBox.Password.ToString();
            }
            return "";
        }

        private void LogIn(object parameter)
        {
            MessageBox.Show($"Username:{_userName}\nPassword:{GetPassword(parameter)}");
        }
        #endregion
    }
}
