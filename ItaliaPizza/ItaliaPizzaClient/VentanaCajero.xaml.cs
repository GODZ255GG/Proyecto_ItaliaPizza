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
using System.Windows.Shapes;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para VentanaCajero.xaml
    /// </summary>
    public partial class VentanaCajero : Window
    {
        public VentanaCajero()
        {
            InitializeComponent();
            MostrarInformacionUsuario();
        }

        private void MostrarInformacionUsuario()
        {
            lbNombreUsuario.Content = $"Hola {Domain.Empleados.EmpleadosClient.Nombre}";
        }

        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void BtnPedidos_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ListaPedidos();
        }

        private void BtnFinanzas_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Finanzas();
        }
    }
}
