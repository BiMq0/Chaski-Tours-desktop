﻿using chaski_tours_desk.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace chaski_tours_desk.Componentes.User
{
    public partial class ReservaForm : Window
    {
        private List<Tour> toursDisponibles;
        private List<CalendarioSalida> salidasDisponibles;
        private Tour tourSeleccionado;
        private string tipoUsuario;
        private HttpClient cliente = new HttpClient();

        public ReservaForm(string tipo)
        {
            InitializeComponent();
            tipoUsuario = tipo;
            ConfigurarInterfazSegunTipoUsuario();
            CargarTours();
        }

        private void ConfigurarInterfazSegunTipoUsuario()
        {
            if (tipoUsuario == "turista")
            {
                txtCantidadPersonas.Text = "1";
                txtCantidadPersonas.IsEnabled = false;
            }
            else
            {
                txtCantidadPersonas.IsEnabled = true;
            }
            // Asegurarse que se calcule apenas se configure el formulario

        }

        private async void CargarTours()
        {
            try
            {
                var response = await cliente.GetFromJsonAsync<List<Tour>>("http://localhost:8000/api/tour");
                if (response != null)
                {
                    toursDisponibles = response.Where(t => t.Activo == 1).ToList();
                    cmbTours.ItemsSource = toursDisponibles;
                    cmbTours.DisplayMemberPath = "nombre_tour";
                    cmbTours.SelectedValuePath = "id_tour";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los tours: {ex.Message}");
            }
        }

        private void txtCantidadPersonas_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(txtCantidadPersonas.Text, out int cantidad) || cantidad <= 0)
            {
                txtCantidadPersonas.Text = tipoUsuario == "turista" ? "1" : "";
                return;
            }

            int maximo = tipoUsuario == "turista" ? 10 : 25;
            if (cantidad > maximo)
            {
                MessageBox.Show($"La cantidad máxima es {maximo} para {(tipoUsuario == "turista" ? "turistas" : "instituciones")}");
                txtCantidadPersonas.Text = maximo.ToString();
                cantidad = maximo;
            }

            CalcularTotalAPagar();
        }

        private async void cmbTours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tourSeleccionado = cmbTours.SelectedItem as Tour;
            if (tourSeleccionado != null)
            {
                txtCostoTour.Text = tourSeleccionado.costo_tour.ToString("F2");
                txtDuracion.Text = $"{tourSeleccionado.duracion_dias} días / {tourSeleccionado.duracion_noches} noches";

                var response = await cliente.GetFromJsonAsync<List<CalendarioSalida>>("http://localhost:8000/api/calendario");
                salidasDisponibles = response?.FindAll(s => s.id_tour == tourSeleccionado.id_tour);
                cmbFechas.ItemsSource = salidasDisponibles;
                cmbFechas.DisplayMemberPath = "fecha_salida";
                cmbFechas.SelectedValuePath = "id_salida";
                CalcularTotalAPagar();
            }
        }

        private void CalcularTotalAPagar()
        {
            if (tourSeleccionado != null && int.TryParse(txtCantidadPersonas.Text, out int cantidad) && cantidad > 0)
            {
                double total = tourSeleccionado.costo_tour * cantidad;
                txtMontoAPagar.Text = total.ToString("C");
            }
        }

        private async void btnConfirmarReserva_Click(object sender, RoutedEventArgs e)
        {
            if (tourSeleccionado == null || cmbFechas.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un tour y una fecha disponible", "Datos incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtCantidadPersonas.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida de personas", "Dato inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int maximo = tipoUsuario == "turista" ? 10 : 25;
            if (cantidad > maximo)
            {
                MessageBox.Show($"La cantidad máxima es {maximo} para {(tipoUsuario == "turista" ? "turistas" : "instituciones")}",
                              "Límite excedido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Reserva reserva = new Reserva
                {
                    cod_visitante = MainWindow.codVisitanteActual,
                    id_salida = (int)cmbFechas.SelectedValue,
                    cantidad_personas = cantidad,
                    costo_total_reserva = tourSeleccionado.costo_tour * cantidad,
                    estado = "Pendiente"
                };


                var result = await cliente.PostAsJsonAsync("http://localhost:8000/api/reservas/crear", reserva);

                if (result.IsSuccessStatusCode)
                {
                    MostrarMensajeConfirmacion(reserva);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al registrar la reserva. Por favor intente nuevamente.",
                                  "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MostrarMensajeConfirmacion(Reserva reserva)
        {
            var salida = (CalendarioSalida)cmbFechas.SelectedItem;
            DateTime.TryParse(salida.fecha_salida, out DateTime fechaSalidaParseada);
            string fechaFormateada = fechaSalidaParseada.ToString("dd/MM/yyyy");

            string mensaje = $@"
¡Reserva registrada con éxito!

Detalles:
- Tour: {tourSeleccionado.nombre_tour}
- Fecha: {fechaFormateada}
- Cantidad de personas: {reserva.cantidad_personas}
- Total a pagar: {reserva.costo_total_reserva:C}

Para confirmar su reserva, por favor acérquese a nuestra agencia 
para realizar el pago correspondiente en un plazo de 48 horas.

¡Gracias por elegir Chaski Tours!";

            MessageBox.Show(mensaje, "Reserva Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
