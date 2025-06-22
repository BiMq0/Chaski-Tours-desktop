using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using chaski_tours_desk.Modelos;
using System.Net.Http.Json;

namespace chaski_tours_desk.Componentes.Admin
{
    public partial class FormularioAlojamiento : Window
    {
        private readonly HttpClient httpClient = new HttpClient();

        public FormularioAlojamiento()
        {
            InitializeComponent();
            httpClient.BaseAddress = new Uri("http://localhost:8000/api/");
        }

        private async void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            // Validar campos básicos
            if (string.IsNullOrWhiteSpace(txtNombreAlojamiento.Text) ||
                string.IsNullOrWhiteSpace(txtNroEstrellas.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(txtNroEstrellas.Text, out double estrellas) || estrellas < 0)
            {
                MessageBox.Show("Ingrese un número válido de estrellas.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Definir tipos de habitaciones con su base numérica y capacidad fija
            var tiposHabitaciones = new List<(string nombre, int cantidad, int baseNro, int capacidad)>
            {
                ("Individual", chkIndividual.IsChecked == true ? ObtenerCantidad(txtCantidadIndividual) : 0, 100, 1),
                ("Doble", chkDoble.IsChecked == true ? ObtenerCantidad(txtCantidadDoble) : 0, 200, 2),
                ("Suite", chkSuite.IsChecked == true ? ObtenerCantidad(txtCantidadSuite) : 0, 300, 4),
                ("Familiar", chkFamiliar.IsChecked == true ? ObtenerCantidad(txtCantidadFamiliar) : 0, 400, 6)
            };

            var habitacionesSeleccionadas = tiposHabitaciones.Where(t => t.cantidad > 0).ToList();

            if (!habitacionesSeleccionadas.Any())
            {
                MessageBox.Show("Seleccione al menos un tipo de habitación con cantidad válida.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var alojamiento = new Alojamiento
            {
                nombre_aloj = txtNombreAlojamiento.Text,
                nro_estrellas = estrellas,
                nro_habitaciones = habitacionesSeleccionadas.Sum(t => t.cantidad),
                Activo = 1
            };

            try
            {
                // Registrar alojamiento
                var response = await httpClient.PostAsJsonAsync("alojamientos", alojamiento);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al registrar alojamiento: {error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();
                dynamic alojCreado = JsonConvert.DeserializeObject(json);
                int idAlojamiento = alojCreado.alojamiento.id_alojamiento;

                // Registrar habitaciones con la lógica del nro_habitacion
                foreach (var tipo in habitacionesSeleccionadas)
                {
                    for (int i = 0; i < tipo.cantidad; i++)
                    {
                        var habitacion = new Habitacion
                        {
                            // nro_habitacion se genera con trigger en BD, aquí puedes enviar 0 o no enviar ese campo para evitar conflicto
                            nro_habitacion = 0,
                            id_alojamiento = idAlojamiento,
                            tipo_habitacion = tipo.nombre,
                            capacidad = tipo.capacidad,
                            disponible = true
                        };

                        // Crear habitación
                        var resHab = await httpClient.PostAsJsonAsync("habitaciones", habitacion);
                        if (!resHab.IsSuccessStatusCode)
                        {
                            var errJson = await resHab.Content.ReadAsStringAsync();
                            dynamic errData = JsonConvert.DeserializeObject(errJson);
                            string mensaje = errData?.errores != null ? JsonConvert.SerializeObject(errData.errores) : errData?.error ?? "Error desconocido";
                            MessageBox.Show($"Error al registrar habitación: {mensaje}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                }

                MessageBox.Show("Alojamiento y habitaciones registrados exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int ObtenerCantidad(TextBox caja)
        {
            return int.TryParse(caja.Text, out int val) && val > 0 ? val : 0;
        }
    }

    // Clase para habitaciones (puedes moverla a tu carpeta Modelos si prefieres)
    public class Habitacion
    {
        public int nro_habitacion { get; set; }
        public int id_alojamiento { get; set; }
        public string tipo_habitacion { get; set; }
        public int capacidad { get; set; }
        public bool disponible { get; set; }
    }
}
