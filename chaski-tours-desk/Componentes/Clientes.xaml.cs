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

namespace chaski_tours_desk.Componentes
{
    /// <summary>
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class Clientes : UserControl
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/visitantes/";
        public Clientes()
        {
            InitializeComponent();
        }

        private async Task obtenerClientes() {
            var usuarios = await cliente.GetFromJsonAsync<List<Visitante>>(URL);
            
            tbl_Clientes.ItemsSource = usuarios;
        }

        private async void verClientes() {
            await obtenerClientes();
        }

        private void verDatos() {
            if (Window.GetWindow(this).Visibility == Visibility.Visible) {
                verClientes();
            }
        }

        private void Cliente_Loaded(object sender, RoutedEventArgs e)
        {
            verDatos();
        }
    }
}
