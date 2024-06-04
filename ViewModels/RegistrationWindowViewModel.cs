using Microsoft.IdentityModel.Tokens;
using ProjektProgBD.Models;
using ProjektProgBD.Repositories;
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
    public class RegistrationWindowViewModel :ViewModelBase
    {
        #region private properties
        private string _userName;

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
        #endregion

        #region commands
        private ICommand registerCommand;
        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                    registerCommand = new RelayCommand(
                        parameter => Register(parameter),
                        predicate => !GetPassword(predicate).IsNullOrEmpty() && !UserName.IsNullOrEmpty()
                        );

                return registerCommand;
            }
        }
        #endregion

        #region functions
        private string GetPassword(object parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                return passwordBox.Password.ToString();
            }
            return "";
        }

        private void Register(object parameter)
        {
            var newUser = new User { Name = _userName , Password = GetPassword(parameter)};
            newUser.Password = newUser.GetHashPassword();
            if (RepositoryUser.AddUserToDb(newUser))
                MessageBox.Show("Account created!");
                OnRequestClose();
        }

        #endregion

        #region events
        public event EventHandler RequestClose;

        protected void OnRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
        #endregion

    }
}
