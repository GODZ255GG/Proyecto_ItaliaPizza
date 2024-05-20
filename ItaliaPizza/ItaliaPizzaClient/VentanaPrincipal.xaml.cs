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
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
            MostrarInformacionUsuario();
        }

        private void MostrarInformacionUsuario()
        {
            lbNombreUsuario.Content = ("Hola " + Domain.Empleados.EmpleadosClient.Nombre);
        }

        private void BtnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ConsultarEmpleado();
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Inventario();
        }

        private void btnFinanzas_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Finanzas();
        }

        private void BtnReceta_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ListaRecetas();
        }

        private void BtnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnPedidos_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ListaPedidos();
        }
    }
}
