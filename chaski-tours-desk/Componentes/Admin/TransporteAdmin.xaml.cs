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
    /// Lógica de interacción para TransporteAdmin.xaml
    /// </summary>
    public partial class TransporteAdmin : Window
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/transporte";
        public Transporte trans;
        public TransporteAdmin(Transporte x)
        {
            InitializeComponent();
            trans = x;
            configurardatos();
        }

        private void configurardatos()
        {
            
                txt_id.Text = trans.id_vehiculo.ToString();
                txt_matricula.Text = trans.matricula.ToString();
                txt_marca.Text = trans.marca.ToString();
                txt_modelo.Text = trans.modelo.ToString();
                txt_capacidad.Text = trans.capacidad.ToString();
                txt_anio.Text = trans.anio.ToString();
                txt_disponible.Text = trans.disponible.ToString();
                txt_activo.Text = trans.activo.ToString();
            
            
        }


        private async void BorrarTransporte()
        {
            await EliminarTransporteAsync();
        }
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        { 
            BorrarTransporte();
        }

        public async Task EliminarTransporteAsync()
        {
            int id = int.Parse(txt_id.Text);
            HttpResponseMessage response = await cliente.DeleteAsync($"{URL}/{id}");
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Transporte borrado correctamente");
                Close();
            }
            else
            {
                MessageBox.Show("Error al borrar el transporte");
            }
        }

        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            if(validar())
            {
                mandarTransporte();
            }
        }

        private bool validar()
        {
            if (txt_matricula.Text == "" ||
            txt_marca.Text == "" ||
            txt_modelo.Text == "" ||
            txt_capacidad.Text == "" ||
            txt_anio.Text == "" ||
            txt_disponible.Text == "")
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
            if (txt_matricula.Text.Length > 8)
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
        public async Task ActualizarTransporteAsync()
        {
            Transporte transporte = new Transporte
            {
                id_vehiculo = int.Parse(txt_id.Text),
                matricula = txt_matricula.Text,
                marca = txt_marca.Text,
                modelo = txt_modelo.Text,
                capacidad = int.Parse(txt_capacidad.Text),
                anio = txt_anio.Text,
                disponible = int.Parse(txt_disponible.Text),
                activo = int.Parse(txt_activo.Text)
            };
            string json = JsonSerializer.Serialize(transporte);
            MessageBox.Show(json);

            HttpResponseMessage response = await cliente.PutAsJsonAsync($"{URL}/{transporte.id_vehiculo}", transporte);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Transporte actualizado correctamente");
                Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar el transporte");
            }

        }
        private async void mandarTransporte()
        {
            await ActualizarTransporteAsync();
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
