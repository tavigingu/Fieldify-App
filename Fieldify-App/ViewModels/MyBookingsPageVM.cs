using Fieldify_App.Commands;
using Fieldify_App.Data;
using Fieldify_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace Fieldify_App.ViewModels
{
    public class MyBookingsPageVM : ViewModelBase
    {
        public MyBookingsPageVM(User user)
        {

            IsAdmin = user.UserTypeId == 1;

            LoadUser(user);
            LoadBookings();

            CancelBookingCommand = new RelayCommand<object>(
               parameter => CancelBooking(parameter as Programare));

        }

        public ICommand CancelBookingCommand { get; }

        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        private void CancelBooking(Programare programare)
        {

            if (programare == null)
                return;

            // Actualizează statusul în baza de date
            bool success = MyBookingsPageModel.SetBookingStatusToCancelled(programare.Id);

            if (success)
            {
                // Actualizează statusul local
                programare.StatusProgramare = "Anulata";

                // Notifică UI-ul despre schimbare
                OnPropertyChanged(nameof(Bookings));
            }
            else
            {
                MessageBox.Show("Anularea programării a eșuat. Încercați din nou.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        private ObservableCollection<Programare> _bookings;
        public ObservableCollection<Programare> Bookings
        {
            get => _bookings;
            set
            {
                _bookings = value;
                OnPropertyChanged(nameof(Bookings));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public TimeSpan OraInceput { get; set; }
        public TimeSpan OraSfarsit { get; set; }

        // Proprietate care combină ora de început și ora de sfârșit
        public string OraInceputSfarsit
        {
            get
            {
                return $"{OraInceput} - {OraSfarsit}";
            }
        }

        private void LoadUser(User user)
        {
            CurrentUser = user;

            if (CurrentUser != null)
            {
                Email = CurrentUser.Email;           
            }
        }

        private void LoadBookings()
        {   
            
            
            var bookings = MyBookingsPageModel.GetBookingsByUserEmail(CurrentUser.Email, IsAdmin);

            Bookings = new ObservableCollection<Programare>((List<Programare>)bookings);
        }

    }
}
