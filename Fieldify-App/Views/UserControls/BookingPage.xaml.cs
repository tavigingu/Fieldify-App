using Fieldify_App.Data;
using Fieldify_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fieldify_App.Views.UserControls
{
    /// <summary>
    /// Interaction logic for BookingPage.xaml
    /// </summary>
    /// 


    public partial class BookingPage : UserControl
    {
        private BookingPageVM _viewModel;

        public BookingPage()
        {
            InitializeComponent();
        }

        public BookingPage(User current_user)
        {
            InitializeComponent();
            _viewModel = new BookingPageVM(current_user);

            DataContext = _viewModel;
        }
    }
}
