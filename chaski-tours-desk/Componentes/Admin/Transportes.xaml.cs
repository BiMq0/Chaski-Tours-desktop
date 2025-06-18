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
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using chaski_tours_desk.Modelos;

namespace chaski_tours_desk.Componentes.Admin
{
    /// <summary>
    /// Lógica de interacción para Transportes.xaml
    /// </summary>
    public partial class Transportes : UserControl
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/transporte";
        public Transportes()
        {
            InitializeComponent();
        }

        private async Task obtenerTransportes()
        {
            var transportes = await cliente.GetFromJsonAsync<List<Transporte>>(URL);

            tbl_Transportes.ItemsSource = transportes;
        }

        private async void verTransportes()
        {
            await obtenerTransportes();
        }

        private void verDatos()
        {
            if (Window.GetWindow(this).Visibility == Visibility.Visible)
            {
                verTransportes();
            }
        }

        private void Transporte_Loaded(object sender, RoutedEventArgs e)
        {
            verDatos();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
