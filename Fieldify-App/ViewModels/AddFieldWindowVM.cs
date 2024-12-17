using Fieldify_App.Commands;
using Fieldify_App.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Fieldify_App.Models;
using Fieldify_App.Views;
using System.Windows;

namespace Fieldify_App.ViewModels
{
    internal class AddFieldWindowVM : ViewModelBase
    {

        private string _fieldName;
        private string _description;
        private string _price;
        private bool _isFootballSelected;
        private bool _isBasketballSelected;
        private bool _isTennisSelected;


        // Properties for data binding
        public string FieldName
        {
            get => _fieldName;
            set
            {
                _fieldName = value;
                OnPropertyChanged(nameof(FieldName));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public bool IsFootballSelected
        {
            get => _isFootballSelected;
            set
            {
                _isFootballSelected = value;
                OnPropertyChanged(nameof(IsFootballSelected));
            }
        }

        public bool IsBasketballSelected
        {
            get => _isBasketballSelected;
            set
            {
                _isBasketballSelected = value;
                OnPropertyChanged(nameof(IsBasketballSelected));
            }
        }

        public bool IsTennisSelected
        {
            get => _isTennisSelected;
            set
            {
                _isTennisSelected = value;
                OnPropertyChanged(nameof(IsTennisSelected));
            }
        }

        // Command for the OK Button
        public ICommand AddFieldCommand { get; }
        public ICommand ClosePageCommand { get; }

        public AddFieldWindowVM()
        {
            AddFieldCommand = new RelayCommand<object>(_ => AddFieldToDatabase());
            ClosePageCommand = new RelayCommand<object>(_ => ClosePage());
        }

        private void ClosePage()
        {
            Application.Current.Windows[Application.Current.Windows.Count - 1]?.Close();
        }
        private void AddFieldToDatabase()
        {
            if (string.IsNullOrEmpty(FieldName) || string.IsNullOrEmpty(Price) ||
                (!IsFootballSelected && !IsBasketballSelected && !IsTennisSelected))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Determinăm tipul terenului
            string fieldType = IsFootballSelected ? "Fotbal" :
                               IsBasketballSelected ? "Baschet" :
                               "Tenis";

            if (!int.TryParse(Price, out int priceValue))
            {
                MessageBox.Show("Invalid price format.");
                return;
            }

            try
            {
                // Apelăm metoda din Model
                AddFieldWindowModel.AddField(FieldName, Description, priceValue, fieldType);

                MessageBox.Show("Field added successfully!");

                // Închidem fereastra curentă
                Application.Current.Windows[Application.Current.Windows.Count - 1]?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
