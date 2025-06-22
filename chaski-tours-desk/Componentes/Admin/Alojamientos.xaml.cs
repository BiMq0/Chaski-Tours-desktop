using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using chaski_tours_desk.Modelos;

namespace chaski_tours_desk.Componentes.Admin
{
    public partial class Alojamientos : UserControl
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/alojamientos";
        private List<Alojamiento> todosLosAlojamientos = new List<Alojamiento>();

        public Alojamientos()
        {
            InitializeComponent();
        }

        private async Task obtenerAlojamientos()
        {
            try
            {
                var response = await cliente.GetFromJsonAsync<List<Alojamiento>>(URL);
                todosLosAlojamientos = response ?? new List<Alojamiento>();
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener alojamientos: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AplicarFiltros()
        {
            if (todosLosAlojamientos == null || txbBusqueda == null || chkActivos == null || chkInactivos == null)
                return;

            var resultado = todosLosAlojamientos.AsEnumerable();

            // Filtro por nombre
            if (!string.IsNullOrWhiteSpace(txbBusqueda.Text))
            {
                string busqueda = txbBusqueda.Text.ToLower();
                resultado = resultado.Where(a =>
                    a.nombre_aloj != null &&
                    a.nombre_aloj.ToLower().Contains(busqueda)
                );
            }

            // Filtro por estado (Activo/Inactivo)
            if (chkActivos.IsChecked == true && chkInactivos.IsChecked == false)
            {
                resultado = resultado.Where(a => a.Activo == 1);
            }
            else if (chkInactivos.IsChecked == true && chkActivos.IsChecked == false)
            {
                resultado = resultado.Where(a => a.Activo == 0);
            }
            // Si ambos están checkeados o ninguno, muestra todos

            tbl_Alojamientos.ItemsSource = resultado.ToList();
        }

        private async void verAlojamientos()
        {
            await obtenerAlojamientos();
        }

        private void Alojamiento_Loaded(object sender, RoutedEventArgs e)
        {
            verAlojamientos();
        }

        private void txbBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            AplicarFiltros();
        }

        private void Filtros_Changed(object sender, RoutedEventArgs e)
        {
            AplicarFiltros();
        }

        private void AbrirFormulario_Click(object sender, RoutedEventArgs e)
        {
            var formulario = new FormularioAlojamiento();
            formulario.Owner = Window.GetWindow(this);

            if (formulario.ShowDialog() == true)
            {
                verAlojamientos();
            }
        }

        
    }
}