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

namespace chaski_tours_desk.Componentes
{
    /// <summary>
    /// Lógica de interacción para SignUp.xaml
    /// </summary>
    public partial class SignUp : UserControl
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private async void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            try { await registrarCliente(); }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el cliente, Intente nuevamente");
                MessageBox.Show(ex.Message, "Error");
            }
            finally { aclarar(); }
        }

        private async Task<bool> registrarCliente()
        {
            oscurecer();
            return true;
        }

        private void cbTipoUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (panelCamposTurista == null || panelCamposInstitucion == null)
            {
                return;
            }

            if (cbTipoUsuario.SelectedIndex == 0)
            {
                panelCamposTurista.Visibility = Visibility.Visible;
                panelCamposInstitucion.Visibility = Visibility.Collapsed;
            }
            else
            {
                panelCamposTurista.Visibility = Visibility.Collapsed;
                panelCamposInstitucion.Visibility = Visibility.Visible;
            }
        }

        private void oscurecer()
        {
            SolidColorBrush oscuro = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BB635968"));
            if (cbTipoUsuario.SelectedIndex == 0)
            {
                brdDocumento.Background = oscuro;
                brdNombres.Background = oscuro;
                brdApPat.Background = oscuro;
                brdApMat.Background = oscuro;
                brdTelefono.Background =  oscuro;
                brdCorreo.Background = oscuro;
                brdPassword.Background = oscuro;
            }
            else {
                brdNomEmpresa.Background = oscuro;
                brdTelEmpresa.Background = oscuro;
                brdCorreoEmpresa.Background = oscuro;
                brdPassEmpresa.Background = oscuro;
            }
        }

        private void aclarar()
        {
            SolidColorBrush claro = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF"));
            if (cbTipoUsuario.SelectedIndex == 0)
            {
                brdDocumento.Background = claro;
                brdNombres.Background = claro;
                brdApPat.Background = claro;
                brdApMat.Background = claro;
                brdTelefono.Background = claro;
                brdCorreo.Background = claro;
                brdPassword.Background = claro;
            }
            else
            {
                brdNomEmpresa.Background = claro;
                brdTelEmpresa.Background = claro;
                brdCorreoEmpresa.Background = claro;
                brdPassEmpresa.Background = claro;
            }
        }
    }
}
