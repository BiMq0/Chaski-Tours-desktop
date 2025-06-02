using chaski_tours_desk.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace chaski_tours_desk.Componentes.Admin
{
    /// <summary>
    /// Lógica de interacción para Tours.xaml
    /// </summary>
    public partial class Tours : UserControl
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/tour/";
        public Tours()
        {
            InitializeComponent();
        }

        private async Task obtenerSitios()
        {
            var sitios = await cliente.GetFromJsonAsync<List<Tour>>(URL);


            tbl_Tours.ItemsSource = sitios;
        }

        private async void verTours()
        {
            await obtenerSitios();
        }

        private void verDatos()
        {
            if (Window.GetWindow(this).Visibility == Visibility.Visible)
            {
                verTours();
            }
        }

        private void Tours_Loaded(object sender, RoutedEventArgs e)
        {
            verDatos();
        }
    }
}
