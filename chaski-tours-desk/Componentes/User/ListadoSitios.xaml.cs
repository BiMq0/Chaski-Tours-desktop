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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace chaski_tours_desk.Componentes.User
{
    /// <summary>
    /// Lógica de interacción para ListadoSitios.xaml
    /// </summary>
    public partial class ListadoSitios : UserControl
    {
        HttpClient client = new HttpClient();
        private string URL_Sitios = "http://localhost:8000/api/sitios/";
        public event EventHandler CerrarListadoSitios;
        public ObservableCollection<Sitio> SitiosParaBinding { get; set; } = new ObservableCollection<Sitio>();
        public ListadoSitios()
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

            List<Sitio> sitios = new List<Sitio>();

            sitios = await client.GetFromJsonAsync<List<Sitio>>(URL_Sitios);



            SitiosParaBinding.Clear();
            foreach (var sitio in sitios)
            {
                SitiosParaBinding.Add(sitio);
            }

        }

        private void btnReservar_Click(object sender, RoutedEventArgs e)
        {
            CerrarListadoSitios?.Invoke(this, EventArgs.Empty);
        }
    }
}
