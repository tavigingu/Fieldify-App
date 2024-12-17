using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fieldify_App.Views.UserControls
{
    /// <summary>
    /// Interaction logic for AddFieldWindow.xaml
    /// </summary>
    public partial class AddFieldWindow : Window
    {
        public AddFieldWindow()
        {
            InitializeComponent();
        }

        private void priceBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string text)
        {
            // Verifică dacă textul este format doar din cifre
            return Regex.IsMatch(text, @"^\d+$");
        }
    }
}
