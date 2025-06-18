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
using System.Windows.Shapes;
using System.Net.Http;
using System.Net.Http.Json;
using chaski_tours_desk.Modelos;


namespace chaski_tours_desk.Componentes.Admin
{
    /// <summary>
    /// Lógica de interacción para FormularioCliente.xaml
    /// </summary>
    public partial class FormularioCliente : Window
    {
        private readonly HttpClient cliente = new HttpClient();
        private Visitante visitanteActual;
        private bool modoEdicion = false;

        public FormularioCliente()
        {
            InitializeComponent();
            CargarNacionalidades();
            btnEditar.Visibility = Visibility.Collapsed;
            btnEliminar.Visibility = Visibility.Collapsed;
            btnGuardar.Visibility = Visibility.Visible;

        }
        public FormularioCliente(Visitante visitante)
        {
            InitializeComponent();
            visitanteActual = visitante;

            
             _= CargarNacionalidades();
            CargarDatosVisitante();
            BloquearCampos();

            btnEditar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;
            btnGuardar.Visibility = Visibility.Collapsed;

        }
        private async Task CargarNacionalidadesYDatos()
        {
            await CargarNacionalidades();
            CargarDatosVisitante(); // Solo después de que cbNacionalidad... tenga Items
            BloquearCampos();

            btnEditar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;
            btnGuardar.Visibility = Visibility.Collapsed;
        }

        private void cbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tipo = (cbTipo.SelectedItem as ComboBoxItem)?.Content.ToString();
            panelTurista.Visibility = tipo == "Turista" ? Visibility.Visible : Visibility.Collapsed;
            panelInstitucion.Visibility = tipo == "Institucion" ? Visibility.Visible : Visibility.Collapsed;
        }

        private async void Registrar_Click(object sender, RoutedEventArgs e)
        {
            string tipo = (cbTipo.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (tipo == "Turista")
            {
                if (cbNacionalidadTurista.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, seleccione una nacionalidad para el turista.");
                    return;
                }

                var nacionalidad = cbNacionalidadTurista.SelectedItem.ToString();

                var turista = new
                {
                    correo_electronico = txtCorreo.Text.Trim(),
                    contrasenia = txtContrasenia.Password.Trim(),
                    nombre = txtNombre.Text.Trim(),
                    ap_pat = txtApPat.Text.Trim(),
                    ap_mat = txtApMat.Text.Trim(),
                    documento = txtDocumento.Text.Trim(),
                    fecha_nac = dpFechaNac.SelectedDate?.ToString("yyyy-MM-dd"),
                    nacionalidad = nacionalidad,
                    telefono = txtTelefono.Text.Trim()
                };

                var response = await cliente.PostAsJsonAsync("http://localhost:8000/api/visitantes/turistas/crear", turista);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Turista registrado correctamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al registrar turista.");
                }
            }
            else if (tipo == "Institucion")
            {
                if (cbNacionalidadInstitucion.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, seleccione una nacionalidad para la institución.");
                    return;
                }

                var nacionalidad = cbNacionalidadInstitucion.SelectedItem.ToString();

                var institucion = new
                {
                    nombre = txtNombreInst.Text.Trim(),
                    correo_electronico = txtCorreoInst.Text.Trim(),
                    contrasenia = txtContraseniaInst.Password.Trim(),
                    nacionalidad = nacionalidad,
                    telefono = txtTelefonoInst.Text.Trim(),
                    nombre_represent = txtNombreRepresent.Text.Trim(),
                    ap_pat_represent = txtApPatRepresent.Text.Trim(),
                    correo_electronico_represent = txtCorreoRepresent.Text.Trim(),
                    telefono_represent = txtTelefonoRepresent.Text.Trim()
                };

                var response = await cliente.PostAsJsonAsync("http://localhost:8000/api/visitantes/instituciones", institucion);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Institución registrada correctamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al registrar institución.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un tipo válido.");
            }
        }

        // Carga las nacionalidades en ambos ComboBox
        private async Task CargarNacionalidades()
        {
            try
            {
                var nacionalidades = await cliente.GetFromJsonAsync<List<Nacionalidad>>("http://localhost:8000/api/nacionalidades");

                if (nacionalidades != null)
                {
                    var listaPaises = nacionalidades.Select(n => n.nacionalidad).ToList();
                    cbNacionalidadTurista.ItemsSource = listaPaises;
                    cbNacionalidadInstitucion.ItemsSource = listaPaises;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar nacionalidades: {ex.Message}");
            }
        }


        // apartado del crud para cada cliente
        private async void CargarDatosVisitante()
        {
            string tipo = visitanteActual.tipo_visitante;

            if (tipo == "Turista")
            {
                cbTipo.SelectedIndex = 0; // Asumiendo que "Turista" es el primer item
                panelTurista.Visibility = Visibility.Visible;
                panelInstitucion.Visibility = Visibility.Collapsed;

                var turista = await cliente.GetFromJsonAsync<Turista>($"http://localhost:8000/api/visitantes/turistas/{visitanteActual.cod_visitante}");

                txtNombre.Text = turista.nombre;
                txtNombre.Text = turista.ap_pat;
                txtNombre.Text = turista.ap_mat;
                txtNombre.Text = turista.documento;
                txtCorreo.Text = turista.correo_electronico;
                txtCorreo.Text = turista.contrasenia;

                txtTelefono.Text = turista.telefono;
                cbNacionalidadTurista.SelectedItem = turista.nacionalidad;
            }
            else if (tipo == "Institucion")
            {
                cbTipo.SelectedIndex = 1; // Asumiendo que "Institucion" es el segundo item
                panelTurista.Visibility = Visibility.Collapsed;
                panelInstitucion.Visibility = Visibility.Visible;

                var institucion = await cliente.GetFromJsonAsync<Institucion>($"http://localhost:8000/api/visitantes/instituciones/{visitanteActual.cod_visitante}");

                // Carga campos de institución

                txtCorreoInst.Text = institucion.nombre;
                txtCorreoInst.Text = institucion.correo_electronico;
                txtCorreoInst.Text = institucion.contrasenia;
                cbNacionalidadInstitucion.SelectedItem = institucion.nacionalidad;
                txtTelefonoInst.Text = institucion.telefono;
                txtTelefonoInst.Text = institucion.nombre_represent;
                txtTelefonoInst.Text = institucion.ap_pat_represent;
                txtTelefonoInst.Text = institucion.correo_electronico_represent;
                txtTelefonoInst.Text = institucion.telefono_represent;
               
            }
        }
        // Bloquea inputs para no editar al inicio
        private void BloquearCampos()
        {
            txtNombre.IsReadOnly = true;
            txtCorreo.IsReadOnly = true;
            txtTelefono.IsReadOnly = true;
            txtContrasenia.IsEnabled = false;
            // Bloquea demás campos...
            btnGuardar.Visibility = Visibility.Collapsed;
            btnEditar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Visible;
        }
        private void HabilitarEdicion()
        {
            txtNombre.IsReadOnly = false;
            txtCorreo.IsReadOnly = false;
            txtTelefono.IsReadOnly = false;
            txtContrasenia.IsEnabled = true;
            // Habilita demás campos...
            btnGuardar.Visibility = Visibility.Visible;
            btnEditar.Visibility = Visibility.Collapsed;
        }
        // Evento para botón Editar
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            modoEdicion = true;
            HabilitarEdicion();
        }
        // Evento para botón Guardar
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!modoEdicion) return;

            string tipo = (cbTipo.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (tipo == "Turista")
            {
                var turista = new
                {
                    correo_electronico = txtCorreo.Text.Trim(),
                    contrasenia = txtContrasenia.Password.Trim(),
                    nombre = txtNombre.Text.Trim(),
                    // otros campos
                    telefono = txtTelefono.Text.Trim(),
                    nacionalidad = cbNacionalidadTurista.SelectedItem?.ToString()
                };

                var response = await cliente.PutAsJsonAsync($"http://localhost:8000/api/visitantes/turistas/{visitanteActual.cod_visitante}", turista);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Turista actualizado");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al actualizar turista");
                }
            }
            else if (tipo == "Institucion")
            {
                var institucion = new
                {
                    nombre = txtNombreInst.Text.Trim(),
                    correo_electronico = txtCorreoInst.Text.Trim(),
                    contrasenia = txtContraseniaInst.Password.Trim(),
                    telefono = txtTelefonoInst.Text.Trim(),
                    nacionalidad = cbNacionalidadInstitucion.SelectedItem?.ToString()
                    // otros campos
                };

                var response = await cliente.PutAsJsonAsync($"http://localhost:8000/api/visitantes/instituciones/{visitanteActual.cod_visitante}", institucion);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Institución actualizada");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al actualizar institución");
                }
            }
        }
        // Evento para botón Eliminar (borrado lógico)
        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("¿Seguro que quieres inactivar este visitante?", "Confirmar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var response = await cliente.PutAsync($"http://localhost:8000/api/visitantes/{visitanteActual.cod_visitante}/inactivar", null);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Visitante inactivado correctamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al inactivar visitante.");
                }
            }
        }




    }
}
