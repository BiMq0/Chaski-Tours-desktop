using chaski_tours_desk.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http.Json;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace chaski_tours_desk.Componentes.User
{
    /// <summary>
    /// Lógica de interacción para Cuenta.xaml
    /// </summary>
    public partial class Cuenta : Window
    {
        HttpClient cliente = new HttpClient();
        private string URL_Turista = "http://localhost:8000/api/visitantes/turistas/cod/";
        private string URL_Reserva = "http://localhost:8000/api/reservas/cod/";
        private string URL_Turista_Update = "http://localhost:8000/api/visitantes/turistas/";
        private string URL_Tour = "http://localhost:8000/api/tour/";


        public Cuenta(string codVisitante)
        {
            InitializeComponent();
            cargarPerfil(codVisitante);
            cargarReservas(codVisitante);
        }
        private void cargarPerfil(string cod)
        {
            try
            {
                turistaActual = cliente.GetFromJsonAsync<Turista>(URL_Turista + cod).Result;
                if (turistaActual != null)
                {
                    txtNombre.Text = $"{turistaActual.nombre} {turistaActual.ap_pat} {turistaActual.ap_mat}".Trim();
                    txtCorreo.Text = turistaActual.correo_electronico;
                    txtDocumento.Text = turistaActual.documento;
                    txtTelefono.Text = turistaActual.telefono;

                    txtFechaNac.Text = turistaActual.fecha_nac;
                    dpFechaNac.Visibility = Visibility.Collapsed;
                    txtFechaNac.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el perfil: " + ex.Message);
            }
        }


        private void cargarReservas(string cod)
        {
            try
            {
                var reservas = cliente.GetFromJsonAsync<List<Reserva>>(URL_Reserva + cod).Result;

                if (reservas != null && reservas.Any())
                {
                    listaReservas.ItemsSource = reservas;
                }
                else
                {
                    listaReservas.ItemsSource = new List<Reserva> {
                    new Reserva { id_salida = 0, cantidad_personas = 0, costo_total_reserva = 0, estado = "Aún no ha realizado reservas." }
                };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de reservas: " + ex.Message);
            }
        }
        private Turista turistaActual;
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.IsReadOnly = false;
            txtCorreo.IsReadOnly = false;
            txtDocumento.IsReadOnly = false;
            txtTelefono.IsReadOnly = false;
            
            txtFechaNac.Visibility = Visibility.Collapsed;
            dpFechaNac.Visibility = Visibility.Visible;
            
            if (DateTime.TryParse(txtFechaNac.Text, out DateTime fecha))
            {
                dpFechaNac.SelectedDate = fecha;
            }

            txtNombre.BorderThickness = new Thickness(1);
            txtCorreo.BorderThickness = new Thickness(1);
            txtDocumento.BorderThickness = new Thickness(1);
            txtTelefono.BorderThickness = new Thickness(1);
            txtApPaterno.BorderThickness = new Thickness(1);
            txtApMaterno.BorderThickness = new Thickness(1);

            panelApellidos.Visibility = Visibility.Visible;
            panelContrasenias.Visibility = Visibility.Visible;
            txtApPaterno.IsReadOnly = false;
            txtApMaterno.IsReadOnly = false;
            txtApPaterno.BorderThickness = new Thickness(1);
            txtApMaterno.BorderThickness = new Thickness(1);

            var partesNombre = txtNombre.Text.Split(' ');
            txtNombre.Text = partesNombre.Length > 0 ? partesNombre[0] : "";
            
            if (turistaActual != null)
            {
                txtApPaterno.Text = turistaActual.ap_pat ?? "";
                txtApMaterno.Text = turistaActual.ap_mat ?? "";
            }

            btnEditar.Visibility = Visibility.Collapsed;
            btnGuardar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (turistaActual != null)
            {
                txtNombre.Text = $"{turistaActual.nombre} {turistaActual.ap_pat} {turistaActual.ap_mat}".Trim();
                txtCorreo.Text = turistaActual.correo_electronico;
                txtDocumento.Text = turistaActual.documento;
                txtTelefono.Text = turistaActual.telefono;
                txtFechaNac.Text = turistaActual.fecha_nac;
            }

            dpFechaNac.Visibility = Visibility.Collapsed;
            txtFechaNac.Visibility = Visibility.Visible;

            txtNombre.IsReadOnly = true;
            txtCorreo.IsReadOnly = true;
            txtDocumento.IsReadOnly = true;
            txtTelefono.IsReadOnly = true;
            txtApPaterno.IsReadOnly = true;
            txtApMaterno.IsReadOnly = true;

            txtApPaterno.BorderThickness = new Thickness(0);
            txtApMaterno.BorderThickness = new Thickness(0);

            txtNombre.BorderThickness = new Thickness(0);
            txtCorreo.BorderThickness = new Thickness(0);
            txtDocumento.BorderThickness = new Thickness(0);
            txtTelefono.BorderThickness = new Thickness(0);

            panelApellidos.Visibility = Visibility.Collapsed;
            panelContrasenias.Visibility = Visibility.Collapsed;

            btnEditar.Visibility = Visibility.Visible;
            btnGuardar.Visibility = Visibility.Collapsed;
            btnCancelar.Visibility = Visibility.Collapsed;
        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) &&
                txtPassword.Password != txtConfirmarPassword.Password)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            try
            {
                if (dpFechaNac.SelectedDate.HasValue)
                {
                    txtFechaNac.Text = dpFechaNac.SelectedDate.Value.ToString("yyyy-MM-dd");
                }

                var updatedTurista = new Turista
                {
                    cod_visitante = turistaActual.cod_visitante,
                    nombre = txtNombre.Text,
                    correo_electronico = txtCorreo.Text,
                    documento = txtDocumento.Text,
                    telefono = txtTelefono.Text,
                    fecha_nac = txtFechaNac.Text,
                    ap_pat = txtApPaterno.Text,
                    ap_mat = txtApMaterno.Text,
                    nacionalidad = turistaActual.nacionalidad,
                    contrasenia = string.IsNullOrEmpty(txtPassword.Password) ?
                                  turistaActual.contrasenia :
                                  txtPassword.Password
                };

                var response = await cliente.PutAsJsonAsync(URL_Turista_Update + MainWindow.codVisitanteActual,updatedTurista);

                if (response.IsSuccessStatusCode)
                {
                    turistaActual = updatedTurista;

                    btnCancelar_Click(null, null);

                    MessageBox.Show("Perfil actualizado correctamente.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error del servidor: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}");
            }
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
