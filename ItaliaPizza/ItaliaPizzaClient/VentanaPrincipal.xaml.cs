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
    public partial class VentanaPrincipal : Window
    {
        private string usuarioRol;

        public VentanaPrincipal(string rol)
        {
            InitializeComponent();
            usuarioRol = rol;
            MostrarInformacionUsuario();
            EstablecerVisibilidadBotones();
        }

        private void MostrarInformacionUsuario()
        {
            lbNombreUsuario.Content = $"Hola {Domain.Empleados.EmpleadosClient.Nombre}";
        }

        private void EstablecerVisibilidadBotones()
        {
            switch (usuarioRol)
            {
                case "Gerente":
                    btnUsuarios.Visibility = Visibility.Visible;
                    btnPedidos.Visibility = Visibility.Visible;
                    btnInventario.Visibility = Visibility.Visible;
                    btnRecetas.Visibility = Visibility.Visible;
                    btnFinanzas.Visibility = Visibility.Visible;
                    break;
                case "Cajero":
                    btnPedidos.Visibility = Visibility.Visible;
                    btnInventario.Visibility = Visibility.Visible;
                    btnFinanzas.Visibility = Visibility.Visible;
                    break;
                case "Cocinero":
                    btnPedidos.Visibility = Visibility.Visible;
                    btnRecetas.Visibility = Visibility.Visible;
                    break;
                case "Cliente":
                    // Si no quieres mostrar ningún botón para el cliente, deja todo oculto
                    break;
                default:
                    // Oculta todos los botones si el rol no coincide con ninguno conocido
                    btnUsuarios.Visibility = Visibility.Collapsed;
                    btnPedidos.Visibility = Visibility.Collapsed;
                    btnInventario.Visibility = Visibility.Collapsed;
                    btnRecetas.Visibility = Visibility.Collapsed;
                    btnFinanzas.Visibility = Visibility.Collapsed;
                    break;
            }
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