using Fieldify_App.Data;
using Fieldify_App.ViewModels;
using Fieldify_App.Views.UserControls;
using MaterialDesignThemes.Wpf;
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


namespace Fieldify_App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow(User loggedInUser)
        {
            InitializeComponent();

            _viewModel = new MainViewModel(loggedInUser);
        }

        private MainViewModel _viewModel;

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void SetTheme(bool isDark)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            if (isDark)
            {
                theme.SetBaseTheme(Theme.Dark);
            }
            else
            {
                theme.SetBaseTheme(Theme.Light);
            }
            paletteHelper.SetTheme(theme);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new MyProfilePage(_viewModel.CurrentUser);
        }

        private void MenuButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new BookingPage(_viewModel.CurrentUser);
        }

        private void MenuButton_Click_2(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new MyBookingsPage(_viewModel.CurrentUser);
        }

        private void MenuButton_Click_3(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show(); 

            Application.Current.Windows[0]?.Close();
        }

        private void home_btn_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new HomePage();
        }
    }
}