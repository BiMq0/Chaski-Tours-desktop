using chaski_tours_desk.Componentes.User.ListaDE;
using chaski_tours_desk.Modelos.ModelosNoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
    /// Lógica de interacción para Landing.xaml
    /// </summary>
    public partial class Landing : UserControl
    {
        HttpClient client = new HttpClient();

        LDE lstDECategorias = new LDE();

        LDE lstDEDepartamentos = new LDE();
        public Landing()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cargarListasDE();
        }

        private void cargarListasDE()
        {
            cargarListaDECategorias();
            cargarListaDEDepartamentos();
        }

        private void cargarListaDEDepartamentos()
        {
            try
            {
                List<Departamento> departamentos = new Departamento().ObtenerTodasLasDepartamentos();
                lstDEDepartamentos.crearListaDE(departamentos.Cast<object>().ToList());

                if (lstDEDepartamentos.Actual?.Valor is Departamento departamentoActual)
                {
                    ActualizarCuadroDepartamentos(departamentoActual);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cargarListaDECategorias()
        {
            try
            {
                List<Categoria> categorias = new Categoria().ObtenerTodasLasCategorias();
                lstDECategorias.crearListaDE(categorias.Cast<object>().ToList());

                if (lstDECategorias.Actual?.Valor is Categoria categoriaActual)
                {
                    ActualizarCuadroCategorias(categoriaActual);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActualizarCuadroDepartamentos(Departamento departamentoActual)
        {
            imgDepartamentos.Source = departamentoActual.Imagen;
            nomDepartamento.Text = departamentoActual.Nombre;
        }

        private void ActualizarCuadroCategorias(Categoria categoriaActual)
        {
            imgCategorias.Source = categoriaActual.Imagen;
            nomCategorias.Text = categoriaActual.Nombre;
        }

        private void btnDerechaDep_Click(object sender, RoutedEventArgs e)
        {
            lstDEDepartamentos.pasarSiguiente();
            ActualizarCuadroDepartamentos(lstDEDepartamentos.Actual.Valor as Departamento);
        }

        private void btnIzquierdaDep_Click(object sender, RoutedEventArgs e)
        {
            lstDEDepartamentos.pasarAnterior();
            ActualizarCuadroDepartamentos(lstDEDepartamentos.Actual.Valor as Departamento);
        }

        private void btnIzquierdaCat_Click(object sender, RoutedEventArgs e)
        {
            lstDECategorias.pasarSiguiente();
            ActualizarCuadroCategorias(lstDECategorias.Actual.Valor as Categoria);
        }

        private void btnDerechaCat_Click(object sender, RoutedEventArgs e)
        {
            lstDECategorias.pasarAnterior();
            ActualizarCuadroCategorias(lstDECategorias.Actual.Valor as Categoria);
        }
    }
}
