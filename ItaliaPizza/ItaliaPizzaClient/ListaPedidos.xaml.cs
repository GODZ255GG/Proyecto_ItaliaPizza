using ItaliaPizzaClient.ItaliaPizzaServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace ItaliaPizzaClient
{
    /// <summary>
    /// Interaction logic for ListaPedidos.xaml
    /// </summary>
    public partial class ListaPedidos : UserControl
    {
        ItaliaPizzaServer.OrderManagerClient pedidosServer = new ItaliaPizzaServer.OrderManagerClient();
        public ListaPedidos()
        {
            InitializeComponent();
            CargarPedidos();
        }

        private void ActualizarTablaPedidos(object sender, EventArgs e)
        {
            CargarPedidos();
        }

        private void CargarPedidos()
        {
            try
            {
                var pedidosDesdeBD = pedidosServer.RecuperarInformacionPedidos();
                dgPedidos.ItemsSource = pedidosDesdeBD;
            }
            catch (EndpointNotFoundException ex)
            {
                MessageBox.Show("Por el momento no hay conexión con la base de datos, por favor inténtelo más tarde", "Error de conexión con base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Se produjo un error de comunicación al intentar acceder a un recurso remoto. Intente de nuevo", "Problema de comunicación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("La operación que intentaba realizar ha superado el tiempo de espera establecido y no pudo completarse en el tiempo especificado. Intente de nuevo", "Tiempo de espera agotado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ImgConfiguracion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Configuracion confi = new Configuracion();

            this.IsEnabled = false;

            bool? result = confi.ShowDialog();

            this.IsEnabled = true;
        }

        private void BtnRegistrarPedidos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RegistrarPedidos registrarPedidos = new RegistrarPedidos();
                registrarPedidos.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                registrarPedidos.Closed += ActualizarTablaPedidos;

                this.IsEnabled = false;

                bool? result = registrarPedidos.ShowDialog();

                this.IsEnabled = true;
            }
            catch (EndpointNotFoundException ex)
            {
                MessageBox.Show("Por el momento no hay conexión con la base de datos, por favor inténtelo más tarde", "Error de conexión con base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Se produjo un error de comunicación al intentar acceder a un recurso remoto. Intente de nuevo", "Problema de comunicación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException ex)
            {
                MessageBox.Show("La operación que intentaba realizar ha superado el tiempo de espera establecido y no pudo completarse en el tiempo especificado. Intente de nuevo", "Tiempo de espera agotado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DgPedidos_MouseDobleClic(object sender, MouseButtonEventArgs e)
        {
            if (dgPedidos.SelectedItem != null)
            {
                var pedidoSeleccionado = (Pedidos)dgPedidos.SelectedItem;

                DetallesPedido detallesPedido = new DetallesPedido(pedidoSeleccionado);
                detallesPedido.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                detallesPedido.Closed += ActualizarTablaPedidos;

                this.IsEnabled = false;

                bool? result = detallesPedido.ShowDialog();

                this.IsEnabled = true;
            }
        }

        private void TbBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Pedidos[] pedidosArray = pedidosServer.RecuperarInformacionPedidos();
            List<Pedidos> pedidos = pedidosArray.ToList();
            var tbx = sender as TextBox;
            if (tbx != null)
            {
                var filtrarLista = pedidos.Where(x => x.EstadoDelPedido.Contains(tbx.Text)).ToList();
                dgPedidos.ItemsSource = null;
                dgPedidos.ItemsSource = filtrarLista;
            }
            else
            {
                dgPedidos.ItemsSource = pedidos;
            }
        }
    }
}
