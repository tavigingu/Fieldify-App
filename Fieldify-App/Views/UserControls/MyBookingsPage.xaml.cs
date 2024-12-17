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
    /// Interaction logic for MyBookingsPage.xaml
    /// </summary>
    public partial class MyBookingsPage : UserControl
    {
        private MyBookingsPageVM _viewModel;


        public MyBookingsPage(User current_user)
        {
            InitializeComponent();
            _viewModel = new MyBookingsPageVM(current_user);

            DataContext = _viewModel;
        }
    }
}
