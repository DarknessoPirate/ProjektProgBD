using ProjektProgBD.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProjektProgBD.Models;
using ProjektProgBD.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace ProjektProgBD.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel() 
        {
            Tabs = new ObservableCollection<TabItem>();
            
        }

        #region properties
        private string _userName;
        private User _currentUser;
        public ObservableCollection<TabItem> Tabs { get; set; }


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

        public User CurrentUser
        {
            get { return _currentUser; }
            set 
            { 
                _currentUser = value;
                onPropertyChanged(nameof(CurrentUser));
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
                        predicate => !GetPassword(predicate).IsNullOrEmpty() && !UserName.IsNullOrEmpty()
                        );
                
                return logInCommand;
            }
        }

        private ICommand openRegistrationWindowCommand;
        public ICommand OpenRegistrationWindowCommand
        {
            get
            {
                if(openRegistrationWindowCommand == null)
                    openRegistrationWindowCommand = new RelayCommand(
                       parameter => OpenRegistrationWindow(),
                       predicate => true
                       ); 
                return openRegistrationWindowCommand;            
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

        private void SetPassword(object parameter, string text)
        {
            if (parameter is PasswordBox passwordBox)
            {
                passwordBox.Password = text;
            }
        }

        private void LogIn(object parameter)
        {
            User newUser = new User { Name = _userName, Password = GetPassword(parameter) };
            newUser.Password = newUser.GetHashPassword();
            bool foundUser = RepositoryUser.FindUserInDB(newUser);
            if (foundUser)
            {
                Tabs.Add(new TabItem { Header = "Home", Content = new HomeView() });
                Tabs.Add(new TabItem { Header = "Shop", Content = new ShopView() });
                Tabs.Add(new TabItem { Header = "Profile", Content = new ProfileView() });
                CurrentUser = newUser;
                MessageBox.Show("Logged in :)");
            }
            else
            {
                MessageBox.Show("Credentials not found");
                UserName = string.Empty;
                SetPassword(parameter, string.Empty);
            }
        }
        #endregion

        private void OpenRegistrationWindow()
        {
            var registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }
    }
}
