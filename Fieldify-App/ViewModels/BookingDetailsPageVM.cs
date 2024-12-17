using Fieldify_App.Commands;
using Fieldify_App.Data;
using Fieldify_App.Models;
using Fieldify_App.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;   
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Fieldify_App.ViewModels
{
    internal class BookingDetailsPageVM : ViewModelBase
    {

        public ICommand ScheduleCommand { get; }
        public ICommand BackCommand { get; }

        public Teren SelectedField { get; set; }
        public ObservableCollection<string> AvailableHours { get; set; }

        public User CurrentUser { get; }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                Console.WriteLine($"SelectedDate setat la: {_selectedDate}");
                OnPropertyChanged(nameof(SelectedDate)); // Notifică UI-ul despre schimbare
                if (_selectedDate.HasValue)
                {
                    UpdateAvailableHours(); // Actualizează orele disponibile
                }
            }
        }

        private bool _isHourSelected;
        public bool IsHourSelected
        {
            get => _isHourSelected;
            set
            {
                _isHourSelected = value;
                OnPropertyChanged(nameof(IsHourSelected)); // Notifică UI-ul despre schimbare
            }
        }

        private Visibility _scheduleButtonVisibility = Visibility.Collapsed;
        public Visibility ScheduleButtonVisibility
        {
            get => _scheduleButtonVisibility;
            set
            {
                _scheduleButtonVisibility = value;
                OnPropertyChanged(nameof(ScheduleButtonVisibility)); // Notifică UI-ul despre schimbare
            }
        }

        private string _selectedHour;
        public string SelectedHour
        {
            get => _selectedHour;
            set
            {
                _selectedHour = value;
                IsHourSelected = !string.IsNullOrEmpty(_selectedHour); // Actualizează starea butonului
                ScheduleButtonVisibility = IsHourSelected ? Visibility.Visible : Visibility.Collapsed;
                OnPropertyChanged(nameof(SelectedHour)); // Notifică UI-ul
            }
        }

        public BookingDetailsPageVM(Action navigateBack, Teren selectedField, User currentUser)
        {
            SelectedField = selectedField;
            CurrentUser = currentUser; // Salvezi utilizatorul curent

            BackCommand = new RelayCommand<object>(param => navigateBack());
            ScheduleCommand = new RelayCommand<object>(param => ScheduleBooking(), param => IsHourSelected);

            AvailableHours = new ObservableCollection<string>
            {
                "10:00 - 12:00",
                "12:00 - 14:00",
                "14:00 - 16:00",
                "16:00 - 18:00",
                "18:00 - 20:00",
                "20:00 - 22:00"
            };
        }


        private void UpdateAvailableHours()
        {

            try
            {
                if (SelectedField == null || !SelectedDate.HasValue)
                {
                    throw new Exception("SelectedField sau SelectedDate este null.");
                }

                var allHours = new[]
                {
                    "10:00 - 12:00", "12:00 - 14:00", "14:00 - 16:00",
                    "16:00 - 18:00", "18:00 - 20:00", "20:00 - 22:00"
                };

                var bookedHours = BookingDetailsPageModel.GetBookedHours(SelectedField.Id, SelectedDate.Value);

                var freeHours = allHours.Except(bookedHours).ToList();


                    if (AvailableHours.Count != 0)
                        AvailableHours.Clear();
                    foreach (var hour in freeHours)
                    {
                        AvailableHours.Add(hour);
                    }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la actualizarea orelor disponibile: {ex.Message}");
            }

        }

        private void ScheduleBooking()
        {


            if (!string.IsNullOrEmpty(SelectedHour) && SelectedDate.HasValue && SelectedField != null)
            {
                try
                {
                    // Separă ora de început și de sfârșit din SelectedHour
                    var hourParts = SelectedHour.Split(new[] { " - " }, StringSplitOptions.None);
                    if (hourParts.Length != 2)
                    {
                        MessageBox.Show("Ora selectată este invalidă.");
                        return;
                    }

                    var oraInceput = TimeSpan.Parse(hourParts[0]);
                    var oraSfarsit = TimeSpan.Parse(hourParts[1]);


                    var success = BookingDetailsPageModel.SaveBooking(
                       SelectedField.Id, SelectedDate.Value.Date, oraInceput, oraSfarsit, CurrentUser.Id);

                    if (success)
                    {
                        var customMessageBox = new CustomMessageBox(SelectedDate.Value.ToShortDateString(), SelectedField.Nume);
                        customMessageBox.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("A apărut o eroare la salvarea rezervării.");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"A apărut o eroare la salvarea rezervării: {ex.Message}");
                }
            }
        }
    }
}
