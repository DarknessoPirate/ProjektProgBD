using Microsoft.IdentityModel.Tokens;
using ProjektProgBD.Models;
using ProjektProgBD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjektProgBD.ViewModels
{
    public class RegistrationWindowViewModel :ViewModelBase
    {
        #region private properties

        private string _userEmail;
        private bool _isEmailVaild;
        private string _userName;
        private bool _isUserNameValid;
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

        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                _userEmail = value;
                onPropertyChanged(nameof(UserEmail));
                ValidateEmail();
            }
        }

        public bool IsEmailValid
        {
            get { return _isEmailVaild; }
            set
            {
                _isEmailVaild = value;
                onPropertyChanged(nameof(IsEmailValid));
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
                        predicate => !GetPassword(predicate).IsNullOrEmpty() && !UserName.IsNullOrEmpty() && IsEmailValid// ADD USERNAME CHECK HERE
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

            var newUser = new User { Name = _userName, Password = GetPassword(parameter) , Email=_userEmail, Money=100};
            newUser.Password = newUser.GetHashPassword();
            if (RepositoryUser.AddUserToDb(newUser))
                MessageBox.Show("Account created!");
            OnRequestClose();
        }

        private void ValidateEmail()
        {
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex regex = new Regex(pattern);
            IsEmailValid = regex.IsMatch(UserEmail);
        }

        private void ValidateUserName()
        {
            /*
             *  IMPLEMENT THIS LATER
             *  IMPLEMENT THIS LATER
             *  IMPLEMENT THIS LATER
             */
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
