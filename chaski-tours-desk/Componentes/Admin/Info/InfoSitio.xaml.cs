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


namespace chaski_tours_desk.Componentes.Admin.Info
{
    /// <summary>
    /// Lógica de interacción para InfoSitio.xaml
    /// </summary>
    public partial class InfoSitio : Window
    {
        private HttpClient cliente = new HttpClient();
        private string URL = "http://localhost:8000/api/sitios/";
        public InfoSitio(int id_sitio)
        {
            InitializeComponent();
            mostrarSitio(id_sitio);
        }
        private void mostrarSitio(int id_sitio) {
            var sitio = obtenerSitio(id_sitio).Result;
            
            txbNombreSitio.Text = sitio.nombre;
            txbDescConceptualSitio.Text = sitio.desc_conceptual_sitio;
            txbDescHistoricaSitio.Text = sitio.desc_historica_sitio;
            txbCostoSitio.Text = "Precio: \n" + (sitio.costo_sitio.ToString() == "0" ? "Gratis" : "Bs. " + sitio.costo_sitio.ToString());
            txbTemporadaSitio.Text = sitio.temporada_recomendada;
            txbRecomendacionSitio.Text = sitio.recomendacion_climatica;
            txbAperturaSitio.Text = sitio.horario_apertura;
            txbCierreSitio.Text = sitio.horario_cierre;
            txbActivoSitio.Text = sitio.Activo == 1 ? "Activo" : "Inactivo";
        }

        private Task<Sitio> obtenerSitio(int id_sitio)
        {
            return cliente.GetFromJsonAsync<Sitio>(URL + id_sitio);
        }
    }
}
