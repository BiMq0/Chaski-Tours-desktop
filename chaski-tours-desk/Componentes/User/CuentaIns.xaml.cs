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
    /// Lógica de interacción para CuentaIns.xaml
    /// </summary>
    public partial class CuentaIns : Window
    {
        private HttpClient cliente = new HttpClient();
        private string URL_Institucion = "http://localhost:8000/api/visitantes/instituciones/cod/";
        private string URL_Reserva = "http://localhost:8000/api/reservas/cod/";
        private string URL_Institucion_Update = "http://localhost:8000/api/visitantes/instituciones/";
        private Institucion institucionActual;
        public CuentaIns(string codVisitante)
        {
            InitializeComponent();
            cargarPerfil(codVisitante);
            cargarReservas(codVisitante);
        }
        private void cargarPerfil(string cod)
        {
            try
            {
                institucionActual = cliente.GetFromJsonAsync<Institucion>(URL_Institucion + cod).Result;
                if (institucionActual != null)
                {
                    
                    txtNombre.Text = institucionActual.nombre;
                    txtCorreo.Text = institucionActual.correo_electronico;
                    txtNacionalidad.Text = institucionActual.nacionalidad;
                    txtTelefono.Text = institucionActual.telefono;

                    var partesNombre = institucionActual.nombre_represent?.Split(' ') ?? new string[0];
                    txtNombreRepresentante.Text = partesNombre.Length > 0 ? partesNombre[0] : "";
                    txtCorreoRep.Text = institucionActual.correo_electronico_represent;
                    txtTelefonoRep.Text = institucionActual.telefono_represent;
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
                    listaReservas.Visibility = Visibility.Visible;
                    txtNoReservas.Visibility = Visibility.Collapsed;
                }
                else
                {
                    listaReservas.ItemsSource = null;
                    listaReservas.Visibility = Visibility.Collapsed;
                    txtNoReservas.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("JSON"))
                {
                    MessageBox.Show("Aún no ha realizado reservas.");
                    listaReservas.ItemsSource = null;
                }
                else
                {
                    MessageBox.Show("Error al cargar el historial de reservas: " + ex.Message);
                }
            }
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.IsReadOnly = false;
            txtCorreo.IsReadOnly = false;
            txtTelefono.IsReadOnly = false;

            txtNombreRepresentante.IsReadOnly = false;
            txtCorreoRep.IsReadOnly = false;
            txtTelefonoRep.IsReadOnly = false;

            panelApellidosRepresentante.Visibility = Visibility.Visible;
            panelContrasenias.Visibility = Visibility.Visible;

            if (institucionActual != null)
            {
                txtApellidoPaternoRepresentante.Text = institucionActual.ap_pat_represent ?? "";
            }

            txtNombre.BorderThickness = new Thickness(1);
            txtCorreo.BorderThickness = new Thickness(1);
            txtTelefono.BorderThickness = new Thickness(1);
            txtNombreRepresentante.BorderThickness = new Thickness(1);
            txtCorreoRep.BorderThickness = new Thickness(1);
            txtTelefonoRep.BorderThickness = new Thickness(1);
            txtApellidoPaternoRepresentante.BorderThickness = new Thickness(1);

            btnEditar.Visibility = Visibility.Collapsed;
            btnGuardar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (institucionActual != null)
            {
                cargarPerfil(institucionActual.cod_visitante);
            }

            panelApellidosRepresentante.Visibility = Visibility.Collapsed;
            panelContrasenias.Visibility = Visibility.Collapsed;

            txtNombre.IsReadOnly = true;
            txtCorreo.IsReadOnly = true;
            txtTelefono.IsReadOnly = true;
            txtNombreRepresentante.IsReadOnly = true;
            txtCorreoRep.IsReadOnly = true;
            txtTelefonoRep.IsReadOnly = true;

            txtNombre.BorderThickness = new Thickness(0);
            txtCorreo.BorderThickness = new Thickness(0);
            txtTelefono.BorderThickness = new Thickness(0);
            txtNombreRepresentante.BorderThickness = new Thickness(0);
            txtCorreoRep.BorderThickness = new Thickness(0);
            txtTelefonoRep.BorderThickness = new Thickness(0);

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
                var updatedInstitucion = new Institucion
                {
                    cod_visitante = institucionActual.cod_visitante,
                    nombre = txtNombre.Text,
                    correo_electronico = txtCorreo.Text,
                    telefono = txtTelefono.Text,
                    nacionalidad = institucionActual.nacionalidad, 
                    nombre_represent = txtNombreRepresentante.Text,
                    ap_pat_represent = txtApellidoPaternoRepresentante.Text,
                    correo_electronico_represent = txtCorreoRep.Text,
                    telefono_represent = txtTelefonoRep.Text,
                    contrasenia = string.IsNullOrEmpty(txtPassword.Password) ?
                                institucionActual.contrasenia :
                                txtPassword.Password
                };

                var response = await cliente.PutAsJsonAsync(
                    URL_Institucion_Update + institucionActual.cod_visitante,
                    updatedInstitucion);

                if (response.IsSuccessStatusCode)
                {
                    institucionActual = updatedInstitucion;

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
