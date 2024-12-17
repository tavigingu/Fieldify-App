using Fieldify_App.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fieldify_App.ViewModels
{
    internal class BookingConfirmationPageVM : ViewModelBase
    {
        public string FieldName { get; }
        public string BookingDate { get; }
        public string BookingHour { get; }

        public ICommand BackToMainPageCommand { get; }

        public BookingConfirmationPageVM() { }

        public BookingConfirmationPageVM(Action navigateBack, string fieldName, DateTime bookingDate, string bookingHour)
        {
            FieldName = fieldName;
            BookingDate = bookingDate.ToShortDateString();
            BookingHour = bookingHour;

            BackToMainPageCommand = new RelayCommand<object>(param => navigateBack());
        }
    }

}
