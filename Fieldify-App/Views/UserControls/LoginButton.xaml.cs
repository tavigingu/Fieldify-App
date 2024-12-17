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
    /// Interaction logic for LoginButton.xaml
    /// </summary>
    public partial class LoginButton : UserControl
    {
        public LoginButton()
        {
            InitializeComponent();
        }

        //Definirea evenimentului Click
        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LoginButton));

        //Expunerea evenimentului Click
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        // Metodă pentru a lansa evenimentul Click
        private void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(LoginButton.ClickEvent);
            RaiseEvent(newEventArgs);
        }

        // DependencyProperty pentru Command
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(LoginButton), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // DependencyProperty pentru CommandParameter
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(LoginButton), new PropertyMetadata(null));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }


        public static readonly DependencyProperty ButtonTextProperty =
          DependencyProperty.Register("ButtonText", typeof(string), typeof(LoginButton), new PropertyMetadata("LOG IN"));

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public static readonly DependencyProperty ButtonColorProperty =
            DependencyProperty.Register("ButtonColor", typeof(string), typeof(LoginButton), new PropertyMetadata(null));

        public string ButtonColor
        {
            get { return (string)GetValue(ButtonColorProperty); }
            set { SetValue(ButtonColorProperty, value); }
        }

        public static readonly DependencyProperty ButtonTextColorProperty =
            DependencyProperty.Register("ButtonTextColor", typeof(string), typeof(LoginButton), new PropertyMetadata(null));

        public string ButtonTextColor
        {
            get { return (string)GetValue(ButtonTextColorProperty); }
            set { SetValue(ButtonTextColorProperty, value); }
        }

        public static readonly DependencyProperty ButtonStyleProperty =
        DependencyProperty.Register("ButtonStyle", typeof(Style), typeof(LoginButton), new PropertyMetadata(null));

        public Style ButtonStyle
        {
            get { return (Style)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

    }
}

