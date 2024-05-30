using System.Windows;

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Lógica de interacción para VentanaCocinero.xaml
    /// </summary>
    public partial class VentanaCocinero : Window
    {
        public VentanaCocinero()
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

        private void BtnReceta_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ListaRecetas();
        }
    }
}
