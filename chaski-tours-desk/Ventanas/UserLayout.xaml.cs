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

namespace chaski_tours_desk.Ventanas
{
    /// <summary>
    /// Lógica de interacción para UserLayout.xaml
    /// </summary>
    public partial class UserLayout : UserControl
    {
        public UserLayout()
        {
            InitializeComponent();
        }

        private void btnReservar_Click(object sender, RoutedEventArgs e)
        {
            abrirAdmin.Invoke();
        }
        public event Action abrirAdmin;

        private void VerCategorias() { 
            landing.Visibility = Visibility.Collapsed;
            vistaDepartamentos.Visibility = Visibility.Collapsed;
            vistaCategorias.Visibility = Visibility.Visible;
        }

        private void VerDepartamentos()
        {
            landing.Visibility = Visibility.Collapsed;
            vistaDepartamentos.Visibility = Visibility.Visible;
            vistaCategorias.Visibility = Visibility.Collapsed;
        }

        private void VerLanding()
        {
            landing.Visibility = Visibility.Visible;
            vistaCategorias.Visibility = Visibility.Collapsed;
            vistaDepartamentos.Visibility = Visibility.Collapsed;
        }

        //aun no implementados

        private void VerSitios()
        {
            MessageBox.Show("Ver Sitios no implementado aún.");
        }

        private void VerTours()
        {
            MessageBox.Show("Ver Tours no implementado aún.");
        }

        private void VerSitios(string nombre)
        {
            MessageBox.Show($"Ver Sitios de {nombre} no implementado aún.");
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            landing.AbrirDepartamentos += VerDepartamentos;
            landing.AbrirCategorias += VerCategorias;
            vistaCategorias.volverLanding += VerLanding;
            vistaDepartamentos.volverLanding += VerLanding;

            //Pruebas no implementadas
            landing.AbrirSitios += VerSitios;
            landing.AbrirTours += VerTours;

            vistaCategorias.VerSitios += VerSitios;
            vistaDepartamentos.VerSitios += VerSitios;
        }

    }
}
