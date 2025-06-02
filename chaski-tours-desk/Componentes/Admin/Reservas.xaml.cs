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
    /// Lógica de interacción para Reservas.xaml
    /// </summary>
    public partial class Reservas : UserControl
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/reservas";
        public Reservas()
        {
            InitializeComponent();
        }
        private async Task obtenerReserva()
        {
            var usuarios = await cliente.GetFromJsonAsync<List<Reserva>>(URL);

            tbl_Reserva.ItemsSource = usuarios;
        }

        private async void verReserva()
        {
            await obtenerReserva();
        }

        private void verDatos()
        {
            if (Window.GetWindow(this).Visibility == Visibility.Visible)
            {
                verReserva();
            }
        }

        private void Reserva_Loaded(object sender, RoutedEventArgs e)
        {
            verDatos();
        }

    }
}