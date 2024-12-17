using Fieldify_App.Commands;
using Fieldify_App.Data;
using Fieldify_App.Models;
using Fieldify_App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Fieldify_App.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _email;
        private string _password;

        public ICommand LoginCommand { get; }
        public ICommand GoToSignup { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<object>(_ => LoggedIn());
            GoToSignup = new RelayCommand<object>(_ => SignupWindow());
        }

        private void SignupWindow()
        {
            SignupWindow signupWindow = new SignupWindow();
            signupWindow.Show();

            Application.Current.Windows[0]?.Close();
        }

        private void LoggedIn()
        {
            User _currentUser = LoginModel.ValidateUser(_email, _password);

            if (_currentUser != null)
            {
                MainWindow mainWindow = new MainWindow(_currentUser);
                mainWindow.Show();
        
                Application.Current.Windows[0]?.Close();

            }
            else
            {
                // Afișare mesaj de eroare dacă autentificarea eșuează
                MessageBox.Show($"Invalid email or password.\nEmail: {Email}\nPassword: {Password}");
            }
        }

        public string Email { 
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(_email));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(_password));
            }
        }

    }
}
