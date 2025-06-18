using chaski_tours_desk.Modelos;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using chaski_tours_desk.Componentes.Admin;


namespace chaski_tours_desk.Componentes.Admin.Info
{
    /// <summary>
    /// Lógica de interacción para InfoSitio.xaml
    /// </summary>
    public partial class InfoSitio : Window
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/sitios/";
        private string URL_Ubi = "http://localhost:8000/api/ubicaciones/";
        public InfoSitio(int id_sitio)
        {
            InitializeComponent();
            obtenerUbicacion(obtenerSitio(id_sitio));
            cargarDatos(obtenerSitio(id_sitio));
        }

        private void cargarDatos(Sitio sitio)
        {
            hiddenId.Text = sitio.id_sitio.ToString();
            txbNombreSitio.Text = sitio.nombre;
            txbDescConceptual.Text = sitio.desc_conceptual_sitio;
            txbDescHistorica.Text = sitio.desc_historica_sitio;
            txbCostoSitio.Text = sitio.costo_sitio == 0 ? "Gratis" : "Bs." + sitio.costo_sitio.ToString();
            txbTemporada.Text = sitio.temporada_recomendada;
            txbRecomendacion.Text = sitio.recomendacion_climatica;
            txbAperturaSitio.Text = sitio.horario_apertura;
            txbCierreSitio.Text = sitio.horario_cierre;
            txbActivo.Text = sitio.Activo == 1 ? "Activo" : "Inactivo";
        }

        private Sitio obtenerSitio(int id_sitio)
        {   
            var sitio = cliente.GetFromJsonAsync<Sitio>(URL + id_sitio).Result;
            
            return sitio;
        }

        private void obtenerUbicacion(Sitio sitio)
        {
            var ubicacion = cliente.GetFromJsonAsync<List<Ubicacion>>(URL_Ubi + sitio.id_ubicacion).Result;
            cargarMapa(ubicacion[0]);
        }

        private  void cargarMapa(Ubicacion ubi) {

            var mapControl = new Map();
            mapControl.Center = new Location(double.Parse(ubi.latitud), double.Parse(ubi.longitud));
            //mapControl.Center = new Location(-17.383300, -66.166700);
            mapControl.ZoomLevel = 15;


            var pushpin = new Pushpin()
            {
                Location = new Location(double.Parse(ubi.latitud), double.Parse(ubi.longitud)),
                //Location = new Location(-17.383300, -66.166700),
                Background = new SolidColorBrush(Colors.Red),
            };

            mapControl.Children.Add(pushpin);
            mapControl.Height = 1200;
            mapa.Children.Add( mapControl);
        }

        private int editUpdate = 0;
        private async void btnEditarSitio_Click(object sender, RoutedEventArgs e)
        {

            void habilitar(bool valor) {
                txbNombreSitio.IsEnabled = valor;
                txbDescConceptual.IsEnabled = valor;
                txbDescHistorica.IsEnabled = valor;
                txbCostoSitio.IsEnabled = valor;
                txbTemporada.IsEnabled = valor;
                txbRecomendacion.IsEnabled = valor;
                txbAperturaSitio.IsEnabled = valor;
                txbCierreSitio.IsEnabled = valor;
            }
            editUpdate++;
            if (editUpdate == 1)
            {
                brdEditar.Style = (Style)Application.Current.Resources["BordeBotonesUser"];
                btnEditarSitio.Style = (Style)Application.Current.Resources["UserButtonStyle"];
                btnEditarSitio.Content = "Guardar";
                habilitar(true);
                btnEliminarSitio.Visibility = Visibility.Collapsed;
                btnVolver.Visibility = Visibility.Collapsed;
            }
            else if(editUpdate == 2){
                var nuevoSitio = new Sitio{
                    desc_conceptual_sitio = txbDescConceptual.Text,
                    desc_historica_sitio = txbDescHistorica.Text,
                    costo_sitio = txbCostoSitio.Text == "Gratis" ? 0 : double.Parse(txbCostoSitio.Text.Replace("Bs.", "").Trim()),
                    temporada_recomendada = txbTemporada.Text,
                    recomendacion_climatica = txbRecomendacion.Text,
                    horario_apertura = txbAperturaSitio.Text,
                    horario_cierre = txbCierreSitio.Text
                };

                var response = await cliente.PutAsJsonAsync(URL + hiddenId.Text, nuevoSitio);

                if (response.IsSuccessStatusCode)MessageBox.Show("Sitio actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                else MessageBox.Show("Error al actualizar el sitio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                brdEditar.Style = (Style)Application.Current.Resources["BordeBotonesSecundarios"];
                btnEditarSitio.Style = (Style)Application.Current.Resources["TertiaryButtonStyle"];
                btnEditarSitio.Content = "Editar";
                habilitar(false);
                btnEliminarSitio.Visibility = Visibility.Visible;
                btnVolver.Visibility = Visibility.Visible;
            }
        }

        private void btnEliminarSitio_Click(object sender, RoutedEventArgs e)
        {
            cliente.DeleteFromJsonAsync<Sitio>(URL + hiddenId.Text);
            Sitios sitio = new Sitios();
            sitio.verDatos();
            Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
