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
    public partial class Clientes : UserControl
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/visitantes/";
        private List<Visitante> todosLosClientes = new List<Visitante>();

        public Clientes()
        {
            InitializeComponent();
        }

        private async Task obtenerClientes()
        {
            /*todosLosClientes = await cliente.GetFromJsonAsync<List<Visitante>>(URL);
            AplicarFiltros();*/
            todosLosClientes = await cliente.GetFromJsonAsync<List<Visitante>>(URL);
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            var resultado = todosLosClientes.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(txbBusqueda.Text))
            {
                string busqueda = txbBusqueda.Text.ToLower();
                resultado = resultado.Where(c =>
                    (!string.IsNullOrEmpty(c.NombreCompleto) && c.NombreCompleto.ToLower().Contains(busqueda))
                    || (!string.IsNullOrEmpty(c.cod_visitante) && c.cod_visitante.ToLower().Contains(busqueda))
                );
            }


            if (chkActivos.IsChecked == true && chkInactivos.IsChecked == false)
            {
                resultado = resultado.Where(c => c.Activo == 1);

            }
            else if (chkInactivos.IsChecked == true && chkActivos.IsChecked == false)
            {
                resultado = resultado.Where(c => c.Activo == 0);
            }

            if (chkTuristas.IsChecked == true && chkInstituciones.IsChecked == false)
            {
                resultado = resultado.Where(c => c.tipo_visitante == "Turista");
            }
            else if (chkInstituciones.IsChecked == true && chkTuristas.IsChecked == false)
            {
                resultado = resultado.Where(c => c.tipo_visitante == "Institucion");
            }

            tbl_Clientes.ItemsSource = resultado.ToList();
        }

        private async void verClientes()
        {
            await obtenerClientes();
        }

        private void verDatos()
        {
            if (Window.GetWindow(this).Visibility == Visibility.Visible)
            {
                verClientes();
            }
        }

        private void Cliente_Loaded(object sender, RoutedEventArgs e)
        {
            verDatos();
        }

        private void Filtros_Changed(object sender, RoutedEventArgs e)
        {
            AplicarFiltros();
        }

        private void txbBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            AplicarFiltros();
        }

        private void AbrirFormulario_Click(object sender, RoutedEventArgs e)
        {
            var form = new FormularioCliente();
            form.ShowDialog();
            verClientes(); 
        }

        //para hacer el crud de cadad cliente es decir veremos los detalles
        private void tbl_Clientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var clienteSeleccionado = (Visitante)tbl_Clientes.SelectedItem;
            if (clienteSeleccionado != null)
            {
                var formDetalle = new FormularioCliente(clienteSeleccionado);
                formDetalle.ShowDialog();
                // Después de cerrar, refresca la lista por si hubo cambios
                _ = obtenerClientes();
            }
        }
       
        private void dgVisitantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbl_Clientes.SelectedItem is Visitante visitanteSeleccionado)
            {
                var formulario = new FormularioCliente(visitanteSeleccionado);
                formulario.ShowDialog();

                // Deselecciona la fila para permitir abrir nuevamente el mismo visitante
                tbl_Clientes.SelectedItem = null;

                
            }
        }




    }
}
