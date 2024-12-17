using Fieldify_App.Commands;
using Fieldify_App.Data;
using Fieldify_App.Models;
using Fieldify_App.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Fieldify_App.ViewModels
{
    internal class BookingPageVM : ViewModelBase
    {

        private ObservableCollection<Teren> _fields;

        public ObservableCollection<Teren> Fields
        {
            get => _fields;
            set
            {
                _fields = value;
                OnPropertyChanged(nameof(Fields));
            }
        }

        public User CurrentUser { get; }

        public BookingPageVM(User loggedInUser)
        {
            CurrentUser = loggedInUser;
            LoadFields();
            CurrentView = this;

            IsAdmin = loggedInUser.UserTypeId == 1;

            OpenAddFieldWindowCommand = new RelayCommand<object>(_ => OpenAddFieldWindow());

            BookFieldCommand = new RelayCommand<object>(
                parameter => BookField(parameter as Teren)
            );

            DeleteFieldCommand = new RelayCommand<Teren>(DeleteField);
        }

        public BookingPageVM()
        {
            LoadFields();
            // Inițial setăm pagina curentă ca fiind cea principală
            CurrentView = this;

            // Inițializăm comanda folosind RelayCommand
            BookFieldCommand = new RelayCommand<object>(
                parameter => BookField(parameter as Teren)
        );
        }

        private void DeleteField(Teren field)
        {
            if (field == null) return;

            // Confirmarea ștergerii
            if (MessageBox.Show($"Sigur doriți să ștergeți terenul \"{field.Nume}\"?", "Confirmare", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Ștergem din baza de date și din listă
                if (BookingPageModel.DeleteField(field.Id))
                {
                    Fields.Remove(field);
                    MessageBox.Show("Terenul a fost șters cu succes!");
                }
                else
                {
                    MessageBox.Show("A apărut o eroare la ștergerea terenului.");
                }
            }
        }

        private void LoadFields()
        {
            // Obținem datele din model și le convertim într-o ObservableCollection
            var fields = BookingPageModel.GetFields();
            Fields = new ObservableCollection<Teren>(fields);

        }

        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand BookFieldCommand { get; }
        public ICommand OpenAddFieldWindowCommand { get; }
        public ICommand DeleteFieldCommand { get; } 



        private void BookField(Teren selectedField)
        {
            CurrentView = new BookingDetailsPageVM(
                () => CurrentView = this,
                selectedField,
                CurrentUser // Transmiți utilizatorul curent
            );
        }

        private void OpenAddFieldWindow()
        {
            // Deschide un nou Window pentru adăugarea unui teren
            var addFieldWindow = new AddFieldWindow();
            addFieldWindow.ShowDialog();
        }

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
    }
}
