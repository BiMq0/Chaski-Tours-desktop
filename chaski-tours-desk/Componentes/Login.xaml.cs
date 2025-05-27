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
            try
            {
                async Task verificarUsuario()
                {
                    brdMail.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BB635968"));
                    brdPass.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BB635968"));
                    
                    var usuario = await cliente.GetFromJsonAsync<List<Turista>>(URL+txtUsuario.Text);
                    MessageBox.Show(usuario.Count.ToString());
                    if (usuario[0].correo_electronico == txtUsuario.Text && usuario[0].contrasenia == txtPassword.Password)
                    {
                        MessageBox.Show("Bienvenido");
                        FullLayout fullLayout = new FullLayout();
                        fullLayout.Show();
                        Window.GetWindow(this).Close();
                    }
                    else
                    {
                        usuario = null;
                        MessageBox.Show("Correo o contraseña incorrectos UnU");
                    }
                }

                await verificarUsuario();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al verificar el usuario, Intente nuevamente");
                MessageBox.Show(ex.Message);
            }
            finally {
                brdMail.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
                brdPass.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            }
        }
    }
}
