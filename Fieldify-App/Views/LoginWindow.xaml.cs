using Fieldify_App.Data;
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
using System.Windows.Shapes;

namespace Fieldify_App.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
       
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void themeToggle_Click(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
                UpdateDynamicPrimaryBrush(BaseTheme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
                UpdateDynamicPrimaryBrush(BaseTheme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void UpdateDynamicPrimaryBrush(BaseTheme baseTheme)
        {
            // Access resources from App.xaml and update the DynamicPrimaryBrush
            if (baseTheme == BaseTheme.Dark)
            {
                // Application.Current.Resources["DynamicPrimaryBrush"] = Application.Current.Resources["DarkThemeColor"];

                var darkThemeBrush = (Color)Application.Current.Resources["DarkThemeColor"];
                var secDarkThemeBrush = (Color)Application.Current.Resources["DarkSecThemeColor"];
                var dynamicPrimaryBrush = (SolidColorBrush)Application.Current.Resources["DynamicBrush"];
                var SecDynamicPrimaryBrush = (SolidColorBrush)Application.Current.Resources["SecDynamicBrush"];

                // Actualizăm culoarea brush-ului dinamic
                dynamicPrimaryBrush.Color = darkThemeBrush;
                SecDynamicPrimaryBrush.Color = secDarkThemeBrush;
            }
            else
            {
                // Application.Current.Resources["DynamicPrimaryBrush"] = Application.Current.Resources["LightThemeBrush"];

                var lightThemeBrush = (Color)Application.Current.Resources["LightThemeColor"];
                var secLightThemeBrush = (Color)Application.Current.Resources["LightSecThemeColor"];
                var dynamicPrimaryBrush = (SolidColorBrush)Application.Current.Resources["DynamicBrush"];
                var SecDynamicPrimaryBrush = (SolidColorBrush)Application.Current.Resources["SecDynamicBrush"];

                // Actualizăm culoarea brush-ului dinamic
                dynamicPrimaryBrush.Color = lightThemeBrush;
                SecDynamicPrimaryBrush.Color = secLightThemeBrush;

            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            SignupWindow signupWindow = new SignupWindow();

            // Deschide fereastra SignupWindow
            signupWindow.Show();

            // Închide fereastra curentă (LoginWindow)
            this.Close();
        }
    }
}
