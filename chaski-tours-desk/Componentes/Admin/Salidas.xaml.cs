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

namespace chaski_tours_desk.Componentes.Admin
{
    /// <summary>
    /// Lógica de interacción para Salidas.xaml
    /// </summary>
    public partial class Salidas : UserControl
    {
        public Salidas()
        {
            InitializeComponent();
            CargarDias();
        }

        private void CargarDias() { 
            List<string> dias = new List<string> { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado"};
            int cols = 0;
            dias.ForEach(dia => {
                var txtBlock = new TextBlock
                {
                    Text = dia,
                    Style = (Style)Application.Current.Resources["TituloTXB"],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 10, 0, 10)
                };
                Grid.SetRow(txtBlock, 1);
                Grid.SetColumn(txtBlock, cols++);
                grilla.Children.Add(txtBlock);
            });
        }
    }
}
