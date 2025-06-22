using chaski_tours_desk.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace chaski_tours_desk.Componentes.Admin.FormsInfo
{
    /// <summary>
    /// Lógica de interacción para InfoTour.xaml
    /// </summary>
    public partial class InfoTour : Window
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/tours/";
        private string URL_sitio = "http://localhost:8000/api/sitios/";
        private string URL_aloja = "http://localhost:8000/api/alojamientos/";
        public InfoTour(int id_tour)
        {
            InitializeComponent();
            cargarDatos(id_tour);
        }
        private async void cargarDatos(int id_tour)
        {
            try
            {
                var tour = await cliente.GetFromJsonAsync<Tour>(URL + id_tour);
                if (tour != null)
                {
                    hiddenId.Text = tour.id_tour.ToString();
                    txbNombreTour.Text = tour.nombre_tour;
                    txbDescTour.Text = tour.descripcion_tour;
                    txbCostoTour.Text = $"Bs. {tour.costo_tour:F2}";
                    txbDias.Text = tour.duracion_dias.ToString();
                    txbNoches.Text = tour.duracion_noches.ToString();
                    cmbActivo.SelectedIndex = tour.Activo == 1 ? 1 : 0;
                }
                else
                {
                    MessageBox.Show("No se encontró el tour.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del tour: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
