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
using System.IO;

namespace chaski_tours_desk.Componentes
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : UserControl
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            comprobarLogin();
        }

        private void comprobarLogin()
        {
            if (!File.Exists("Cookie/logueao.txt"))
            {
                new LogSignWindow().Show();
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("Ya tiene una sesion iniciada");
                //Carrito de reserva .show();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists("Cookie/logueao.txt"))
                {
                    File.Delete("Cookie/logueao.txt");
                    MessageBox.Show("Se cerro la sesion");
                }
                else MessageBox.Show("No tiene una sesion iniciada");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
