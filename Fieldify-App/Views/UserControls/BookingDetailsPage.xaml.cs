using Fieldify_App.Data;
using Fieldify_App.ViewModels;
using System;
using System.Windows.Controls;


namespace Fieldify_App.Views.UserControls
{
    /// <summary>
    /// Interaction logic for BookingDetailsPage.xaml
    /// </summary>
    public partial class BookingDetailsPage : UserControl
    {
        private BookingDetailsPageVM _viewModel;

        public BookingDetailsPage()
        {
            InitializeComponent();
            MyDatePicker.DisplayDateStart = DateTime.Now.AddDays(1);
        }


        public BookingDetailsPage(Action navigateBack, Teren selectedField, User currentUser)
        {
            InitializeComponent();

            _viewModel = new BookingDetailsPageVM(navigateBack, selectedField, currentUser);
            DataContext = _viewModel;

            MyDatePicker.DisplayDateStart = DateTime.Now.AddDays(1);

        }

    }
}
