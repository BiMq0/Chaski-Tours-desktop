using chaski_tours_desk.Componentes.User.ListaDE;
using chaski_tours_desk.Modelos;
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
        private string URL1 = "http://localhost:8000/api/sitios/";
        private string URL2 = "http://localhost:8000/api/tour/";
        List<Sitio> sitios = new List<Sitio>();
        List<Tour> tours = new List<Tour>();
        public Landing()
        {
            InitializeComponent();
        }
        private async Task obtenerSitios()
        {
            sitios = await client.GetFromJsonAsync<List<Sitio>>(URL1);
        }

        public async void verSitios()
        {
            await obtenerSitios();
        }

        private async Task obtenerTours()
        {
            tours = await client.GetFromJsonAsync<List<Tour>>(URL2);
            cargarDatosaCarts();
        }

        public async void verTours()
        {
            await obtenerTours();
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            verSitios();
            verTours();
            cargarListasDE();

        }

        private void cargarDatosaCarts()
        {
            tbx_nombresitio1.Text = sitios[1].nombre;
            tbx_descripcionsitio1.Text = sitios[1].desc_conceptual_sitio;
            tbx_nombresitio2.Text = sitios[2].nombre;
            tbx_descripcionsitio2.Text = sitios[2].desc_conceptual_sitio;

            tbx_nombretour1.Text = tours[1].nombre_tour;
            tbx_descripciontour1.Text = tours[1].descripcion_tour;
            tbx_nombretour2.Text = tours[2].nombre_tour;
            tbx_descripciontour2.Text = tours[2].descripcion_tour;
        }


        //estos nenes se dedican a entregar acciones a las imagenes
        private void Grid1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        }
        private void Grid2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("imagen2");
        }
        private void Grid3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("imagen3");
        }
        private void Grid4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("imagen4");
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
