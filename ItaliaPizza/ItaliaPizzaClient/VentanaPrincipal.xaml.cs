using System.Windows;

namespace ItaliaPizzaClient
{
    public partial class VentanaPrincipal : Window
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
            MostrarInformacionUsuario();
        }

        private void MostrarInformacionUsuario()
        {
            lbNombreUsuario.Content = $"Hola {Domain.Empleados.EmpleadosClient.Nombre}";
        }

        private void BtnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ConsultarEmpleado();
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Inventario();
        }

        private void BtnFinanzas_Click(object sender, RoutedEventArgs e)
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

        private void BtnPedidos_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ListaPedidos();
        }
    }
}