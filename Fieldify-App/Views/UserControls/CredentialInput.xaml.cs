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
    /// Interaction logic for CredentialInput.xaml
    /// </summary>
    public partial class CredentialInput : UserControl
    {
        public CredentialInput()
        {
            InitializeComponent();
        }

       public static readonly DependencyProperty TextProperty =
       DependencyProperty.Register("Text", typeof(string), typeof(CredentialInput),
       new FrameworkPropertyMetadata(string.Empty));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty HintProperty =
             DependencyProperty.Register("HintLabel", typeof(string), typeof(CredentialInput), new PropertyMetadata(string.Empty));

        public string HintLabel
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }


    }
}
