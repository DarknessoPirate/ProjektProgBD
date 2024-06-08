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
using System.Net.Mail;

namespace ProjektProgBD.ViewModels
{
    public class RegistrationWindowViewModel :ViewModelBase
    {
        #region private properties

        private string _userEmail;
        private int _isEmailVaild;
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

        public int IsEmailValid
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
                        predicate => !GetPassword(predicate).IsNullOrEmpty() && !UserName.IsNullOrEmpty() && IsEmailValid == 1// ADD USERNAME CHECK HERE
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
            try
            {
                var newUser = new User { Name = _userName, Password = GetPassword(parameter), Email = _userEmail, Money = 100 };
                newUser.Password = newUser.GetHashPassword();
                if (RepositoryUser.AddUserToDb(newUser))
                    MessageBox.Show("Account created!");
                OnRequestClose();
            }catch(Microsoft.EntityFrameworkCore.DbUpdateException exception)
            {
                MessageBox.Show(exception.InnerException.Message);
            }
            
           
        }

        private void ValidateEmail()
        {
            try
            {
                var address = new MailAddress(UserEmail).Address;
                // Regular expression to enforce more strict email validation
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (Regex.IsMatch(UserEmail, pattern))
                {
                    IsEmailValid = 1;
                }
                else
                {
                    IsEmailValid = -1;
                }
            }
            catch (FormatException)
            {
                if (UserEmail == null)
                {
                    IsEmailValid = 0;
                }
                else
                {
                    IsEmailValid = -1;
                }
            }
            catch (ArgumentException)
            {
                IsEmailValid = -1;
            }

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
