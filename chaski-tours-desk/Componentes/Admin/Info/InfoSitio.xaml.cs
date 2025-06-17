using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using chaski_tours_desk.Modelos;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Maps.MapControl.WPF;


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
            mapControl.ZoomLevel = 10;


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
    }
}
