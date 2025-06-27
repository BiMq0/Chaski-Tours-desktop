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
using System.Windows.Shapes;
using System.Net.Http.Json;

namespace chaski_tours_desk.Componentes.User
{
    /// <summary>
    /// Lógica de interacción para ReservasUsuario.xaml
    /// </summary>
    public partial class ReservasUsuario : Window
    {
        private static HttpClient cliente = new HttpClient();
        private string URL_reserva = "http://localhost:8000/api/reservas/";
        private string URL_salida = "http://localhost:8000/api/calendario/";
        private string URL_tour = "http://localhost:8000/api/tour/";


        public ReservasUsuario()
        {
            InitializeComponent();
        }
        public ReservasUsuario(int idReserva) : this()
        {
            _ = cargarReserva(idReserva);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public async Task cargarReserva(int idReserva)
        {
            try
            {
                var reserva = await cliente.GetFromJsonAsync<Reserva>(URL_reserva + idReserva);
                if (reserva == null)
                {
                    MessageBox.Show("No se encontró la reserva.");
                    return;
                }

                var salida = await cliente.GetFromJsonAsync<Salida>(URL_salida + reserva.id_salida);
                if (salida == null)
                {
                    MessageBox.Show("No se encontró la salida asociada.");
                    return;
                }


                var tour = await cliente.GetFromJsonAsync<Tour>(URL_tour + salida.id_tour);
                if (tour == null)
                {
                    MessageBox.Show("No se encontró el tour asociado.");
                    return;
                }

                txtNombreTour.Text = tour.nombre_tour ?? "N/A";
                txtCantidadPersonas.Text = reserva.cantidad_personas.ToString();
                txtCostoTotal.Text = reserva.costo_total_reserva.ToString("C2");
                txtEstado.Text = reserva.estado ?? "N/A";
                txtFechaSalida.Text = salida.fecha_salida ?? "N/A";
                txtFechaRegreso.Text = salida.fecha_regreso ?? "N/A";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la reserva: " + ex.Message);
            }
        }
    }
}
