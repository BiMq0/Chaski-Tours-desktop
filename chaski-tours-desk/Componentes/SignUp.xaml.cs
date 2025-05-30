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


        private void registrarTurista() { 
        
        }
    }
}
