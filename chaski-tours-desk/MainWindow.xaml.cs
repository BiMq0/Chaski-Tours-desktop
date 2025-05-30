﻿using chaski_tours_desk.Componentes;
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
using System.IO;

namespace chaski_tours_desk
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Dashboard.cambiarVentana += cambiarVentanas;
        }

        public void cambiarVentanas(int index)
        {
            switch (index) { 
                case 0:
                    Inicio.Visibility = Visibility.Visible;
                    Reservas.Visibility = Visibility.Collapsed;
                    Clientes.Visibility = Visibility.Collapsed;
                    Tours.Visibility = Visibility.Collapsed;
                    Sitios.Visibility = Visibility.Collapsed;
                    Transportes.Visibility = Visibility.Collapsed;
                    Alojamientos.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    Inicio.Visibility = Visibility.Collapsed;
                    Reservas.Visibility = Visibility.Visible;
                    Clientes.Visibility = Visibility.Collapsed;
                    Tours.Visibility = Visibility.Collapsed;
                    Sitios.Visibility = Visibility.Collapsed;
                    Transportes.Visibility = Visibility.Collapsed;
                    Alojamientos.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    Inicio.Visibility = Visibility.Collapsed;
                    Reservas.Visibility = Visibility.Collapsed;
                    Clientes.Visibility = Visibility.Visible;
                    Tours.Visibility = Visibility.Collapsed;
                    Sitios.Visibility = Visibility.Collapsed;
                    Transportes.Visibility = Visibility.Collapsed;
                    Alojamientos.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    Inicio.Visibility = Visibility.Collapsed;
                    Reservas.Visibility = Visibility.Collapsed;
                    Clientes.Visibility = Visibility.Collapsed;
                    Tours.Visibility = Visibility.Visible;
                    Sitios.Visibility = Visibility.Collapsed;
                    Transportes.Visibility = Visibility.Collapsed;
                    Alojamientos.Visibility = Visibility.Collapsed;
                    break;
                case 4:
                    Inicio.Visibility = Visibility.Collapsed;
                    Reservas.Visibility = Visibility.Collapsed;
                    Clientes.Visibility = Visibility.Collapsed;
                    Tours.Visibility = Visibility.Collapsed;
                    Sitios.Visibility = Visibility.Visible;
                    Transportes.Visibility = Visibility.Collapsed;
                    Alojamientos.Visibility = Visibility.Collapsed;
                    break;
                case 5:
                    Inicio.Visibility = Visibility.Collapsed;
                    Reservas.Visibility = Visibility.Collapsed;
                    Clientes.Visibility = Visibility.Collapsed;
                    Tours.Visibility = Visibility.Collapsed;
                    Sitios.Visibility = Visibility.Collapsed;
                    Transportes.Visibility = Visibility.Visible;
                    Alojamientos.Visibility = Visibility.Collapsed;
                    break;
                case 6:
                    Inicio.Visibility = Visibility.Collapsed;
                    Reservas.Visibility = Visibility.Collapsed;
                    Clientes.Visibility = Visibility.Collapsed;
                    Tours.Visibility = Visibility.Collapsed;
                    Sitios.Visibility = Visibility.Collapsed;
                    Transportes.Visibility = Visibility.Collapsed;
                    Alojamientos.Visibility = Visibility.Visible;
                    break;
            }
        }
   

    }
}
