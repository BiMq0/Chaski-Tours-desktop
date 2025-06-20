using chaski_tours_desk.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
    /// Lógica de interacción para ReservasAdmin.xaml
    /// </summary>
    public partial class ReservasAdmin : Window
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/reservas";
        public Reserva res;
        public ReservasAdmin(Reserva x)
        {
            
            InitializeComponent();
            res = x;
            configurardatos();
        }

        private void configurardatos()
        {

            txt_id.Text = res.id_reserva.ToString();
            txt_codvisitante.Text = res.cod_visitante.ToString();
            txt_idalojamiento.Text = res.id_alojamiento.ToString();
            txt_idsalida.Text = res.id_salida.ToString();
            txt_cantidad.Text = res.cantidad_personas.ToString();
            txt_costototal.Text = res.costo_total_reserva.ToString("F2"); 
            txt_estado.Text = res.estado.ToString();
            txt_fechadereservacion.Text = res.fecha_reservacion.ToString(); 


        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            BorrarReserva();
        }


        private async void BorrarReserva()
        {
            await EliminarReserva();
        }
        

        public async Task EliminarReserva()
        {
            int id = int.Parse(txt_id.Text);
            HttpResponseMessage response = await cliente.DeleteAsync($"{URL}/{id}");
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Reserva borrada correctamente");
                Close();
            }
            else
            {
                MessageBox.Show("Error al borrar la reserva");
            }
        }

        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            if (validar())
            {
                mandarReserva();
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
                MessageBox.Show("CANTIDAD, COSTO deben ser números válidos.");
                return false;
            }
            return true;
        }
        private async void mandarReserva()
        {
            await ActualizarReserva();
        }
        public async Task ActualizarReserva()
        {
            Reserva res = new Reserva
            {
                id_reserva = int.Parse( txt_id.Text) ,
                cod_visitante = txt_codvisitante.Text ,
                id_alojamiento = int.Parse(txt_idalojamiento.Text),
                id_salida = int.Parse(txt_idsalida.Text),
                cantidad_personas = int.Parse( txt_cantidad.Text),
                costo_total_reserva =double.Parse( txt_costototal.Text),
                estado = txt_estado.Text,
                fecha_reservacion = txt_fechadereservacion.Text 
            };
            string json = JsonSerializer.Serialize(res);
            MessageBox.Show(json);

            HttpResponseMessage response = await cliente.PutAsJsonAsync($"{URL}/{res.id_reserva}", res);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Reserva actualizada correctamente");
                Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar la reserva");
            }

        }
        
    }
}
