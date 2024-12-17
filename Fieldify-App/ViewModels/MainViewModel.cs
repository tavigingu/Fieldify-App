using Fieldify_App.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Fieldify_App.Commands;
using Fieldify_App.Views.UserControls;

namespace Fieldify_App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private User _currentUser;
        private object _currentPage;

        public ICommand NavigateCommand { get; }

        public MainViewModel() {  }

        public MainViewModel(User loggedInUser)
        {
            CurrentUser = loggedInUser;
            CurrentPage = new MyProfilePage(CurrentUser);

            NavigateCommand = new RelayCommand<object>(NavigateToPage);
        }

        public void NavigateToPage(object parameter)
        {
            if (parameter is string pageName)
            {
                // Aici schimbăm pagina în funcție de numele ei
                switch (pageName)
                {
                    case "MyProfile":
                        CurrentPage = new MyProfilePageVM(CurrentUser);
                        break;
                        //case "Bookings":
                        //    CurrentPage = new BookingsPageVM(CurrentUser);
                        //    break;
                        //case "Home":
                        //    CurrentPage = new HomePageVM();
                        //    break;
                        //default:
                        //    CurrentPage = new ErrorPageVM(); // Pentru situații neașteptate
                        //    break;
                }
            }
        }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public object CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

    }
     
 }
