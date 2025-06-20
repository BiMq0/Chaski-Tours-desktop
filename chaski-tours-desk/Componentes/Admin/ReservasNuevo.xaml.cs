using chaski_tours_desk.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace chaski_tours_desk.Componentes.Admin
{
    /// <summary>
    /// Lógica de interacción para ReservasNuevo.xaml
    /// </summary>
    public partial class ReservasNuevo : Window
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/reservas/crear";
        public ReservasNuevo()
        {
            InitializeComponent();
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Enviar_Click(object sender, RoutedEventArgs e)
        {
            if (validar())
            {
                mandarReserva();
            }

        }
        
        private async void mandarReserva()
        {
            await crearReserva();
        }
        public async Task crearReserva()
        {

            var res = new Reserva
            {
                id_reserva = 0,
                cod_visitante = txt_codvisitante.Text,
                id_alojamiento = int.Parse(txt_idalojamiento.Text),
                id_salida = int.Parse(txt_idsalida.Text),
                cantidad_personas = int.Parse(txt_cantidad.Text),
                costo_total_reserva = double.Parse(txt_costototal.Text),
                estado = txt_estado.Text,
                fecha_reservacion = txt_fechadereservacion.Text
            };
            string json = JsonSerializer.Serialize(res);
            MessageBox.Show(json);
            HttpResponseMessage response = await cliente.PostAsJsonAsync(URL, res);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Reserva creada correctamente");
                Close();
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Error al crear la reserva");
            }

        }
        private bool validar()
        {
            if (txt_cantidad.Text == "" ||
            txt_costototal.Text == "" ||
            txt_estado.Text == "" ||
            txt_fechadereservacion.Text == "")
            {
                MessageBox.Show("Por favor, complete todos los campos");
                return false;
            }
            if (!int.TryParse(txt_cantidad.Text, out int cantidad) ||
                 !double.TryParse(txt_costototal.Text, out double costo))
            {
                MessageBox.Show("CANTIDAD, COSTO  deben ser números válidos.");
                return false;
            }
            return true;
        }
    }
}
