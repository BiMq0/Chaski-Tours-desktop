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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http.Json;
using chaski_tours_desk.Modelos;

namespace chaski_tours_desk.Componentes
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private string URL = "http://localhost:8000/api/visitantes/turistas/";
        private static readonly HttpClient cliente = new HttpClient();
        public Login()
        {
            InitializeComponent();
        }

        

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            async Task verificarUsuario(){
                var usuario = await cliente.GetFromJsonAsync<List<Turista>>(URL+txtUsuario.Text);

                MessageBox.Show(usuario[0].cod_visitante); 
                MessageBox.Show(usuario[0].correo_electronico); 
                MessageBox.Show(usuario[0].contrasenia); 
                MessageBox.Show(usuario[0].documento.ToString()); 
                MessageBox.Show(usuario[0].nombre); 
                MessageBox.Show(usuario[0].ap_pat); 
                MessageBox.Show(usuario[0].ap_mat); 
                MessageBox.Show(usuario[0].nacionalidad);
                MessageBox.Show(usuario[0].telefono);
            }

            await verificarUsuario();
        }
    }
}
