using chaski_tours_desk.Componentes.User.ListaDE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Lógica de interacción para Landing.xaml
    /// </summary>
    public partial class Landing : UserControl
    {
        HttpClient client = new HttpClient();
        LDE lstDECategorias = new LDE();
        LDE lstDEDepartamentos = new LDE();
        public Landing()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cargarListasDE();
        }

        private void cargarListasDE()
        {
            cargarListaDECategorias();
            cargarListaDEDepartamentos();
        }

        private void cargarListaDECategorias()
        {
            
        }

        private void cargarListaDEDepartamentos()
        {
            
        }

        private void btnDerechaDep_Click(object sender, RoutedEventArgs e)
        {
            categoryScrollViewer.ScrollToHorizontalOffset(
            Math.Max(0, categoryScrollViewer.HorizontalOffset - 140));
        }

        private void btnIzquierdaDep_Click(object sender, RoutedEventArgs e)
        {
            categoryScrollViewer.ScrollToHorizontalOffset(
            Math.Max(0, categoryScrollViewer.HorizontalOffset - 140));
        }

        private void btnIzquierdaCat_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnDerechaCat_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
