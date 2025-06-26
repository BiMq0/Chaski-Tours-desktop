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

namespace chaski_tours_desk.Componentes.User
{
    /// <summary>
    /// Lógica de interacción para navbar.xaml
    /// </summary>
    public partial class navbar : UserControl
    {
        public navbar()
        {
            InitializeComponent();
        }

        private void Grid_InicioEvento(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("si funca el boton inicio");
        }
        private void Grid_ToursEvento(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("si funca el boton tour");
        }
        private void Grid_CiudadesEvento(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("si funca el boton ciudades");
        }
        private void Grid_SitioEvento(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("si funca el boton sitio");
        }
        private void Grid_CategoriaEvento(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("si funca el boton categoria");
        }

    }
    
}
