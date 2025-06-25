using chaski_tours_desk.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Net.Http.Json;

namespace chaski_tours_desk.Componentes.User
{
    /// <summary>
    /// Lógica de interacción para ListadoTours.xaml
    /// </summary>
    public partial class ListadoTours : UserControl
    {
        HttpClient client = new HttpClient();
        private string URLTours = "http://localhost:8000/api/tour/";
        public event EventHandler CerrarListadoTours;

        public ObservableCollection<Tour> SitiosParaBinding { get; set; } = new ObservableCollection<Tour>();
        public ListadoTours()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cargarDatosaCarts();
        }
        private async void cargarDatosaCarts()
        {

            List<Tour> tours = new List<Tour>();

            tours = await client.GetFromJsonAsync<List<Tour>>(URLTours);



            SitiosParaBinding.Clear();
            foreach (var tour in tours)
            {
                SitiosParaBinding.Add(tour);
            }

        }

        private void btnReservar_Click(object sender, RoutedEventArgs e)
        {
            CerrarListadoTours?.Invoke(this, EventArgs.Empty);
        }
    }
}
