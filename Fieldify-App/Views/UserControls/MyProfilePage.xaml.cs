using Fieldify_App.Data;
using Fieldify_App.Models;
using Fieldify_App.ViewModels;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for MyProfilePage.xaml
    /// </summary>
    public partial class MyProfilePage : UserControl
    {
        private MyProfilePageVM _viewModel;

        public MyProfilePage(User current_user)
        {
            InitializeComponent();
            _viewModel = new MyProfilePageVM(current_user);
            DataContext = _viewModel;

            ITheme theme = paletteHelper.GetTheme();
            themeToggle.IsChecked = theme.GetBaseTheme() == BaseTheme.Dark;

        }

        private void descEditIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Ascundem TextBlock-ul și iconița de editare
            descriptionBlock.Visibility = Visibility.Collapsed;
            descEditIcon.Visibility = Visibility.Collapsed;


            // Creăm TextBox pentru a edita numele
            TextBox descTextBox = new TextBox
            {
                //cum ii pun bindingu aici??
                Text = descriptionBlock.Text,
                Margin = descriptionBlock.Margin,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 16,
                FontWeight = FontWeights.Bold

            };

            descTextBox.SetBinding(TextBox.TextProperty, new Binding("Description")
            {
                Source = _viewModel,                // Legăm direct ViewModel-ul
                Mode = BindingMode.TwoWay,          // Sincronizare bidirecțională
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged // Actualizare automată
            });


            // Când pierde focus, nu face nimic - schimbările vor fi salvate cu butonul „Save”
            descTextBox.LostFocus += (s, args) => { /* Intenționat lăsat gol */ };

            // Adăugăm TextBox-ul în UI și îl focalizăm
            StackPanel parentPanel = (StackPanel)descriptionBlock.Parent;
            parentPanel.Children.Insert(parentPanel.Children.IndexOf(descriptionBlock), descTextBox);
            descTextBox.Focus();

            // Afișăm butonul de „Save”
            saveDescButton.Visibility = Visibility.Visible;

        }

        private void emailEditIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            emailBlock.Visibility = Visibility.Collapsed;
            emailEditIcon.Visibility = Visibility.Collapsed;

            // Creăm TextBox pentru a edita numele
            TextBox emailTextBox = new TextBox
            {
                Text = emailBlock.Text,
                Margin = emailBlock.Margin,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 16,
                FontWeight = FontWeights.Bold
            };

            emailTextBox.SetBinding(TextBox.TextProperty, new Binding("Email")
            {
                Source = _viewModel,                // Legăm direct ViewModel-ul
                Mode = BindingMode.TwoWay,          // Sincronizare bidirecțională
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged // Actualizare automată
            });

            // Când pierde focus, nu face nimic - schimbările vor fi salvate cu butonul „Save”
            emailTextBox.LostFocus += (s, args) => { /* Intenționat lăsat gol */ };

            // Adăugăm TextBox-ul în UI și îl focalizăm
            StackPanel parentPanel = (StackPanel)emailBlock.Parent;
            parentPanel.Children.Insert(parentPanel.Children.IndexOf(emailBlock), emailTextBox);
            emailTextBox.Focus();

            // Afișăm butonul de „Save”
            saveEmailButton.Visibility = Visibility.Visible;

        }

        private void saveDescButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel parentPanel = (StackPanel)descriptionBlock.Parent;

            var textBox = parentPanel.Children.OfType<TextBox>().FirstOrDefault();
            if (textBox != null)
            {
                parentPanel.Children.Remove(textBox); // Scoate TextBox-ul
            }

            // Afișăm TextBlock și iconița de editare
            descriptionBlock.Visibility = Visibility.Visible;
            descEditIcon.Visibility = Visibility.Visible;
            saveDescButton.Visibility = Visibility.Collapsed;
        }

        private void saveEmailButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel parentPanel = (StackPanel)emailBlock.Parent;

            var textBox = parentPanel.Children.OfType<TextBox>().FirstOrDefault();
            if (textBox != null)
            {
                parentPanel.Children.Remove(textBox); // Scoate TextBox-ul
            }

            // Afișăm TextBlock și iconița de editare
            emailBlock.Visibility = Visibility.Visible;
            emailEditIcon.Visibility = Visibility.Visible;
            saveEmailButton.Visibility = Visibility.Collapsed;
        }


        private void phoneEditIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Ascundem TextBlock-ul și iconița de editare
            phoneBlock.Visibility = Visibility.Collapsed;
            phoneEditIcon.Visibility = Visibility.Collapsed;

            // Creăm TextBox pentru a edita numele
            TextBox phoneTextBox = new TextBox
            {
                Text = phoneBlock.Text,
                Margin = phoneBlock.Margin,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 16,
                FontWeight = FontWeights.Bold
            };

            phoneTextBox.SetBinding(TextBox.TextProperty, new Binding("Phone")
            {
                Source = _viewModel,                // Legăm direct ViewModel-ul
                Mode = BindingMode.TwoWay,          // Sincronizare bidirecțională
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged // Actualizare automată
            });

            // Când pierde focus, nu face nimic - schimbările vor fi salvate cu butonul „Save”
            phoneTextBox.LostFocus += (s, args) => { /* Intenționat lăsat gol */ };

            // Adăugăm TextBox-ul în UI și îl focalizăm
            StackPanel parentPanel = (StackPanel)phoneBlock.Parent;
            parentPanel.Children.Insert(parentPanel.Children.IndexOf(phoneBlock), phoneTextBox);
            phoneTextBox.Focus();

            // Afișăm butonul de „Save”
            savePhoneButton.Visibility = Visibility.Visible;
        }


        private void savePhoneButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel parentPanel = (StackPanel)phoneBlock.Parent;

            var textBox = parentPanel.Children.OfType<TextBox>().FirstOrDefault();
            if (textBox != null)
            {
                parentPanel.Children.Remove(textBox); // Scoate TextBox-ul
            }

            // Afișăm TextBlock și iconița de editare
            phoneBlock.Visibility = Visibility.Visible;
            phoneEditIcon.Visibility = Visibility.Visible;
            savePhoneButton.Visibility = Visibility.Collapsed;
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
    }
}