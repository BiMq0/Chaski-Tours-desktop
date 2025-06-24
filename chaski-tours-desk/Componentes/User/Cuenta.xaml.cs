﻿using chaski_tours_desk.Modelos;
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
using System.Windows.Shapes;

namespace chaski_tours_desk.Componentes.User
{
    /// <summary>
    /// Lógica de interacción para Cuenta.xaml
    /// </summary>
    public partial class Cuenta : Window
    {
        HttpClient cliente = new HttpClient();
        private string URL_Turista = "http://localhost:8000/visitantes/turistas/cod/";
        private string URL_Reserva = "http://localhost:8000/reservas/cod/";
        private string cod_visitante;

        public Cuenta(string codVisitante)
        {
            InitializeComponent();
            cod_visitante = codVisitante;
            cargarPerfil();
            cargarReservas();
        }
        private void cargarPerfil()
        {
            try
            {
                var turista = cliente.GetFromJsonAsync<Turista>(URL_Turista + cod_visitante).Result;
                if (turista != null)
                {
                    txtNombre.Text = turista.nombre;
                    txtCorreo.Text = turista.correo_electronico;
                    txtDocumento.Text = turista.documento;
                    txtTelefono.Text = turista.telefono;
                    txtFechaNac.Text = turista.fecha_nac;
                }
                else
                {
                    MessageBox.Show("No se pudo cargar el perfil del turista.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el perfil: " + ex.Message);
            }
        }
        private void cargarReservas()
        {
            try
            {
                var reservas = cliente.GetFromJsonAsync<List<Reserva>>(URL_Reserva + cod_visitante).Result;

                if (reservas != null && reservas.Any())
                {
                    listaReservas.ItemsSource = reservas;
                }
                else
                {
                    listaReservas.ItemsSource = new List<Reserva> {
                    new Reserva { id_salida = 0, cantidad_personas = 0, costo_total_reserva = 0, estado = "Aún no ha realizado reservas." }
                };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el historial de reservas: " + ex.Message);
            }
        }
    }
}
