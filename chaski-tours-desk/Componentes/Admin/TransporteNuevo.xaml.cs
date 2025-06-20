using chaski_tours_desk.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para TransporteNuevo.xaml
    /// </summary>
    public partial class TransporteNuevo : Window
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/transporte/crear";
        public TransporteNuevo()
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
                mandarTransporte();
            }
            
        }
        private bool validar()
        {
            if (txt_matricula.Text == "" ||
            txt_marca.Text == "" ||
            txt_capacidad.Text == "" ||
            txt_anio.Text == "" ||
            txt_disponible.Text == "" )
            {
                MessageBox.Show("llene todos los campos");
                return false;
            }
            if (!int.TryParse(txt_capacidad.Text, out int capacidad) ||
                !int.TryParse(txt_anio.Text, out int anio) ||
                !int.TryParse(txt_disponible.Text, out int disponible))
            {
                MessageBox.Show("Capacidad, Año, Disponible deben ser números válidos.");
                return false;
            }
            int anioActual = DateTime.Now.Year;
            if (anio < 1950 || anio > anioActual + 1)
            {
                MessageBox.Show($"El año debe estar entre 1950 y {anioActual + 1}.");
                return false;
            }
            if (txt_matricula.Text.Length>8)
            {
                MessageBox.Show("La matricula debe de ser valida");
                return false;
            }
            if (txt_capacidad.Text.Length > 3)
            {
                MessageBox.Show("La capacidad debe de ser valida");
                return false;
            }
            if (txt_disponible.Text != "0" && txt_disponible.Text != "1")
            {
                MessageBox.Show("La disponibilidad debe de ser 1 o 0");
                return false;
            }
            

            return true;
        }
        private async void mandarTransporte()
        {
            await CrearTransporteAsync();
        }
        public async Task CrearTransporteAsync()
        {

            var transporte = new Transporte
            {
                id_vehiculo = 0,
                matricula = txt_matricula.Text,
                marca = txt_marca.Text,
                modelo = txt_modelo.Text,
                capacidad = int.Parse(txt_capacidad.Text),
                anio = txt_anio.Text,
                disponible = int.Parse(txt_disponible.Text),
                activo = 1
            };
            string json = JsonSerializer.Serialize(transporte);
            MessageBox.Show(json);
            HttpResponseMessage response = await cliente.PostAsJsonAsync(URL, transporte);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Transporte creado correctamente");
                Close();
            }
            else
            {
                string error = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Error al crear el transporte");
            }

        }

        
    }
}
