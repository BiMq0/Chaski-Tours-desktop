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
using System.IO;

namespace chaski_tours_desk
{
    /// <summary>
    /// Lógica de interacción para LogSignWindow.xaml
    /// </summary>
    public partial class LogSignWindow : Window
    {
        public LogSignWindow()
        {
            InitializeComponent();
            verLogSignUp();
        }
        private void verLogSignUp()
        {
            Login.btnSignup.Click += BtnSignup_Click;
            Signup.btnLogin.Click += BtnLogin_Click;

            void BtnSignup_Click(object sender, RoutedEventArgs e)
            {
                Signup.Visibility = Visibility.Visible;
                Login.Visibility = Visibility.Collapsed;
            }

            void BtnLogin_Click(object sender, RoutedEventArgs e)
            {
                Signup.Visibility = Visibility.Collapsed;
                Login.Visibility = Visibility.Visible;
            }
        }
    }
}
