﻿using System;
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
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void listaBtns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cambiarVentana.Invoke(listaBtns.SelectedIndex);
        }

        public event Action<int> cambiarVentana;
    }
}
